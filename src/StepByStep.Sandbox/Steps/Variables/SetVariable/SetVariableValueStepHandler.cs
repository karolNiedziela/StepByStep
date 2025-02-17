using StepByStep.Sandbox.ExpressionEvaluators;

namespace StepByStep.Sandbox.Steps.Variables.SetVariable
{
    internal sealed class SetVariableValueStepHandler : IStepHandler
    {
        public Task Handle(IStep step, Automat automat, IExpressionEvaluator expressionEvaluator)
        {
            var setVariableValueStep = (SetVariableValueStep)step;
            var existingVariable = automat.Variables.Find(v => v.Name == setVariableValueStep.For.Name);

            if (existingVariable is null)
            {
                throw new ArgumentNullException($"Not found variable with name '{setVariableValueStep.DisplayName}'.");
            }

            if (existingVariable.VariableType != setVariableValueStep.Value.VariableType)
            {
                throw new ArgumentException($"Variable '{setVariableValueStep.DisplayName}' has type '{existingVariable.VariableType}' but expected '{setVariableValueStep.For.VariableType}'.");
            }

            if (string.IsNullOrEmpty(setVariableValueStep.Value.Value))
            {
                throw new ArgumentNullException($"Variable '{setVariableValueStep.DisplayName}' has no value.");
            }
                            
            existingVariable.SetValue(setVariableValueStep.Value.Value);

            Console.WriteLine($"Variable value set to {existingVariable.Value}.");

            return Task.CompletedTask;
        }
    }
}
