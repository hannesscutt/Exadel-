namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllAsync();

        Task<ProjectDTO> GetByIdAsync(Guid id);

        Task<ProjectDTO> CreateAsync(CreateProjectDTO project);

        Task<List<string>> GetNamesAsync();

        Task<List<string>> GetActivitiesAsync(Guid id);

        Task<string> GetNameAsync(Guid id);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken token);

        Task<bool> ExistsAsync(Guid id, CancellationToken token);

        Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto);
    }
}
