namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

        public TaskService(ITaskRepository repository, IMapper mapper, IProjectService projectService)
        {
            _repository = repository;
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<TaskDTO> CreateAsync(CreateTaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            task.ProjectName = _projectService.GetNameAsync(task.ProjectId).Result;
            await _repository.InsertOneAsync(task);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetOnDateAsync(DateTime date)
        {
            var tasks = await _repository.GetOnDateAsync(date);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public Task DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            await _repository.UpdateAsync(task);
            return taskDto;
        }

        public Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId)
        {
            return _repository.ApproveAsync(date, projectId, employeeId);
        }

        public async Task<List<CreateTaskDTO>> BulkCreateAsync(BulkCreateTaskDTO bulkCreateTaskDto)
        {
            var list = new List<CreateTaskDTO>();
            var projectName = await _projectService.GetNameAsync(bulkCreateTaskDto.Task.ProjectId);
            var newTasks = bulkCreateTaskDto.Dates.Select(date =>
            {
                var task = _mapper.Map<Data.Models.Task>(bulkCreateTaskDto.Task);
                task.Date = date;
                task.ProjectName = projectName;
                return task;
            });

            await _repository.BulkCreateAsync(new List<Data.Models.Task>(newTasks));
            return list;
        }
    }
}