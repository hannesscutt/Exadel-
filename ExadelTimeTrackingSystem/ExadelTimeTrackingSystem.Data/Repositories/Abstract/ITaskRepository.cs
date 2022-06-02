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

        System.Threading.Tasks.Task DeleteTaskAsync(Guid id);

        System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task);

        System.Threading.Tasks.Task ApproveTasksAsync(DateTime date, Guid projectId, Guid employeeId);

        System.Threading.Tasks.Task BulkCreateTasksDTOAsync(List<Models.Task> tasks);
    }
}
