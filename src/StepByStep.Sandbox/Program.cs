using Microsoft.Extensions.DependencyInjection;
using StepByStep.Sandbox;
using StepByStep.Sandbox.ExpressionEvaluators;
using StepByStep.Sandbox.Functions;
using StepByStep.Sandbox.Steps.Variables;
using System.Reflection;

var serviceCollection = new ServiceCollection();
RegisterFunctions(serviceCollection);

serviceCollection.AddTransient<IExpressionEvaluator, ExpressionEvaluator>();

serviceCollection.AddTransient<IInitializeVariable, InitializeVariable>();
serviceCollection.AddTransient<IAutomatProcessor, AutomatProcessor>();


var serviceProvider = serviceCollection.BuildServiceProvider();

var allFunctions = serviceProvider.GetServices<IFunction>();

var expressionEvaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();

var firstNameVariable = new InitializeVariable("Initialize firstname", 
    new Variable("First Name", VariableType.String, "Karol"));
var lastNameVariable = new InitializeVariable("Initialize lastname", 
    new Variable("Last Name", VariableType.String, "Niedziela"));
var fullnameVariable = new InitializeVariable("Initialize fullname", 
    new Variable("Full name", VariableType.String, "@concat(First Name, ' ', Last Name)"));

var variables = new List<Variable>
{
    new("StringVariable", VariableType.String, "concat(variable1, ' ', variable2)"),
    new("variable1", VariableType.String, "Hello"),
    new("variable2", VariableType.String, "World"),
    new("variable3", VariableType.Integer, "5"),
    new("variable4", VariableType.Integer,  "2"),
    firstNameVariable.Variable!,
    lastNameVariable.Variable!,
    fullnameVariable.Variable!
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


var firstAutomat = new Automat();
firstAutomat.Steps.AddRange([firstNameVariable, lastNameVariable, fullnameVariable]);
firstAutomat.Variables.AddRange(variables);

var automatProcessor = serviceProvider.GetRequiredService<IAutomatProcessor>();
await automatProcessor.RunAsync(firstAutomat);

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