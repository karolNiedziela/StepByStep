using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using StepByStep.Core.Repositories;
using StepByStep.Infrastructure.Persistence.Repositories;

namespace StepByStep.Infrastructure.Persistence
{
    internal static class Extensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // Register MongoDB class maps
            MongoDbConfigurator.RegisterClassMaps();

            var objectSerializer = new ObjectSerializer(ObjectSerializer.AllAllowedTypes);
            BsonSerializer.RegisterSerializer(objectSerializer);

            services.AddSingleton<IAutomatRepository, AutomatRepository>();

            return services;
        }
    }
}
