using StepByStep.Sandbox.Steps;

namespace StepByStep.Sandbox
{
    internal sealed class Automat
    {
        public List<IStep> Steps { get; set; } = [];

        public List<Variable> Variables { get; set; } = [];
    }
}
