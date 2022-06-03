namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaskRepository : IMongoRepository<Models.Task>
    {
        Task<List<Models.Task>> GetTasksOnDateAsync(DateTime date);

        Task DeleteTaskAsync(Guid id);

        Task UpdateTaskAsync(Models.Task task);

        Task ApproveTasksAsync(DateTime date, Guid projectId, Guid employeeId);

        Task BulkCreateTasksDTOAsync(List<Models.Task> tasks);
    }
}
