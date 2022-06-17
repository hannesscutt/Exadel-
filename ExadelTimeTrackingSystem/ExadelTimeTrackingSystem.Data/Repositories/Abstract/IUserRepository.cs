namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;

    public interface IUserRepository : IMongoRepository<Models.User>
    {
    }
}
