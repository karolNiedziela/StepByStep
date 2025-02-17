using StepByStep.Sandbox.Steps;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StepByStep.Sandbox
{
    internal sealed class StepJsonConverter : JsonConverter<IStep>
    {
        public override IStep Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDocument.RootElement;
                var typeName = jsonObject.GetProperty("AssemblyQualifiedName").GetString();

                var type = Type.GetType(typeName!);
                if (type == null)
                {
                    throw new NotSupportedException($"Step type '{typeName}' is not supported.");
                }

                var step = JsonSerializer.Deserialize(jsonObject.GetRawText(), type, options) as IStep;
                if (step == null)
                {
                    throw new JsonException($"Deserialization of type '{typeName}' failed.");
                }

                return step;
            }
        }

        public override void Write(Utf8JsonWriter writer, IStep value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
}
