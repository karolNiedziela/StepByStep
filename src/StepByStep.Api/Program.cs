using Microsoft.Extensions.Hosting;
using StepByStep.Api;
using StepByStep.Core;
using StepByStep.Infrastructure;
using StepByStep.SharedResources;
using StepByStep.SharedResources.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedResources(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddInfrastructure();

builder.AddServiceDefaults();

builder.AddMongoDBClient("StepByStepStore");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddAutomatEndpoints();

app.UseHttpsRedirection();

await app.RunAsync();
