// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
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
