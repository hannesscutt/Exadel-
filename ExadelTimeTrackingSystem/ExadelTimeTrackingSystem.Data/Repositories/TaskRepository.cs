namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class TaskRepository : MongoRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<Models.Task>> GetOnDateAsync(DateTime date)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Date, date.Date);
            return GetCollection<Models.Task>().Find(filter).ToListAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(t => t.Id, id);
            return GetCollection<Models.Task>().DeleteOneAsync(filter);
        }

        public Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var updateBuilder = Builders<Models.Task>.Update;
            var dateFilter = filterBuilder.Eq(t => t.Date, date);
            var projectFilter = filterBuilder.Eq(t => t.ProjectId, projectId);
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var updateDefinition = updateBuilder.Set(t => t.Status, Models.Enums.Status.Approved);
            return GetCollection<Models.Task>().UpdateManyAsync(dateFilter & projectFilter & employeeFilter, updateDefinition);
        }

        public Task BulkCreateAsync(List<Models.Task> tasks)
        {
            return GetCollection<Models.Task>().InsertManyAsync(tasks);
        }
    }
}