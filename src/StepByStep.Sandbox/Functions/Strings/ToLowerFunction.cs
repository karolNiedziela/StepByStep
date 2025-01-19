namespace StepByStep.Sandbox.Functions.Strings
{
    internal sealed class ToLowerFunction : IFunction<string>
    {
        public string Name => "toLower";

        public string Evaluate(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Exactly one argument is required.");
            }

            return args[0].ToLowerInvariant();
        }
    }
}
