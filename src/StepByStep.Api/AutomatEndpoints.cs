using StepByStep.Core;
using StepByStep.Core.Repositories;
using StepByStep.Sandbox;
using System.Text.Json;

namespace StepByStep.Api
{
    internal static class AutomatEndpoints
    {
        public static void AddAutomatEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/automats", async (IAutomatRepository repository) =>
            {
                var automat = await repository.GetAsync("67b256e3ed68b95a4053629e");

                return Results.Ok();
            });

            app.MapPost("/automats", async (IAutomatRepository repository) =>
            {
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
                var options = new JsonSerializerOptions
                {
                    Converters = { new StepJsonConverter() },
                    WriteIndented = true
                };
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

                var automat = JsonSerializer.Deserialize<Automat>(InputTest.Input, options)!;

                await repository.CreateAsync(automat);

                return Results.Ok();
            });
        }
    }
}
