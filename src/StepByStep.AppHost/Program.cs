using Microsoft.Extensions.Configuration;
using StepByStep.SharedResources.Persistence.Options;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDbOptions = builder.Configuration.GetSection(MongoDbOptions.SectionName).Get<MongoDbOptions>()!;

var mongo = builder.AddMongoDB("mongo")
                   .WithContainerName("mongodb")
                   .WithMongoExpress()
                   .WithLifetime(ContainerLifetime.Persistent)
                   .WithDataVolume("mongodb_data");
var mongoDb = mongo.AddDatabase(mongoDbOptions.DatabaseName);

builder.AddProject<Projects.StepByStep_Api>("stepbystep-api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

await builder.Build().RunAsync();
