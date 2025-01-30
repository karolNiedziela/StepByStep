namespace StepByStep.Sandbox.Functions
{
    internal interface IFunction
    {
        string Name { get; }

        FunctionResult Evaluate(string[] args);
    }
}
