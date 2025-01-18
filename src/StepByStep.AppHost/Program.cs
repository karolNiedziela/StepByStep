var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StepByStep_Api>("stepbystep-api");

await builder.Build().RunAsync();
