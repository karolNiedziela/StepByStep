namespace StepByStep.Sandbox.Functions.Strings
{
    internal sealed class ReplaceFunction : IFunction
    {
        public string Name => "replace";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Exactly three arguments are required.");
            }

            string input = args[0];
            string oldValue = args[1];
            string newValue = args[2];

            return new FunctionResult(input.Replace(oldValue, newValue, StringComparison.Ordinal), typeof(string));
        }
    }
}
