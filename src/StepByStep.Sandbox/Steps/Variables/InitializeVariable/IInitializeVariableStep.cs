namespace StepByStep.Sandbox.Steps.Variables.InitializeVariable
{
    internal interface IInitializeVariableStep : IStep
    {
        Variable? Variable { get; }
    }
}
