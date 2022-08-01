namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> ExistAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ExistAsync(ids, cancellationToken);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.ExistsAsync(id, cancellationToken);
        }

        public Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetNameAsync(id, cancellationToken);
        }

        public Task<string> GetEmailAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _repository.GetEmailAsync(id, cancellationToken);
        }

        public async Task<List<Message>> WeeklyApproverEmailAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<Message> messages = new List<Message>();
            var emailList = await _repository.WeeklyApproverEmailAsync(cancellationToken);
            foreach (var email in emailList)
            {
                messages.Add(new Message(new string[] { email }, "Weekly Approver Reminder", "Please remember to submit your approval tables today. Thank you");
            }

            return messages;
        }
    }
}
