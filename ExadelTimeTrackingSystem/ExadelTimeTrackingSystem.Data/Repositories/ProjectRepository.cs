namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }
    }
}
