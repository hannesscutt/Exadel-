namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Extensions;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class TaskRepository : MongoRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<Models.Task>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            return GetCollection<Models.Task>().Find(filter).ToListAsync();
        }

        public Task<List<Models.Task>> GetOnDateAsync(DateTime date, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var dateFilter = filterBuilder.Eq(d => d.Date, date.Date);
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            return GetCollection<Models.Task>().Find(dateFilter & employeeFilter).ToListAsync();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(t => t.Id, id);
            return GetCollection<Models.Task>().DeleteOneAsync(filter);
        }

        public Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var updateBuilder = Builders<Models.Task>.Update;
            var dateFilter = filterBuilder.Eq(t => t.Date, date);
            var projectFilter = filterBuilder.Eq(t => t.ProjectId, projectId);
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var updateDefinition = updateBuilder.Set(t => t.Status, Models.Enums.Status.Approved);
            return GetCollection<Models.Task>().UpdateManyAsync(dateFilter & projectFilter & employeeFilter, updateDefinition);
        }

        public Task BulkCreateAsync(List<Models.Task> tasks, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetCollection<Models.Task>().InsertManyAsync(tasks);
        }

        public Task<List<string>> GetHours(DateTime date, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<DateTime> dateList = new List<DateTime>();
            List<string> testList = new List<string>();
            int total = 0;

            for (int i = 0; i < 7; i++)
            {
                dateList.Add(date);
                date = date.AddDays(1);
            }

            var filterBuilder = Builders<Models.Task>.Filter;
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var datefilter = filterBuilder.In(t => t.Date, dateList);

            var hours = GetCollection<Models.Task>().Aggregate().Match(datefilter & employeeFilter).Group(
                t => t.Date,
                group => new
                {
                    date = group.Key,
                    Total = group.Sum(t => t.HoursSpent),
                }).ToList();

            foreach (var hour in hours)
            {
                total += hour.Total;
                testList.Add(hour.date.DayOfWeek + ": " + hour.Total);
            }

            testList.Add("Total: " + total);
            return Task.FromResult(testList);
        }
    }
}