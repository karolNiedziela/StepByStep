namespace StepByStep.SharedResources.Persistence.Options
{
    public sealed class MongoDbOptions
    {
        public const string SectionName = "MongoDb";

        public string DatabaseName { get; set; } = null!;

        public string AutomatsCollectionName { get; set; } = null!;
    }
}
