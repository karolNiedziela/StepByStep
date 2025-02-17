using StepByStep.Core.ExpressionEvaluators;

namespace StepByStep.Core.Steps
{
    internal interface IStepHandler
    {
        Task Handle(IStep step, Automat automat, IExpressionEvaluator expressionEvaluator);
    }
}
