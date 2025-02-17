using MongoDB.Bson.Serialization;
using StepByStep.Core.Steps.Variables.InitializeVariable;
using StepByStep.Core.Steps.Variables.SetVariable;

namespace StepByStep.Infrastructure.Persistence
{
    public static class MongoDbConfigurator
    {
        public static void RegisterClassMaps()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(InitializeVariableStep)))
            {
                BsonClassMap.RegisterClassMap<InitializeVariableStep>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("InitializeVariableStep");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(SetVariableValueStep)))
            {
                BsonClassMap.RegisterClassMap<SetVariableValueStep>(cm =>
                {
                    cm.AutoMap();
                    cm.SetDiscriminator("SetVariableValueStep");
                });
            }

        }
    }
}
