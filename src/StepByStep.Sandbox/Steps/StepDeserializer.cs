using StepByStep.Sandbox.Steps.Variables.InitializeVariable;
using StepByStep.Sandbox.Steps.Variables.SetVariable;
using System.Text.Json;

namespace StepByStep.Sandbox.Steps
{
    internal sealed class StepDeserializer : IStepDeserializer
    {
        public IStep DeserializeStep(string json)
        {
            var jsonDocument = JsonDocument.Parse(json);
            var typeProperty = jsonDocument.RootElement.GetProperty("TypeName").GetString();

            return typeProperty switch
            {
                nameof(InitializeVariableStep) => JsonSerializer.Deserialize<InitializeVariableStep>(json)!,
                nameof(SetVariableValueStep) => JsonSerializer.Deserialize<SetVariableValueStep>(json)!,
                _ => throw new NotSupportedException($"Step type '{typeProperty}' is not supported.")
            };
        }
    }
}
