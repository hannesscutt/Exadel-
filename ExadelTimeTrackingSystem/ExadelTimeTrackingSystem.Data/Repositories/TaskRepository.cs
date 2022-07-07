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
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class TaskRepository : MongoRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<Models.Task>> GetAllForEmployeeAsync(Guid employeeId, CancellationToken cancellationToken)
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

        public async Task<Dictionary<DateTime, int>> GetHoursByDatesAsync(DateTime date, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var startDate = date;
            var endDate = date.DeepCopy().AddDays(6);

            var filterBuilder = Builders<Models.Task>.Filter;
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var startDateFilter = filterBuilder.Gte(t => t.Date, startDate);
            var endDateFilter = filterBuilder.Lte(t => t.Date, endDate);

            var hours = await GetCollection<Models.Task>().Aggregate().Match(endDateFilter & startDateFilter & employeeFilter).Group(
                t => t.Date,
                group => new
                {
                    Date = group.Key,
                    Total = group.Sum(t => t.HoursSpent),
                }).SortBy(t => t.Date).ToListAsync();
/*
            GetCollection<Models.Task>().Aggregate(

            new BsonArray
{
    new BsonDocument("$match",
    new BsonDocument()),
    new BsonDocument("$match",
    new BsonDocument("Date",
    new BsonDocument
            {
                { "$gte",
    new DateTime(2022, 6, 26, 0, 0, 0) },
                { "$lte",
    new DateTime(2022, 7, 2, 0, 0, 0) }
            })),
    new BsonDocument("$group",
    new BsonDocument
        {
            { "_id", "$Date" },
            { "total",
    new BsonDocument("$sum", "$HoursSpent") }
        }),
    new BsonDocument("$project",
    new BsonDocument("doc",
    new BsonDocument
            {
                { "_id", "$_id" },
                { "subtotal", "$total" }
            })),
    new BsonDocument("$group",
    new BsonDocument
        {
            { "_id", BsonNull.Value },
            { "total",
    new BsonDocument("$sum", "$doc.subtotal") },
            { "result",
    new BsonDocument("$push", "$doc") }
        }),
    new BsonDocument("$project",
    new BsonDocument
        {
            { "result", 1 },
            { "_id", 0 },
            { "total", 1 }
        })
}
);
*/


            var hourDictionary = hours.ToDictionary(t => t.Date, t => t.Total);
            return hourDictionary;
        }
    }
}