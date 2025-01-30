using System.Globalization;

namespace StepByStep.Sandbox.Functions.Numbers
{
    internal sealed class MultiplyFunction : IFunction
    {
        public string Name => "multiply";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Exactly two arguments are required.");
            }

            if (!int.TryParse(args[0], out var firstNumber) || !int.TryParse(args[1], out var secondNumber))
            {
                throw new ArgumentException("Both arguments must be valid integers.");
            }

            var result = firstNumber * secondNumber;
            return new FunctionResult(result.ToString(CultureInfo.InvariantCulture), typeof(int));
        }
    }
}
