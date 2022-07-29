namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using Mustache;

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

        public async Task<TaskDTO> UpdateAsync(UpdateTaskDTO updateTaskDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var task = _mapper.Map<Data.Models.Task>(updateTaskDto);
            task.ProjectName = await _projectService.GetNameAsync(task.ProjectId, cancellationToken);
            await _repository.UpdateAsync(task, cancellationToken);
            return _mapper.Map<TaskDTO>(task);
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

        public async Task<List<Message>> EmailApproverAsync(List<string> approverNames, List<string> approverEmails, string employeeName, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<Message> messages = new List<Message>();
            string currentRecepient = string.Empty;
            string currentApprover = string.Empty;
            string message = Constants.MustacheTemplates.EMAILTABLEHEADER;
            bool firstIteration = true;
            var taskList = await _repository.EmailApproverAsync(approverNames, approverEmails, employeeName, employeeId, cancellationToken);
            var zippedLists = approverNames.Zip(approverEmails).Zip(taskList);

            foreach (var entry in zippedLists)
            {
                if (firstIteration)
                {
                    currentApprover = entry.First.First;
                    currentRecepient = entry.First.Second;
                    firstIteration = false;
                }

                var data = new
                {
                    EmployeeName = employeeName,
                    Hours = entry.Second.HoursSpent,
                    Date = entry.Second.Date.ToLongDateString(),
                    ProjectName = entry.Second.ProjectName,
                };

                if (entry.First.Second == currentRecepient)
                {
                    message = message + Template.Compile(Constants.MustacheTemplates.EMAILTABLEBODY).Render(data);
                }
                else
                {
                    messages.Add(new Message(new string[] { currentRecepient }, "Hello " + currentApprover, message));
                    currentRecepient = entry.First.Second;
                    currentApprover = entry.First.First;
                    message = Constants.MustacheTemplates.EMAILTABLEHEADER + Template.Compile(Constants.MustacheTemplates.EMAILTABLEBODY).Render(data);
                }
            }

            messages.Add(new Message(new string[] { currentRecepient }, "Hello " + currentApprover, message));
            return messages;
        }

        public Task<List<Guid>> GetApproversAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetApproversAsync(id, cancellationToken);
        }
    }
}