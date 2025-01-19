namespace StepByStep.Sandbox.Functions.Strings
{
    internal sealed class ConcatFunction : IFunction<string>
    {
        public string Name => "concat";

        public string Evaluate(string[] args) => string.Join("", args);
    }
}
