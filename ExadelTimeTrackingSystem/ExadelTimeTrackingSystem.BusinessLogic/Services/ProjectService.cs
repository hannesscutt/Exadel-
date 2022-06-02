namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
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

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _repository.InsertOneAsync(project);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetByIdAsync(Guid id)
        {
            var project = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public Task<List<string>> GetNamesAsync()
        {
            return _repository.GetNamesAsync();
        }

        public Task<List<string>> GetActivitiesAsync(Guid id)
        {
            return _repository.GetActivitiesAsync(id);
        }

        public Task<string> GetProjectNameAsync(Guid id)
        {
            return _repository.GetProjectNameAsync(id);
        }
    }
}
