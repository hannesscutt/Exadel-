namespace ExadelTimeTrackingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using MongoDB.Driver;

    public class TaskRepository : MongoRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(IMongoDbSettings settings)
            : base(settings)
        {
        }

        public Task<List<Models.Task>> GetOnDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(d => d.Date, date.Date);
            return GetCollection<Models.Task>().Find(filter).ToListAsync();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var filter = filterBuilder.Eq(t => t.Id, id);
            return GetCollection<Models.Task>().DeleteOneAsync(filter);
        }

        public Task ApproveAsync(DateTime date, Guid projectId, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var updateBuilder = Builders<Models.Task>.Update;
            var dateFilter = filterBuilder.Eq(t => t.Date, date);
            var projectFilter = filterBuilder.Eq(t => t.ProjectId, projectId);
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var updateDefinition = updateBuilder.Set(t => t.Status, Models.Enums.Status.Approved);
            return GetCollection<Models.Task>().UpdateManyAsync(dateFilter & projectFilter & employeeFilter, updateDefinition);
        }

        public Task BulkCreateAsync(List<Models.Task> tasks, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetCollection<Models.Task>().InsertManyAsync(tasks);
        }

        public Task<List<Models.Task>> EmailApproverAsync(List<string> approverNames, List<string> approverEmails, string employeeName, Guid employeeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filterBuilder = Builders<Models.Task>.Filter;
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, employeeId);
            var statusFilter = filterBuilder.Eq(t => t.Status, Models.Enums.Status.WaitingForApproval);

            return GetCollection<Models.Task>().Find(/*statusFilter &*/ employeeFilter).SortBy(t => t.ApproverId).ToListAsync();
        }

        public Task<List<Guid>> GetApproversAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filterBuilder = Builders<Models.Task>.Filter;
            var employeeFilter = filterBuilder.Eq(t => t.EmployeeId, id);
            var statusFilter = filterBuilder.Eq(t => t.Status, Models.Enums.Status.WaitingForApproval);

            return GetCollection<Models.Task>().Find(employeeFilter /*& statusFilter*/).SortBy(t => t.ApproverId).Project(t => t.ApproverId).ToListAsync();
        }
    }
}