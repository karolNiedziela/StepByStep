namespace StepByStep.Core.Steps.Variables.SetVariable
{
    public interface ISetVariableValueStep : IStep
    {
#pragma warning disable CA1716 // Identifiers should not match keywords
        Variable For { get; }
#pragma warning restore CA1716 // Identifiers should not match keywords

        Variable Value { get; }
    }
}
