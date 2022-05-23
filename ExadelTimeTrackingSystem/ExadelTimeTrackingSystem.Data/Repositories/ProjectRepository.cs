namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        private readonly IMongoDatabase _database;

        public ProjectRepository(IMongoDbSettings settings)
            : base(settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(settings.ConnectionString).DatabaseName);
        }

        public Task<List<string>> GetNamesAsync()
        {
            var filterBuilder = Builders<Project>.Filter;
            var list = new List<string>();
            var filter = filterBuilder.Empty;
            var collection = GetCollection<Project>().Find(filter).ToListAsync();
            foreach (var project in collection.Result)
            {
                list.Add(project.Name);
            }

            return System.Threading.Tasks.Task.FromResult(list);
        }

        private IMongoCollection<Project> GetCollection<TProject2>()
        {
            return _database.GetCollection<Project>(typeof(TProject2).Name);
        }
    }
}
