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
            var filter = filterBuilder.Eq(d => d.DateTime, date.Date);
            return GetCollection<Models.Task>().Find(filter).ToListAsync();
        }

        public Task<DeleteResult> DeleteTaskAsync(Guid id)
        {
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Id, id);
            var db = GetCollection<Models.Task>();
            return db.DeleteOneAsync(filter);
        }
    }
}
