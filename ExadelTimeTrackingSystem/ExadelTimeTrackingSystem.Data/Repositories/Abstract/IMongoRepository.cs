namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;

    public interface IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        Task<List<TDocument>> GetAllAsync();

        Task<TDocument> GetByIdAsync(Guid id);

        Task InsertOneAsync(TDocument document);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken token);

        Task<bool> ExistsAsync(Guid id, CancellationToken token);
    }
}
