using System.Globalization;

namespace StepByStep.Sandbox.Functions.Numbers
{
    internal sealed class DivideFunction : IFunction
    {
        public string Name => "divide";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Exactly two arguments are required.");
            }

            if (!double.TryParse(args[0], out var firstNumber) || !double.TryParse(args[1], out var secondNumber))
            {
                throw new ArgumentException("Both arguments must be valid numbers.");
            }

            if (secondNumber == 0)
            {
                throw new DivideByZeroException("The second argument must not be zero.");
            }

            var result = firstNumber / secondNumber;

            return new FunctionResult(result.ToString(CultureInfo.InvariantCulture), typeof(double));
        }
    }
}
