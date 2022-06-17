namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
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
    }
}