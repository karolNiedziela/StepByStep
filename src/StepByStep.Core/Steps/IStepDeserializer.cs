namespace StepByStep.Core.Steps
{
    internal interface IStepDeserializer
    {
        IStep DeserializeStep(string json);
    }
}
