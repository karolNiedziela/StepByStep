using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StepByStep.Core;
using StepByStep.Core.Repositories;
using StepByStep.SharedResources.Persistence.Options;

namespace StepByStep.Infrastructure.Persistence.Repositories
{
    internal sealed class AutomatRepository : IAutomatRepository
    {
        private readonly IMongoCollection<Automat> _automatsCollection;

        public AutomatRepository(IMongoClient mongoClient, IOptions<MongoDbOptions> mongoDbOptions)
        {
            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbOptions.Value.DatabaseName);

            _automatsCollection = mongoDatabase.GetCollection<Automat>(
                mongoDbOptions.Value.AutomatsCollectionName);
        }

        public async Task<Automat?> GetAsync(string id) =>
            await _automatsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Automat automat) =>
            await _automatsCollection.InsertOneAsync(automat);
    }
}
