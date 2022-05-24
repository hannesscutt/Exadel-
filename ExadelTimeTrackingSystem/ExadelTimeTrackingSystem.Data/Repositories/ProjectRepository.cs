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
        public ProjectRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<string>> GetNamesAsync()
        {
            var filterBuilder = Builders<Project>.Filter;
            var list = new List<string>();
            var filter = filterBuilder.Empty;
            return GetCollection<Project>().Find(filter).Project(p => p.Name).ToListAsync();
        }

        public Task<string[]> GetProjectActivitiesAsync(Guid id)
        {
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, id);
            return System.Threading.Tasks.Task.FromResult(GetCollection<Project>().Find(filter).Project(p => p.Activities).SingleOrDefault());
        }
    }
}
