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
        Task<List<TDocument>> GetAllAsync(CancellationToken cancellationToken);

        Task<TDocument> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task InsertOneAsync(TDocument document, CancellationToken cancellationToken);

        Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task UpdateAsync(TDocument document, CancellationToken cancellationToken);
    }
}
