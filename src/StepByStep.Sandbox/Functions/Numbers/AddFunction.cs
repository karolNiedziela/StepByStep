namespace StepByStep.Sandbox.Functions.Numbers
{
    internal sealed class AddFunction : IFunction<int>
    {
        public string Name => "add";

        public int Evaluate(string[] args)
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

            return sum;
        }
    }
}
