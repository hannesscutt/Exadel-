namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface IUserService
    {
        Task<bool> ExistAsync(List<Guid> ids, CancellationToken token);

        Task<bool> ExistsAsync(Guid id, CancellationToken token);

        Task<string> GetNameAsync(Guid id, CancellationToken cancellationToken);

        Task<string> GetEmailAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Message>> WeeklyApproverEmailAsync(CancellationToken cancellationToken);
    }
}
