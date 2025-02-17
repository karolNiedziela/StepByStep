namespace StepByStep.Sandbox.Triggers
{
    internal sealed class ManualTrigger : ITrigger
    {
        public string DisplayName => "Manual";

        public string TypeName =>  GetType().Name;

        public string AssemblyQualifiedName => GetType().AssemblyQualifiedName!;
    }
}
