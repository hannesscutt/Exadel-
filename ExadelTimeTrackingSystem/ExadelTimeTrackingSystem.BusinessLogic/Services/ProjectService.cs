namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var project = _mapper.Map<Project>(projectDto);
            await _repository.InsertOneAsync(project, cancellationToken);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<List<ProjectDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var projects = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var project = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<ProjectDTO>(project);
        }

        public Task<List<string>> GetNamesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetNamesAsync(cancellationToken);
        }

        public Task<List<string>> GetActivitiesAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetActivitiesAsync(id, cancellationToken);
        }

        public Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetNameAsync(id, cancellationToken);
        }

        public Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ExistAsync(ids, cancellationToken);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ExistsAsync(id, cancellationToken);
        }

        public async Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var project = _mapper.Map<Project>(projectDto);
            await _repository.UpdateAsync(project, cancellationToken);
            return projectDto;
        }

        public Task<bool> ActivityExistsAsync(string activity, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ActivityExistsAsync(id, activity, cancellationToken);
        }
    }
}
