namespace StepByStep.Sandbox.Functions.Integers
{
    internal sealed class SubstractFunction : IFunction<int>
    {
        public string Name => "substract";

        public int Evaluate(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Exactly two arguments are required.");
            }

            if (!int.TryParse(args[0], out var firstNumber) || !int.TryParse(args[1], out var secondNumber))
            {
                throw new ArgumentException("Both arguments must be valid integers.");
            }

            return firstNumber - secondNumber;
        }
    }
}
