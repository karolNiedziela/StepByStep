using StepByStep.Sandbox.ExpressionEvaluators;
using StepByStep.Sandbox.Steps;
using StepByStep.Sandbox.Steps.Variables.InitializeVariable;
using StepByStep.Sandbox.Steps.Variables.SetVariable;
using System.Text.Json;

namespace StepByStep.Sandbox
{
    internal sealed class AutomatProcessor : IAutomatProcessor
    {
        private readonly IExpressionEvaluator _expressionEvaluator;
        private readonly IStepDeserializer _stepDeserializer;
        private readonly Dictionary<string, IStepHandler> _stepHandlers;

        public AutomatProcessor(IExpressionEvaluator expressionEvaluator, IStepDeserializer stepDeserializer)
        {
            _expressionEvaluator = expressionEvaluator;
            _stepDeserializer = stepDeserializer;
            _stepHandlers = new Dictionary<string, IStepHandler>
            {
                { nameof(InitializeVariableStep), new InitializeVariableStepHandler() },
                { nameof(SetVariableValueStep), new SetVariableStepHandler() }
            };
        }

        public Task RunAsync(Automat automat)
        {
            foreach (var step in automat.Steps)
            {
                var json = JsonSerializer.Serialize<object>(step);

                var deserializedStep = _stepDeserializer.DeserializeStep(json);

                if (!_stepHandlers.TryGetValue(deserializedStep.TypeName, out var handler))
                {
                    throw new NotSupportedException($"Step type '{deserializedStep.TypeName}' is not supported.");
                }

                handler.Handle(deserializedStep, automat, _expressionEvaluator);
            }

            return Task.CompletedTask;
        }
    }
 }
