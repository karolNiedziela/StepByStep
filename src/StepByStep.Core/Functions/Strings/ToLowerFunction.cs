namespace StepByStep.Core.Functions.Strings
{
    internal sealed class ToLowerFunction : IFunction
    {
        public string Name => "toLower";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Exactly one argument is required.");
            }

            return new FunctionResult(args[0].ToLowerInvariant(), typeof(string));
        }
    }
}
