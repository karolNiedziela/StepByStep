namespace StepByStep.Core.Functions
{
    public interface IFunction
    {
        string Name { get; }

        FunctionResult Evaluate(string[] args);
    }
}
