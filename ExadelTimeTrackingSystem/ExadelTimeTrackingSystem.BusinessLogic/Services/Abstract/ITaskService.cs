namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllAsync(CancellationToken token);

        Task<TaskDTO> GetByIdAsync(Guid id, CancellationToken token);

        Task<TaskDTO> CreateAsync(CreateTaskDTO task, CancellationToken token);

        Task<List<TaskDTO>> GetOnDateAsync(DateTime date, CancellationToken token);

        Task DeleteAsync(Guid id, CancellationToken token);

        Task<TaskDTO> UpdateAsync(TaskDTO task, CancellationToken token);

        Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken token);

        Task<List<TaskDTO>> BulkCreateAsync(BulkCreateTaskDTO bulkCreateTaskDto, CancellationToken token);
    }
}