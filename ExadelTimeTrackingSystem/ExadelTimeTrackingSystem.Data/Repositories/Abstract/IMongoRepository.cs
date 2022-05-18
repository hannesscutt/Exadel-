﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;

    public interface IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        Task<List<TDocument>> GetAllAsync();

        Task<TDocument> GetByIdAsync(Guid id);

        Task InsertOneAsync(TDocument document);
    }
}
