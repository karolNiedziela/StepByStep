namespace StepByStep.Sandbox.Steps
{
    internal interface IStepDeserializer
    {
        IStep DeserializeStep(string json);
    }
}
