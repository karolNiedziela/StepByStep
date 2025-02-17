using Microsoft.Extensions.DependencyInjection;
using StepByStep.Core.Functions;
using StepByStep.Core.Steps;
using System.Reflection;

namespace StepByStep.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            RegisterFunctions(services);
            RegisterStepHandlers(services);

            return services;
        }

        private static void RegisterFunctions(IServiceCollection services)
        {
            var functionType = typeof(IFunction);
            var implementations = Assembly.GetExecutingAssembly()
                                          .GetTypes()
                                          .Where(t => functionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var implementation in implementations)
            {
                services.AddTransient(functionType, implementation);
            }
        }

        private static void RegisterStepHandlers(IServiceCollection services)
        {
            var stepHandlerType = typeof(IStepHandler);
            var implementations = Assembly.GetExecutingAssembly()
                                          .GetTypes()
                                          .Where(t => stepHandlerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var implementation in implementations)
            {
                services.AddTransient(stepHandlerType, implementation);
            }
        }

    }
}
