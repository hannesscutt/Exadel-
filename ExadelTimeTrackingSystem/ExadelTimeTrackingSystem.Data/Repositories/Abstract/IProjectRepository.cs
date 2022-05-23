﻿namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models;

    public interface IProjectRepository : IMongoRepository<Project>
    {
        Task<List<string>> GetNamesAsync();
    }
}
