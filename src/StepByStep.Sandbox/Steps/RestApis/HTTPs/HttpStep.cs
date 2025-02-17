
namespace StepByStep.Sandbox.Steps.RestApis.HTTPs
{
    internal sealed class HttpStep : IHttpStep
    {
        public string DisplayName { get; set; } = null!;

        public string TypeName => GetType().Name;

        public string AssemblyQualifiedName => GetType().AssemblyQualifiedName!;

        public required string Url { get; set; }

        public required HttpMethod Method { get; set; }

        public string Body { get; set; } = null!;

        public Dictionary<string, string> Headers => [];
    }
}
