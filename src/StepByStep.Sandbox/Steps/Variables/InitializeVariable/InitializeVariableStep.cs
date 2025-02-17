using System.Text.Json.Serialization;

namespace StepByStep.Sandbox.Steps.Variables.InitializeVariable
{
    internal sealed class InitializeVariableStep : IInitializeVariableStep
    {
        public string DisplayName { get; private set; }

        public string TypeName => GetType().Name;

        public string AssemblyQualifiedName => GetType().AssemblyQualifiedName!;

        public Variable Variable { get; private set; }

        public InitializeVariableStep(string displayName, Variable variable)
        {
            DisplayName = displayName;
            Variable = variable;
        }
    }
}
