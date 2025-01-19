using System.Globalization;

namespace StepByStep.Sandbox.Functions.Strings
{
    internal sealed class SubstringFunction : IFunction<string>
    {
        public string Name => "substring";

        public string Evaluate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Substring function requires 3 arguments.");
            }

            var str = args[0];
            var startIndex = int.Parse(args[1], CultureInfo.InvariantCulture);
            var length = int.Parse(args[2], CultureInfo.InvariantCulture);

            return str.Substring(startIndex, length);
        }
    }
}
