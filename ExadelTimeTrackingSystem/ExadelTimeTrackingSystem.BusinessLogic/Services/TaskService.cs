namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Extensions;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

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

        public System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            return _repository.DeleteTaskAsync(id);
        }

        public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            await _repository.UpdateTaskAsync(task);
            return taskDto;
        }

        public System.Threading.Tasks.Task ApproveTasksAsync(DateTime date, Guid projectId, Guid employeeId)
        {
            return _repository.ApproveTasksAsync(date, projectId, employeeId);
        }

        public async Task<List<CreateTaskDTO>> BulkCreateTasksDTOAsync(BulkTaskDTO bulkTask)
        {
            var list = new List<CreateTaskDTO>();
            /*
            var newTasks = tasks.Dates.Select(d => {
                tasks.Task.Date = d;
            });
            */
            foreach (var date in bulkTask.Dates)
            {
                bulkTask.Task.Date = date;
                list.Add(CopyExtension.Copy(bulkTask.Task));
            }

            var tasks = _mapper.Map<List<Data.Models.Task>>(list);
            foreach (var task in tasks)
            {
                task.ProjectName = _projectService.GetProjectNameAsync(task.ProjectId).Result;
            }

            await _repository.BulkCreateTasksDTOAsync(tasks);
            return list;
        }
    }
}