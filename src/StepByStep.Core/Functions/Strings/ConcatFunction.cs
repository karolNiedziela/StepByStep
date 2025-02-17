namespace StepByStep.Core.Functions.Strings
{
    internal sealed class ConcatFunction : IFunction
    {
        public string Name => "concat";

        public FunctionResult Evaluate(string[] args) => new(string.Join("", args), typeof(string));
    }
}
