namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<TaskDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<TaskDTO> CreateAsync(CreateTaskDTO task, CancellationToken cancellationToken);

        Task<List<TaskDTO>> GetOnDateAsync(DateTime date, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<TaskDTO> UpdateAsync(UpdateTaskDTO task, CancellationToken cancellationToken);

        Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken cancellationToken);

        Task<List<TaskDTO>> BulkCreateAsync(BulkCreateTaskDTO bulkCreateTaskDto, CancellationToken cancellationToken);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Message>> EmailApproverAsync(List<string> approverNames, List<string> approverEmails, string employeeName, Guid employeeId, CancellationToken cancellationToken);

        Task<List<Guid>> GetApproversAsync(Guid id, CancellationToken cancellationToken);
    }
}