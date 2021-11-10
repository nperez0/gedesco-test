using Application.Models;
using Core.Queries;
using Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetail>
    {
        private readonly IModelRepository<UserDetail> _userDetailRepository;

        public GetUserByIdQueryHandler(IModelRepository<UserDetail> userDetailRepository)
        {
            _userDetailRepository = userDetailRepository ?? throw new ArgumentNullException(nameof(userDetailRepository));
        }

        public Task<UserDetail> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            => _userDetailRepository.Find(request.UserId, cancellationToken);
    }
}
