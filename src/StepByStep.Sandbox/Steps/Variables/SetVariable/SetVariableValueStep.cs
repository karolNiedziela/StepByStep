using StepByStep.Sandbox.Steps.Variables.InitializeVariable;

namespace StepByStep.Sandbox.Steps.Variables.SetVariable
{
    internal sealed class SetVariableValueStep : ISetVariableValueStep
    {
        public string DisplayName { get; private set; }

        public string TypeName => GetType().Name;

        public string AssemblyQualifiedName => GetType().AssemblyQualifiedName!;

        public Variable For { get; private set; }

        public Variable Value  { get; private set; }

        public SetVariableValueStep(string displayName, Variable @for, Variable value)
        {
            DisplayName = displayName;
            For = @for;
            Value = value;
        }
    }
}
