using Core.Events;
using Core.Extensions;
using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public class UserUpdatedEvent : IEvent
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<Address> Addresses { get; private set; }

        private UserUpdatedEvent() { }

        private UserUpdatedEvent(Guid userId, string name, IReadOnlyCollection<Address> addresses)
        {
            UserId = userId;
            Name = name;
            Addresses = addresses;
        }

        public static UserUpdatedEvent Create(Guid userId, string name, IReadOnlyCollection<Address> addresses)
        {
            userId.ThrowIfEmpty(nameof(userId));
            addresses.ThrowIfEmpty(nameof(addresses));

            return new UserUpdatedEvent(userId, name, addresses);
        }
    }
}
