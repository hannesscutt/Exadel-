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
            task.ProjectName = _projectService.GetProjectNameAsync(task.ProjectId).Result;
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

        public async Task<List<TaskDTO>> GetTasksOnDateAsync(DateTime date)
        {
            var tasks = await _repository.GetTasksOnDateAsync(date);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public Task DeleteTaskAsync(Guid id)
        {
            return _repository.DeleteTaskAsync(id);
        }

        public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            await _repository.UpdateTaskAsync(task);
            return taskDto;
        }

        public Task ApproveTasksAsync(DateTime date, Guid projectId, Guid employeeId)
        {
            return _repository.ApproveTasksAsync(date, projectId, employeeId);
        }

        public async Task<List<CreateTaskDTO>> BulkCreateTasksDTOAsync(CreateBulkTaskDTO bulkTask)
        {
            var list = new List<CreateTaskDTO>();
            var projectName = await _projectService.GetProjectNameAsync(bulkTask.Task.ProjectId);
            var newTasks = bulkTask.Dates.Select(date =>
            {
                var task = _mapper.Map<Data.Models.Task>(bulkTask.Task);
                task.Date = date;
                task.ProjectName = projectName;
                return task;
            });

            await _repository.BulkCreateTasksDTOAsync(new List<Data.Models.Task>(newTasks));
            return list;
        }
    }
}