namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllAsync();

        Task<TaskDTO> GetByIdAsync(Guid id);

        Task<TaskDTO> CreateAsync(CreateTaskDTO task);
    }
}
