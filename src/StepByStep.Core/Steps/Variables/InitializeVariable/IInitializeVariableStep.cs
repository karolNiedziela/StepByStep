namespace StepByStep.Core.Steps.Variables.InitializeVariable
{
    public interface IInitializeVariableStep : IStep
    {
        Variable? Variable { get; }
    }
}
