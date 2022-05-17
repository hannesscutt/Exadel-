namespace ExadelTimeTrackingSystem.Data.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;

    public interface IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        Task <List<TDocument>> GetAllAsync();

        Task<TDocument> GetByIdAsync(Guid id);

        Task InsertOneAsync(TDocument document);
    }
}
