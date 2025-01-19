namespace StepByStep.Sandbox
{
    internal sealed class Variable
    {
        public string Name { get; private set; }

        public VariableType Type { get; private set; }

        public string? Value { get; private set; }

        public Variable(string name, VariableType type, string? value = null)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public void SetValue(string? value) => Value = value;
    }
}
