namespace StepByStep.Sandbox.Steps.RestApis.HTTPs
{
    internal interface IHttpStep : IStep
    {
        string Url { get; }

        HttpMethod Method { get; }

        string Body { get; }

        public Dictionary<string, string> Headers { get; }
    }
}
