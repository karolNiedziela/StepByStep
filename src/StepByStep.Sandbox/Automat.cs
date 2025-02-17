using StepByStep.Sandbox.Steps;
using StepByStep.Sandbox.Triggers;

namespace StepByStep.Sandbox
{
    internal sealed class Automat
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required ITrigger Trigger { get; set; }

        public List<IStep> Steps { get; set; } = [];

        public List<Variable> Variables { get; set; } = [];
    }
}
