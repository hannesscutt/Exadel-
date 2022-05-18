// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
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

        Task CreateAsync(CreateProjectDTO project);
    }
}
