using Application.Models;
using Core.Extensions;
using Core.Queries;
using System;

namespace Application.Queries
{
    public class GetUserByIdQuery : IQuery<UserDetail>
    {
        public Guid UserId { get; set; }

        private GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public static GetUserByIdQuery Create(Guid userId)
        {
            userId.ThrowIfEmpty(nameof(userId));

            return new GetUserByIdQuery(userId);
        }
    }
}
