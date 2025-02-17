using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StepByStep.SharedResources.Persistence.Options;

namespace StepByStep.SharedResources.Persistence
{
    internal static class Extensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.SectionName));

            return services;
        }
    }
}
