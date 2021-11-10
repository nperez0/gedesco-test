using Core.Events;
using Core.Extensions;
using System;

namespace Domain.Events
{
    public class UserDeletedEvent : IEvent
    {
        public Guid UserId { get; }

        private UserDeletedEvent(Guid userId)
        {
            UserId = userId;
        }

        public static UserDeletedEvent Create(Guid userId)
        {
            userId.ThrowIfEmpty(nameof(userId));

            return new UserDeletedEvent(userId);
        }
    }
}
