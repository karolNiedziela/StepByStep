using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StepByStep.SharedResources.Persistence;

namespace StepByStep.SharedResources
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedResources(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            return services;
        }
    }
}
