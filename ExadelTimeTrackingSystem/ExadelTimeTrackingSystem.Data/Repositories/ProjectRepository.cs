namespace ExadelTimeTrackingSystem.Data.Repositories
{
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
    }
}
