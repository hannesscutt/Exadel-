// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public Task CreateAsync(CreateProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
