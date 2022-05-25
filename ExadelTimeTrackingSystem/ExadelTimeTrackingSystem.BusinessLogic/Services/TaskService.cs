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
    using MongoDB.Driver;

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TaskDTO> CreateAsync(CreateTaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
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

        public Task<DeleteResult> DeleteTaskAsync(Guid id)
        {
            return _repository.DeleteTaskAsync(id);
        }

        public async Task<UpdateResult> UpdateTaskAsync(CreateTaskDTO taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            return await _repository.UpdateTaskAsync(task);
        }
    }
}