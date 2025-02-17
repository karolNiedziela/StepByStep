using StepByStep.Sandbox.Steps;

namespace StepByStep.Sandbox
{
    internal sealed class Automat
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required List<IStep> Steps { get; set; } = [];

        public List<Variable> Variables { get; set; } = [];
    }
}
