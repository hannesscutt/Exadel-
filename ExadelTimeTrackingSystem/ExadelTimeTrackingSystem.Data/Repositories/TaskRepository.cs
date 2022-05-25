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

        public Task<DeleteResult> DeleteTaskAsync(Guid id)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, id);
            return GetCollection<Models.Task>().DeleteOneAsync(filter);
        }

        public Task<UpdateResult> UpdateTaskAsync(Models.Task task)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, task.Id);
            var updateFilter = Builders<Models.Task>.Update
                .Set(d => d, task);
                /*
                .Set(d => d.HoursSpent, task.HoursSpent)
                .Set(d => d.Status, task.Status)
                .Set(d => d.Activity, task.Activity)
                .Set(d => d.Description, task.Description)
                .Set(d => d.ApproverId, task.ApproverId)
                .Set(d => d.EmployeeId, task.EmployeeId)
                .Set(d => d.IsOvertime, task.IsOvertime)
                .Set(d => d.ProjectName, task.ProjectName);
                */
            return GetCollection<Models.Task>().UpdateOneAsync(filter, updateFilter);
        }
    }
}
