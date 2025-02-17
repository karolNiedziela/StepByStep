using StepByStep.Core.Steps.Variables.InitializeVariable;
using StepByStep.Core.Steps.Variables.SetVariable;
using System.Text.Json;

namespace StepByStep.Core.Steps
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
