namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public abstract class MongoRepository<TDocument> : IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(settings.ConnectionString).DatabaseName);
        }

        public Task<List<TDocument>> GetAllAsync()
        {
            var filterBuilder = Builders<TDocument>.Filter;
            var filter = filterBuilder.Empty;
            return GetCollection<TDocument>().Find(filter).ToListAsync();
        }

        public Task<TDocument> GetByIdAsync(Guid id)
        {
            var filterBuilder = Builders<TDocument>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, id);
            return GetCollection<TDocument>().Find(filter).SingleOrDefaultAsync();
        }

        public Task InsertOneAsync(TDocument document)
        {
            return GetCollection<TDocument>().InsertOneAsync(document);
        }

        public async Task<bool> ExistsAsync(List<Guid> idList)
        {
            var filterBuilder = Builders<TDocument>.Filter;
            var emptyFilter = filterBuilder.Empty;
            var count = await GetCollection<TDocument>().CountAsync(emptyFilter);
            if (count < idList.Count)
            {
                return false;
            }

            foreach (var id in idList)
            {
                var idFilter = filterBuilder.Eq(d => d.Id, id);
                if (await GetCollection<TDocument>().CountAsync(idFilter) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        protected IMongoCollection<TDocument> GetCollection<TDocument2>()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument2).Name);
        }
    }
}
