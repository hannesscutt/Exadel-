namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;

    public interface IProjectRepository : IMongoRepository<Project>
    {
        Task<List<string>> GetNamesAsync();

        Task<List<string>> GetActivitiesAsync(Guid id);

        Task<string> GetNameAsync(Guid id);

        System.Threading.Tasks.Task UpdateAsync(Project project);
    }
}
