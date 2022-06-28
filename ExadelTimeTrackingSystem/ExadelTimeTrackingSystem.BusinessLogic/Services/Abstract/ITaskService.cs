namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllAsync(Guid employeeId, CancellationToken cancellationToken);

        Task<TaskDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<TaskDTO> CreateAsync(CreateTaskDTO task, CancellationToken cancellationToken);

        Task<List<TaskDTO>> GetOnDateAsync(DateTime date, Guid employeeId, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<TaskDTO> UpdateAsync(UpdateTaskDTO task, CancellationToken cancellationToken);

        Task ApproveAsync(ApproveTaskDTO approveTaskDto, CancellationToken cancellationToken);

        Task<List<TaskDTO>> BulkCreateAsync(BulkCreateTaskDTO bulkCreateTaskDto, CancellationToken cancellationToken);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<List<string>> GetHours(GetHoursDTO hoursDto, CancellationToken cancellationToken);
    }
}