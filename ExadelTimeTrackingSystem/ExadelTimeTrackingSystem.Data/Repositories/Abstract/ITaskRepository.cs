namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaskRepository : IMongoRepository<Models.Task>
    {
        Task<List<Models.Task>> GetOnDateAsync(DateTime date);

        Task DeleteAsync(Guid id);

        Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId);

        Task BulkCreateAsync(List<Models.Task> tasks);
    }
}