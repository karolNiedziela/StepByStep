using System.Text.Json.Serialization;

namespace StepByStep.Sandbox.Steps.Variables
{
    internal sealed class InitializeVariable : IInitializeVariable
    {
        public string DisplayName { get; private set; }

        public string TypeName => nameof(InitializeVariable);

        public Variable? Variable { get; private set; }

        public InitializeVariable(string displayName, Variable? variable = null)
        {
            DisplayName = displayName;
            Variable = variable;
        }
    }
}
