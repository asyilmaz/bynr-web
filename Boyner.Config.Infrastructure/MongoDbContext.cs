using Boyner.Config.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Boyner.Config.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Boyner.Config.Domain.Config> Configs
        {
            get
            {
                return _database.GetCollection<Boyner.Config.Domain.Config>("Config");
            }
        }
    }
}
