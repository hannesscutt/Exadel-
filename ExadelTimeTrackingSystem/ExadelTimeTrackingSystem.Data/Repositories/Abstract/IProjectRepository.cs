namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;

    public interface IProjectRepository : IMongoRepository<Project>
    {
        Task<List<string>> GetNamesAsync(CancellationToken cancellationToken);

        Task<List<string>> GetActivitiesAsync(Guid id, CancellationToken cancellationToken);

        Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken);
    }
}
