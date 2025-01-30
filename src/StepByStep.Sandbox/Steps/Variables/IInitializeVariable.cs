namespace StepByStep.Sandbox.Steps.Variables
{
    internal interface IInitializeVariable : IStep
    {
        Variable? Variable { get; }
    }
}
