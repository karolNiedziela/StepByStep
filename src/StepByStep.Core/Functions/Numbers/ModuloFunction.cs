using System.Globalization;

namespace StepByStep.Core.Functions.Numbers
{
    internal sealed class ModuloFunction : IFunction
    {
        public string Name => "modulo";

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

            if (secondNumber == 0)
            {
                throw new DivideByZeroException("The second argument must not be zero.");
            }

            var result = firstNumber % secondNumber;

            return new FunctionResult(result.ToString(CultureInfo.InvariantCulture), typeof(int));
        }
    }
}
