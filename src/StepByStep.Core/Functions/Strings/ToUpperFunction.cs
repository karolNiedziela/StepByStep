namespace StepByStep.Core.Functions.Strings
{
    internal sealed class ToUpperFunction : IFunction
    {
        public string Name => "toUpper";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Exactly one argument is required.");
            }

            return new FunctionResult(args[0].ToUpperInvariant(), typeof(string));
        }
    }
}
