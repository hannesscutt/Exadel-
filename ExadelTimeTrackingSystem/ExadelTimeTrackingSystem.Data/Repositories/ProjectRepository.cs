﻿namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<string>> GetNamesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Empty;
            return GetCollection<Project>().Find(filter).Project(p => p.Name).ToListAsync();
        }

        public Task<List<string>> GetActivitiesAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, id);
            return GetCollection<Project>().Find(filter).Project(p => p.Activities).SingleOrDefaultAsync();
        }

        public Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, id);
            return GetCollection<Project>().Find(filter).Project(p => p.Name).SingleOrDefaultAsync();
        }

        public async Task<bool> ActivityExistsAsync(Guid id, string activity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq(p => p.Id, id);
            var filterActivities = filterBuilder.AnyEq(p => p.Activities, activity);
            return await GetCollection<Project>().Find(filter & filterActivities).CountAsync() > 0;
        }
    }
}
