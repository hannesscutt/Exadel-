namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class UserRepository : MongoRepository<Models.User>, IUserRepository
    {
        public UserRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(u => u.Id, id);
            return GetCollection<User>().Find(filter).Project(u => u.FullName).SingleOrDefaultAsync();
        }

        public Task<string> GetEmailAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(u => u.Id, id);
            return GetCollection<User>().Find(filter).Project(u => u.Email).SingleOrDefaultAsync();
        }

        public Task<List<string>> WeeklyApproverEmailAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.AnyEq(u => u.Roles, Models.Enums.Role.Approver);
            return GetCollection<User>().Find(filter).Project(u => u.Email).ToListAsync();
        }
    }
}