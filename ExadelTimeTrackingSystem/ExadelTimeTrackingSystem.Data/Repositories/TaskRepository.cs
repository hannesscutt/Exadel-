﻿namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Extensions;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
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

            var pipeline = new EmptyPipelineDefinition<Models.Task>();

            var group3 = pipeline.Group(
                new BsonDocument
                    {
                        { "_id", BsonNull.Value },
                        { "total",
                new BsonDocument("$sum", "$HoursSpent") }
                    });

            var group4 = pipeline.Group(
                new BsonDocument
                    {
                        { "_id", "$Date" },
                        { "total",
                new BsonDocument("$sum", "$HoursSpent") }
                    });



            var group1 = pipeline.Group(
                t => BsonNull.Value,
                group => new
                {
                    Total = group.Sum(t => t.HoursSpent),
                });

            var group2 = pipeline.Group(
                t => t.Date,
                group => new
                {
                    Date = group.Key,
                    TotalByDay = group.Sum(t => t.HoursSpent),
                });

            var group1Facet = AggregateFacet.Create("group1", group1);

            var group2Facet = AggregateFacet.Create("group2", group2);

            var hours = await GetCollection<Models.Task>().Aggregate().Match(endDateFilter & startDateFilter & employeeFilter)
                .Facet(group1Facet, group2Facet)
                .Unwind<DaysGrouping>("group1")
                .Project(
                x =>

                new BsonDocument
        {
            { "_id", 0 },
            { "group1facet", "$group1.total" },
            { "group2facet", "$group2" }
        }
                )
                //.SortBy(t => t.Date)
                .ToListAsync();

            //var test = await GetCollection<Models.Task>().Aggregate(pipeline).ToListAsync();

            //var hourDictionary = hours.ToDictionary(t => t.Date, t => t.Total);
            /*
            pipeline = pipeline.Group(
                t => t.Date,
                group => new
                {
                    Date = group.Key,
                    TotalByDate = group.Sum(t => t.HoursSpent),
                });
            */


            /*
            var group = AggregateFacet.Create<Models.Task, Models.Task>("test",
                (t => t.Date,
                group => new
                {
                    Date = group.Key,
                    TotalByDate = group.Sum(t => t.HoursSpent),
                }));

         var grouptest = BsonDocument.Create(
                (t => null,
                group => new
                {
                    Total = group.Sum(t => t.HoursSpent),
                }));

            var grouptest1 = new BsonDocument("$group",
                new BsonDocument
                    {
                        { "_id", BsonNull.Value },
                        { "total",
                new BsonDocument("$sum", "$HoursSpent") }
                    });

            var group1 = pipeline.Group(
                t => BsonNull.Value,
                group => new
                {
                    Total = group.Sum(t => t.HoursSpent),
                });

            var group2 = pipeline.Group(
                t => t.Date,
                group => new
                {
                    Date = group.Key,
                    TotalByDate = group.Sum(t => t.HoursSpent),
                });

            var finalPipeline = pipeline.AppendStage<Models.Task, Models.Task, Models.Task>(grouptest1);

            var group1Facet = AggregateFacet.Create("test", group1); 

            var group2Facet = AggregateFacet.Create("test", group2);

            var test = AggregateFacet.Create("test", finalPipeline);

            var projectionBuilder = Builders<Models.Task>.Projection;

            //var projection = projectionBuilder.Include();
            */
            //var hourDictionary = hours.ToDictionary(t => t.Date, t => t.TotalByDate);
            //var hourDictionary = hours.ToDictionary(t => t, t => t.);
            //return hourDictionary;
            return null;
        }

        private class DaysGrouping
        {
            public TestObject Group1 { get; set; }

            public TestObject[] Group2 { get; set; }
        }

        public class TestObject
        {
            public DateTime Date { get; set; }

            public int Total { get; set; }
        }
    }
}