namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITaskRepository : IMongoRepository<Models.Task>
    {
        Task<List<Models.Task>> GetOnDateAsync(DateTime date, Guid employeeId, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken cancellationToken);

        Task BulkCreateAsync(List<Models.Task> tasks, CancellationToken cancellationToken);

        Task<List<string>> GetHours(DateTime date, Guid employeeId, CancellationToken cancellationToken);

        Task<List<Models.Task>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);
    }
}