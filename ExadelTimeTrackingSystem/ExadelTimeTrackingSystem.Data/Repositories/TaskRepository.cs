namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class TaskRepository : MongoRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<Models.Task>> GetTasksOnDateAsync(DateTime date)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Date, date.Date);
            return GetCollection<Models.Task>().Find(filter).ToListAsync();
        }

        public System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, id);
            return GetCollection<Models.Task>().DeleteOneAsync(filter);
        }

        public System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, task.Id);
            return GetCollection<Models.Task>().ReplaceOneAsync(filter, task);
        }

        public System.Threading.Tasks.Task ApproveTasksAsync(DateTime date, Guid projectId, Guid employeeId)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var updateBuilder = Builders<Models.Task>.Update;
            var dateFilter = filterBuilder.Eq(d => d.Date, date);
            var projectFilter = filterBuilder.Eq(d => d.ProjectId, projectId);
            var employeeFilter = filterBuilder.Eq(d => d.EmployeeId, employeeId);
            var updateFilter = updateBuilder.Set(d => d.Status, Models.Enums.Status.Approved);
            return GetCollection<Models.Task>().UpdateManyAsync(dateFilter & projectFilter & employeeFilter, updateFilter);
        }

        public System.Threading.Tasks.Task BulkCreateTasksDTOAsync(List<Models.Task> tasks)
        {
           return GetCollection<Models.Task>().InsertManyAsync(tasks);
        }
    }
}
