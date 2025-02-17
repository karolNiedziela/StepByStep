using System.Globalization;

namespace StepByStep.Core.Functions.Strings
{
    internal sealed class SubstringFunction : IFunction
    {
        public string Name => "substring";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Substring function requires 3 arguments.");
            }

            var str = args[0];
            var startIndex = int.Parse(args[1], CultureInfo.InvariantCulture);
            var length = int.Parse(args[2], CultureInfo.InvariantCulture);

            return new FunctionResult(str.Substring(startIndex, length), typeof(string));
        }
    }
}
