using StepByStep.Sandbox.ExpressionEvaluators;

namespace StepByStep.Sandbox.ExpressionEvaluators
{
    internal interface IExpressionEvaluator
    {
        ExpressionResult Evaluate(string expression, VariableType returnVariableType = VariableType.String, List<Variable>? variables = null);
    }
}
