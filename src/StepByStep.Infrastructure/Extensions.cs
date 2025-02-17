using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StepByStep.Infrastructure.Persistence;

namespace StepByStep.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddPersistence();

            return services;
        }
    }
}
