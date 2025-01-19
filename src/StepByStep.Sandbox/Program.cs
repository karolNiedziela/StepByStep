using Microsoft.Extensions.DependencyInjection;
using StepByStep.Sandbox;
using StepByStep.Sandbox.Functions;
using System.Reflection;

var serviceCollection = new ServiceCollection();
RegisterFunctions(serviceCollection);
RegisterFunctionsForType<string>(serviceCollection);
RegisterFunctionsForType<int>(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

var stringFunctions = serviceProvider.GetServices<IFunction<string>>();
var integerFunctions = serviceProvider.GetServices<IFunction<int>>();

var allFunctions = serviceProvider.GetServices<IFunction>();

var stringEvaluator = new ExpressionEvaluator<string>(allFunctions);
var integerEvaluator = new ExpressionEvaluator<int>(allFunctions);
var doubleEvaluator = new ExpressionEvaluator<double>(allFunctions);

var variables = new List<Variable>()
{
    new("StringVariable", VariableType.String,  "concat(variable1, ' ', variable2)"),
    new("variable1", VariableType.String, "Hello"),
    new("variable2", VariableType.String, "World"),
    new("variable3", VariableType.Integer, "5"),
    new("variable4", VariableType.Integer, "2"),
};

var result1 = stringEvaluator.Evaluate("@concat(variable1, ' ', variable2)", variables);
Console.WriteLine(result1); // Hello World

var result2 = stringEvaluator.Evaluate("@concat(concat(variable1, ' '), ' ', variable2)", variables);
Console.WriteLine(result2); // Hello World

var result3 = stringEvaluator.Evaluate("@substring(variable1, 0, 4)", variables);
Console.WriteLine(result3); // Hell

var result4 = stringEvaluator.Evaluate("substring");
Console.WriteLine(result4); // substring

var result5 = integerEvaluator.Evaluate("@add(variable3, 4)", variables);
Console.WriteLine(result5); // 9

var result6 = stringEvaluator.Evaluate("@concat(add(variable3, 4), ' ', variable2)", variables);
Console.WriteLine(result6); // 9 World

var result7 = integerEvaluator.Evaluate("@substract(variable3, variable4)", variables);
Console.WriteLine(result7); // 3

var result8 = integerEvaluator.Evaluate("@substract(variable4, variable3)", variables);
Console.WriteLine(result8); // -3

var result9 = integerEvaluator.Evaluate("@multiply(variable4, variable3)", variables);
Console.WriteLine(result9); // 10

var result10 = doubleEvaluator.Evaluate("@divide(variable3, variable4)", variables);
Console.WriteLine(result10); // 2.5

var result11 = integerEvaluator.Evaluate("@modulo(variable3, variable4)", variables);
Console.WriteLine(result11); // 1

var result12 = stringEvaluator.Evaluate("@toLower(Karol)", variables);
Console.WriteLine(result12); // karol

var result13 = stringEvaluator.Evaluate("@toUpper(Karol)", variables);
Console.WriteLine(result13); // KAROL

var result14 = stringEvaluator.Evaluate("@replace(Apple Pie, Apple, Lemon)", variables);
Console.WriteLine(result14); // LemonPie

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

void RegisterFunctionsForType<T>(IServiceCollection services)
{
    var functionType = typeof(IFunction<T>);
    var implementations = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => functionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    foreach (var implementation in implementations)
    {
        services.AddTransient(functionType, implementation);
    }
}