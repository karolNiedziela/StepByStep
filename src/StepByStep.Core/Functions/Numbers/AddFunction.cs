using System.Globalization;

namespace StepByStep.Core.Functions.Numbers
{
    internal sealed class AddFunction : IFunction
    {
        public string Name => "add";

        public FunctionResult Evaluate(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Add function requires at least two arguments.");
            }

            int sum = 0;
            foreach (var arg in args)
            {
                if (int.TryParse(arg, out int number))
                {
                    sum += number;
                }
                else
                {
                    throw new ArgumentException($"Invalid argument '{arg}' for add function. All arguments must be integers.");
                }
            }

            return new FunctionResult(sum.ToString(CultureInfo.InvariantCulture), typeof(int));
        }
    }
}
