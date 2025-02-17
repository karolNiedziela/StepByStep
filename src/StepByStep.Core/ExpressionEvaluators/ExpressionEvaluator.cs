using StepByStep.Core.Functions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace StepByStep.Core.ExpressionEvaluators
{
    public sealed class ExpressionEvaluator : IExpressionEvaluator
    {
        private readonly Dictionary<string, IFunction> _functions;

        public ExpressionEvaluator(IEnumerable<IFunction> functions)
        {
            _functions = functions.ToDictionary(f => f.Name, f => f);
        }

        public ExpressionResult Evaluate(string expression, VariableType returnVariableType = VariableType.String, List<Variable>? variables = null)
        {
            if (!expression.StartsWith('@'))
            {
                return ConvertExpression(expression, returnVariableType);
            }

            expression = expression.Substring(1);

            if (variables?.Count == 0)
            {
                throw new ArgumentException($"No variables provided.");
            }

            var extractedVariables = ExtractVariables(expression);
            foreach (var variable in extractedVariables)
            {
                var variableName = variable.Substring(10, variable.Length - 11);
                var variableValue = variables?.FirstOrDefault(v => v.Name == variableName)?.Value;
                if (variableValue != null)
                {
                    expression = expression.Replace(variable, variableValue, StringComparison.Ordinal);
                }
            }

            return EvaluateExpression(expression, variables!, returnVariableType);
        }

        private List<string> ExtractVariables(string expression)
        {
            var matches = Regex.Matches(expression, @"variables\([^)]+\)");
            return matches.Select(m => m.Value).Distinct().ToList();
        }

        private ExpressionResult EvaluateExpression(string expression, List<Variable> variables, VariableType returnVariableType)
        {
            while (true)
            {
                var match = Regex.Match(expression, @"(\w+)\(([^()]*)\)");
                if (!match.Success)
                {
                    break;
                }

                var functionName = match.Groups[1].Value;
                var arguments = match.Groups[2].Value.Split(',').Select(x => x.Trim()).ToArray();

                if (_functions.TryGetValue(functionName, out var function))
                {
                    var result = EvaluateFunction(function, arguments, variables);
                    expression = expression.Replace(match.Value, result.Value ?? string.Empty, StringComparison.Ordinal);
                }
                else
                {
                    throw new ArgumentException($"Function '{functionName}' is not supported.");
                }
            }

            return ConvertExpression(expression, returnVariableType);
        }

        private FunctionResult EvaluateFunction(IFunction function, string[] arguments, List<Variable> variables)
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

            return function.Evaluate(arguments);
        }

        private ExpressionResult ConvertExpression(string expression, VariableType returnVariableType)
        {
            var type = GetType(returnVariableType);

            var converter = TypeDescriptor.GetConverter(type) ?? throw new InvalidOperationException("No converter found.");

            var convertedValue = converter.ConvertFromString(expression) ?? throw new InvalidOperationException("Conversion failed.");

            var castedValue = Convert.ChangeType(convertedValue, type, System.Globalization.CultureInfo.InvariantCulture)?.ToString();

            return new ExpressionResult(castedValue, type);
        }

        private Type GetType(VariableType variableType)
        {
            return variableType switch
            {
                VariableType.Boolean => typeof(bool),
                VariableType.Integer => typeof(int),
                VariableType.String => typeof(string),
                _ => throw new ArgumentOutOfRangeException(nameof(variableType), variableType, null),
            };
        }
    }
}
