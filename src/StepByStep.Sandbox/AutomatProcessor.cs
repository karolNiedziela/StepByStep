using StepByStep.Sandbox.ExpressionEvaluators;
using StepByStep.Sandbox.Steps;
using System.Text.Json;

namespace StepByStep.Sandbox
{
    internal sealed class AutomatProcessor : IAutomatProcessor
    {
        private readonly IExpressionEvaluator _expressionEvaluator;
        private readonly IStepResolver _stepResolver;
        private readonly IEnumerable<IStepHandler> _stepHandlers;

        public AutomatProcessor(IExpressionEvaluator expressionEvaluator, IStepResolver stepResolver, IEnumerable<IStepHandler> stepHandlers)
        {
            _expressionEvaluator = expressionEvaluator;
            _stepResolver = stepResolver;
            _stepHandlers = stepHandlers;
        }

        public async Task RunAsync(Automat automat)
        {
            foreach (var step in automat.Steps)
            {
                var json = JsonSerializer.Serialize<object>(step);

                var resolvedStep = _stepResolver.Resolve(step);

                Console.WriteLine($"Executing step: {resolvedStep.DisplayName}");

                var handler = _stepHandlers.FirstOrDefault(x => x.GetType().Name == $"{resolvedStep.TypeName}Handler");
                if (handler is null)
                {
                    throw new NotSupportedException($"Step type '{resolvedStep.TypeName}' is not supported.");
                }

                await handler.Handle(resolvedStep, automat, _expressionEvaluator);
            }

            return;
        }
    }
 }
