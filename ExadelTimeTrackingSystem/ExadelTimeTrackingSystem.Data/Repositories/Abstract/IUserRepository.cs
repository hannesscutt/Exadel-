namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;

    public interface IUserRepository : IMongoRepository<Models.User>
    {
        Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken);

        Task<string> GetEmailAsync(Guid id, CancellationToken cancellationToken);

        Task<List<string>> WeeklyApproverEmailAsync(CancellationToken cancellationToken);
    }
}
