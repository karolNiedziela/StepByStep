namespace StepByStep.Sandbox.Functions
{
    internal interface IFunction<out T> : IFunction
    {
        T Evaluate(string[] args);
    }
}
