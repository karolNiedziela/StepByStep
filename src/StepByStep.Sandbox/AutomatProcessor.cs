using StepByStep.Sandbox.ExpressionEvaluators;
using StepByStep.Sandbox.Steps;
using StepByStep.Sandbox.Steps.Variables;
using System.Text.Json;

namespace StepByStep.Sandbox
{
    internal sealed class AutomatProcessor : IAutomatProcessor
    {
        private readonly IExpressionEvaluator _expressionEvaluator;

        public AutomatProcessor(IExpressionEvaluator expressionEvaluator)
        {
            _expressionEvaluator = expressionEvaluator;
        }

        public Task RunAsync(Automat automat)
        {
            foreach (var step in automat.Steps)
            {
                var stepType = step.GetType();

                var test = step as InitializeVariable;
                if (test?.Variable?.Value == null)
                {
                    continue;
                }

                // TODO: Handle deserializing specific step to specific class {"Name":"Initialize firstname","Type":"InitializeVariable","Variable":{"Name":"First Name","VariableType":2,"Value":"Karol"}}
                var json = JsonSerializer.Serialize(test);

                var deserializedStep = DeserializeStep(json);

                var result = _expressionEvaluator.Evaluate(test.Variable.Value, VariableType.String, automat.Variables);
            }

            return Task.CompletedTask;
        }

        private IStep DeserializeStep(string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var typeProperty = jsonDocument.RootElement.GetProperty("TypeName").GetString();

            return typeProperty switch
            {
                "InitializeVariable" => JsonSerializer.Deserialize<InitializeVariable>(json)!,
                _ => throw new NotSupportedException($"Step type '{typeProperty}' is not supported.")
            };
        }
    }
 }
