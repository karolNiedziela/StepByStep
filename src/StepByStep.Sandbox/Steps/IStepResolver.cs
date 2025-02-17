namespace StepByStep.Sandbox.Steps
{
    internal interface IStepResolver
    {
        IStep Resolve(IStep step);
    }
}
