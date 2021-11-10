using Application.Models;
using Core.Queries;
using System;

namespace Application.Queries
{
    public class GetUsersQuery : IQuery<PagedList<UserDetail>>
    {
        public string Keyword { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        private GetUsersQuery(string keyword, int pageNumber, int pageSize)
        {
            Keyword = keyword;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static GetUsersQuery Create(string keyword, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize is <= 0 or > 100)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            return new GetUsersQuery(keyword, pageNumber, pageSize);
        }
    }
}
