namespace StepByStep.Sandbox.Triggers
{
    internal interface ITrigger
    {
        string DisplayName { get; }

        string TypeName { get; }

        string AssemblyQualifiedName { get; }
    }
}
