namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;
    using MongoDB.Driver;

    public interface ITaskRepository : IMongoRepository<Models.Task>
    {
        Task<List<Models.Task>> GetTasksOnDateAsync(DateTime date);

        Task<DeleteResult> DeleteTaskAsync(Guid id);

        Task<UpdateResult> UpdateTaskAsync(Models.Task task);
    }
}
