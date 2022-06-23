namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
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

        public async Task<TaskDTO> CreateAsync(CreateTaskDTO taskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            task.ProjectName = await _projectService.GetNameAsync(task.ProjectId, cancellationToken);
            await _repository.InsertOneAsync(task, cancellationToken);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetOnDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _repository.GetOnDateAsync(date, cancellationToken);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO taskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            await _repository.UpdateAsync(task, cancellationToken);
            return taskDto;
        }

        public Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ApproveAsync(date, projectId, employeeId, cancellationToken);
        }

        public async Task<List<TaskDTO>> BulkCreateAsync(BulkCreateTaskDTO bulkCreateTaskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var projectName = await _projectService.GetNameAsync(bulkCreateTaskDto.Task.ProjectId, cancellationToken);
            var newTasks = bulkCreateTaskDto.Dates.Select(date =>
            {
                var task = _mapper.Map<Data.Models.Task>(bulkCreateTaskDto.Task);
                task.Date = date;
                task.ProjectName = projectName;
                return task;
            }).ToList();

            await _repository.BulkCreateAsync(newTasks, cancellationToken);

            return _mapper.Map<List<TaskDTO>>(newTasks);
        }
    }
}