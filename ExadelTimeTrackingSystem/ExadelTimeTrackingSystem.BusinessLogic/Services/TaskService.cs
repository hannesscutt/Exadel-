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

        public async Task<List<TaskDTO>> GetAllAsync(Guid employeeId, CancellationToken cancellationToken)
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

        public async Task<List<TaskDTO>> GetOnDateAsync(DateTime date, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _repository.GetOnDateAsync(date, employeeId, cancellationToken);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<TaskDTO> UpdateAsync(UpdateTaskDTO updateTaskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var task = _mapper.Map<Data.Models.Task>(updateTaskDto);
            task.ProjectName = await _projectService.GetNameAsync(task.ProjectId, cancellationToken);
            await _repository.UpdateAsync(task, cancellationToken);
            return _mapper.Map<TaskDTO>(task);
        }

        public Task ApproveAsync(ApproveTaskDTO approveTaskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ApproveAsync(approveTaskDto.Date, approveTaskDto.ProjectId, approveTaskDto.EmployeeId, cancellationToken);
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

        public async Task<List<string>> GetHours(GetHoursDTO hoursDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<string> stringList = new List<string>();
            var array = await _repository.GetHours(hoursDto.Date, hoursDto.EmployeeId, cancellationToken);
            stringList.Add("Sunday: " + array[0]);
            stringList.Add("Monday: " + array[1]);
            stringList.Add("Tuesday: " + array[2]);
            stringList.Add("Wednesday: " + array[3]);
            stringList.Add("Thursday: " + array[4]);
            stringList.Add("Friday: " + array[5]);
            stringList.Add("Saturday: " + array[6]);
            stringList.Add("Total: " + array[7]);
            return stringList;
        }
    }
}