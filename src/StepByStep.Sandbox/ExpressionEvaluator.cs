using StepByStep.Sandbox.Functions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace StepByStep.Sandbox
{
    internal sealed class ExpressionEvaluator<T>
    {
        private readonly Dictionary<string, IFunction> _functions;

        public ExpressionEvaluator(IEnumerable<IFunction> functions)
        {
            _functions = functions.ToDictionary(f => f.Name, f => f);
        }

        public T Evaluate(string expression, List<Variable>? variables = null)
        {
            if (!expression.StartsWith('@'))
            {
                return ConvertExpression(expression);
            }

            expression = expression.Substring(1);

            if (variables?.Count == 0)
            {
                throw new ArgumentException($"No variables provided.");
            }

            var extractedVariables = ExtractVariables(expression);
            foreach (var variable in extractedVariables)
            {
                var variableValue = variables?.FirstOrDefault(v => v.Name == variable)?.Value;
                if (variableValue != null)
                {
                    expression = expression.Replace(variable, variableValue, StringComparison.Ordinal);
                }
            }

            return EvaluateExpression(expression, variables!);
        }

        private List<string> ExtractVariables(string expression)
        {
            var matches = Regex.Matches(expression, @"\b\w+\b|'[^']*'");
            return matches.Select(m => m.Value).Distinct().ToList();
        }

        private T EvaluateExpression(string expression, List<Variable> variables)
        {
            while (true)
            {
                var match = Regex.Match(expression, @"(\w+)\(([^()]*)\)");
                if (!match.Success)
                {
                    break;
                }

                var functionName = match.Groups[1].Value;
                var arguments = match.Groups[2].Value.Split(',');

                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = arguments[i].Trim();
                }

                if (_functions.TryGetValue(functionName, out var function))
                {
                    var result = EvaluateFunction(function, arguments, variables);
                    expression = expression.Replace(match.Value, result?.ToString() ?? string.Empty, StringComparison.Ordinal);
                }
                else
                {
                    throw new ArgumentException($"Function '{functionName}' is not supported.");
                }
            }

            return ConvertExpression(expression);
        }

        private object EvaluateFunction(IFunction function, string[] arguments, List<Variable> variables)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i].StartsWith('\'') && arguments[i].EndsWith('\''))
                {
                    arguments[i] = arguments[i][1..^1];
                }
                else
                {
                    var variable = variables?.FirstOrDefault(v => v.Name == arguments[i]);
                    if (variable != null && variable.Value != null)
                    {
                        arguments[i] = variable.Value;
                    }
                }
            }

            return EvaluateFunctionByType(function, arguments);
        }

        private object EvaluateFunctionByType(IFunction function, string[] arguments)
            => function switch
            {
                IFunction<string> stringFunction => stringFunction.Evaluate(arguments),
                IFunction<int> intFunction => intFunction.Evaluate(arguments),
                IFunction<double> doubleFunction => doubleFunction.Evaluate(arguments),
                _ => throw new ArgumentException("Unsupported function type.")
            };
        private T ConvertExpression(string expression)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T)) ?? throw new InvalidOperationException("No converter found.");

            var convertedValue = converter.ConvertFromString(expression) ?? throw new InvalidOperationException("Conversion failed.");

            return (T)convertedValue;
        }
    }
}
