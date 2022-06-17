namespace ExadelTimeTrackingSystem.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> ExistAsync(List<Guid> ids, CancellationToken token)
        {
            return _repository.ExistAsync(ids, token);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return _repository.ExistsAsync(id, token);
        }
    }
}
