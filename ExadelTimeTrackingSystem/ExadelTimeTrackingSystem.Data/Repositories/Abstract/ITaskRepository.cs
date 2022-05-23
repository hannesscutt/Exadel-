namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using ExadelTimeTrackingSystem.Data.Models;

    public interface ITaskRepository : IMongoRepository<Task>
    {
    }
}
