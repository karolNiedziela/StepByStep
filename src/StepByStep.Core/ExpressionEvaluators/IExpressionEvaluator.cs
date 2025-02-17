namespace StepByStep.Core.ExpressionEvaluators
{
    public interface IExpressionEvaluator
    {
        ExpressionResult Evaluate(string expression, VariableType returnVariableType = VariableType.String, List<Variable>? variables = null);
    }
}
