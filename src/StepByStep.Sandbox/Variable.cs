using System.Text.Json.Serialization;

namespace StepByStep.Sandbox
{
    internal sealed class Variable
    {
        public string Name { get; private set; }

        public VariableType VariableType { get; private set; }

        [JsonIgnore]
        public Type Type { get; private set; }

        public string? Value { get; private set; }

        public Variable(string name, VariableType variableType, string? value = null)
        {
            Name = name;
            VariableType = variableType;
            Type = SetType(variableType);
            Value = value;
        }

        public void SetValue(string? value) => Value = value;

        private Type SetType(VariableType variableType)
        {
            return variableType switch
            {
                VariableType.Boolean => typeof(bool),
                VariableType.Integer => typeof(int),
                VariableType.String => typeof(string),
                _ => throw new ArgumentOutOfRangeException(nameof(variableType), variableType, null),
            };
        } 
    }
}
