using Application.Models;
using Core.Extensions;
using Core.Queries;
using Core.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserDetail>>
    {
        private readonly IModelRepository<UserDetail> _userDetailRepository;

        public GetUsersQueryHandler(IModelRepository<UserDetail> userDetailRepository)
        {
            _userDetailRepository = userDetailRepository ?? throw new ArgumentNullException(nameof(userDetailRepository));
        }

        public Task<PagedList<UserDetail>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            var query = _userDetailRepository
                .Query()
                .Where(x => request.Keyword.IsNullOrWhiteSpace() ||
                    x.Username.Contains(request.Keyword) ||
                    x.Name.Contains(request.Keyword));

            var total = query.Count();
            var users = query
                .Skip(skip)
                .Take(request.PageSize)
                .ToArray();

            var result = new PagedList<UserDetail>()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = total,
                Items = users
            };

            return Task.FromResult(result);
        }
    }
}
