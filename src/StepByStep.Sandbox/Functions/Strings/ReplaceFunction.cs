namespace StepByStep.Sandbox.Functions.Strings
{
    internal sealed class ReplaceFunction : IFunction<string>
    {
        public string Name => "replace";

        public string Evaluate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Exactly three arguments are required.");
            }

            string input = args[0];
            string oldValue = args[1];
            string newValue = args[2];

            return input.Replace(oldValue, newValue, StringComparison.Ordinal);
        }
    }
}
