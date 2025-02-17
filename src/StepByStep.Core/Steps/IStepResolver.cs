namespace StepByStep.Core.Steps
{
    internal interface IStepResolver
    {
        IStep Resolve(IStep step);
    }
}
