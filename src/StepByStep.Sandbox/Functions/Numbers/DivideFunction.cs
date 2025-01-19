using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStep.Sandbox.Functions.Numbers
{
    internal sealed class DivideFunction : IFunction<double>
    {
        public string Name => "divide";

        public double Evaluate(string[] args)
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

            return firstNumber / secondNumber;
        }
    }
}
