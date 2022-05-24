namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllAsync();

        Task<ProjectDTO> GetByIdAsync(Guid id);

        Task<ProjectDTO> CreateAsync(CreateProjectDTO project);

        Task<List<string>> GetNamesAsync();

        Task<string[]> GetProjectActivitiesAsync(Guid id);
    }
}
