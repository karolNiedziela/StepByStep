using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StepByStep.Core.Steps;

namespace StepByStep.Core
{
    public sealed class Automat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("Name")]
        public required string Name { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Steps")]
        public List<IStep> Steps { get; set; } = [];

        [BsonIgnore]
        public List<Variable> Variables { get; private set; } = [];
    }
}
