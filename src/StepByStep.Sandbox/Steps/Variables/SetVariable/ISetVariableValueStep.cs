namespace StepByStep.Sandbox.Steps.Variables.SetVariable
{
    internal interface ISetVariableValueStep : IStep
    {
        Variable For { get; }

        Variable Value { get; }
    }
}
