﻿using Microsoft.Extensions.DependencyInjection;
using StepByStep.Sandbox;
using StepByStep.Sandbox.ExpressionEvaluators;
using StepByStep.Sandbox.Functions;
using StepByStep.Sandbox.Steps;
using StepByStep.Sandbox.Steps.RestApis.HTTPs;
using StepByStep.Sandbox.Steps.Variables.InitializeVariable;
using StepByStep.Sandbox.Steps.Variables.SetVariable;
using StepByStep.Sandbox.Triggers;
using System.Reflection;
using System.Text.Json;

var serviceCollection = new ServiceCollection();
RegisterFunctions(serviceCollection);
RegisterStepHandlers(serviceCollection);

serviceCollection.AddTransient<IExpressionEvaluator, ExpressionEvaluator>();

serviceCollection.AddTransient<IInitializeVariableStep, InitializeVariableStep>();
serviceCollection.AddTransient<ISetVariableValueStep, SetVariableValueStep>();
serviceCollection.AddTransient<IHttpStep, HttpStep>();
serviceCollection.AddTransient<IStepResolver, StepResolver>();
serviceCollection.AddTransient<IAutomatProcessor, AutomatProcessor>();

serviceCollection.AddHttpClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var allFunctions = serviceProvider.GetServices<IFunction>();

var expressionEvaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();

var firstNameVariable = new InitializeVariableStep("Initialize firstname", 
    new Variable("First Name", VariableType.String, "Karol"));
var lastNameVariable = new InitializeVariableStep("Initialize lastname", 
    new Variable("Last Name", VariableType.String, "Niedziela"));
var fullnameVariable = new InitializeVariableStep("Initialize fullname", 
    new Variable("Full name", VariableType.String, "@concat(First Name, ' ', Last Name)"));
var emptyVariable = new InitializeVariableStep("Initialize empty",
    new Variable("Empty", VariableType.String, null));
var setValueVariable = new SetVariableValueStep("Set value", emptyVariable.Variable!, lastNameVariable.Variable!);

var httpGetStep = new HttpStep
{
    DisplayName = "Random user api",
    Url = "https://randomuser.me/api/",
    Method = HttpMethod.Get
};


var variables = new List<Variable>
{
    new("StringVariable", VariableType.String, "@concat(variable1, ' ', variable2)"),
    new("variable1", VariableType.String, "Hello"),
    new("variable2", VariableType.String, "World"),
    new("variable3", VariableType.Integer, "5"),
    new("variable4", VariableType.Integer,  "2"),
    firstNameVariable.Variable!,
    lastNameVariable.Variable!,
    fullnameVariable.Variable!,
    emptyVariable.Variable!,
    setValueVariable.Value
};

var result1 = expressionEvaluator.Evaluate("@concat(variables(variable1), ' ', variables(variable2))", returnVariableType: VariableType.String, variables: variables);
Console.WriteLine(result1); // Hello World

var result2 = expressionEvaluator.Evaluate("@concat(concat(variables(variable1), ' '), ' ', variables(variable2))", VariableType.String, variables);
Console.WriteLine(result2); // Hello World

var result3 = expressionEvaluator.Evaluate("@substring(variables(variable1), 0, 4)", VariableType.String, variables);
Console.WriteLine(result3); // Hell

var result4 = expressionEvaluator.Evaluate("substring");
Console.WriteLine(result4); // substring

var result5 = expressionEvaluator.Evaluate("@add(variables(variable3), 4)", VariableType.Integer, variables);
Console.WriteLine(result5); // 9

var result6 = expressionEvaluator.Evaluate("@concat(add(variables(variable3), 4), ' ', variables(variable2))", VariableType.String, variables);
Console.WriteLine(result6); // 9 World

var result7 = expressionEvaluator.Evaluate("@substract(variables(variable3), variables(variable4))", VariableType.Integer, variables);
Console.WriteLine(result7); // 3

var result8 = expressionEvaluator.Evaluate("@substract(variables(variable4), variables(variable3))", VariableType.Integer, variables);
Console.WriteLine(result8); // -3

var result9 = expressionEvaluator.Evaluate("@multiply(variables(variable4), variables(variable3))", VariableType.Integer, variables);
Console.WriteLine(result9); // 10

var result11 = expressionEvaluator.Evaluate("@modulo(variables(variable3), variables(variable4))", VariableType.Integer, variables);
Console.WriteLine(result11); // 1

var result12 = expressionEvaluator.Evaluate("@toLower(Karol)", VariableType.String, variables);
Console.WriteLine(result12); // karol

var result13 = expressionEvaluator.Evaluate("@toUpper(Karol)", VariableType.String, variables);
Console.WriteLine(result13); // KAROL

var result14 = expressionEvaluator.Evaluate("@replace(Apple Pie, Apple, Lemon)", VariableType.String, variables);
Console.WriteLine(result14); // Lemon Pie


#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
var options = new JsonSerializerOptions
{
    Converters = { new StepJsonConverter() },
    WriteIndented = true
};
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

var testAutomat = new Automat
{
    Name = "Manual trigger flow",
    Trigger = new ManualTrigger(),
    Steps = new List<IStep> { httpGetStep, firstNameVariable, lastNameVariable, fullnameVariable, emptyVariable, setValueVariable }
};

//var serializedAutomat = JsonSerializer.Serialize(testAutomat, options);

//var deserializedAutomat = JsonSerializer.Deserialize<Automat>(InputTest.Input, options);

var automatProcessor = serviceProvider.GetRequiredService<IAutomatProcessor>();

await automatProcessor.RunAsync(testAutomat);

//await automatProcessor.RunAsync(deserializedAutomat!);

Console.ReadKey();

void RegisterFunctions(IServiceCollection services)
{
    var functionType = typeof(IFunction);
    var implementations = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => functionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    foreach (var implementation in implementations)
    {
        services.AddTransient(functionType, implementation);
    }
}

void RegisterStepHandlers(IServiceCollection services)
{
    var stepHandlerType = typeof(IStepHandler);
    var implementations = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => stepHandlerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
    foreach (var implementation in implementations)
    {
        services.AddTransient(stepHandlerType, implementation);
    }
}
