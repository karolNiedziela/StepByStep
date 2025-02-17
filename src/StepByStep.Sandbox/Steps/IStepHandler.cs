using StepByStep.Sandbox.ExpressionEvaluators;

namespace StepByStep.Sandbox.Steps
{
    internal interface IStepHandler
    {
        Task Handle(IStep step, Automat automat, IExpressionEvaluator expressionEvaluator);
    }
}
