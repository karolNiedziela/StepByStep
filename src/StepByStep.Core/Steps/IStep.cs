namespace StepByStep.Core.Steps
{
    public interface IStep
    {
        string DisplayName { get; }

        string TypeName { get; }

        string AssemblyQualifiedName { get; }
    }
}
