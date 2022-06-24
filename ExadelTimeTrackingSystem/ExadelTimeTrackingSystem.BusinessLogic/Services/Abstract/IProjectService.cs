namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<ProjectDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ProjectDTO> CreateAsync(CreateProjectDTO project, CancellationToken cancellationToken);

        Task<List<string>> GetNamesAsync(CancellationToken cancellationToken);

        Task<List<string>> GetActivitiesAsync(Guid id, CancellationToken cancellationToken);

        Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto, CancellationToken cancellationToken);
    }
}
