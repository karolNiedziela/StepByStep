using StepByStep.Core.Steps.Variables.InitializeVariable;
using StepByStep.Core.Steps.Variables.SetVariable;
using System.Reflection;
using System.Text.Json;

namespace StepByStep.Core.Steps
{
    internal sealed class StepResolver : IStepResolver
    {
        public IStep Resolve(IStep step)
        {
            var json = JsonSerializer.Serialize<object>(step);
            var jsonDocument = JsonDocument.Parse(json);
            var typeProperty = jsonDocument.RootElement.GetProperty("TypeName").GetString();

            if (typeProperty == null)
            {
                throw new NotSupportedException("Step type is not specified.");
            }

            var stepType = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .FirstOrDefault(t => typeof(IStep).IsAssignableFrom(t) && t.Name == typeProperty);

            if (stepType == null)
            {
                throw new NotSupportedException($"Step type '{typeProperty}' is not supported.");
            }

            return (IStep)JsonSerializer.Deserialize(json, stepType)!;
        }
    }
}
