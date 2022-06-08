namespace ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public interface IUserService
    {
        Task<bool> ListExistsAsync(List<Guid> ids);

        Task<bool> ExistsAsync(Guid id);
    }
}
