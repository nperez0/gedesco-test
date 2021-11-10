using Core.Events;
using Core.Extensions;
using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public class UserCreatedEvent : IEvent
    {
        public Guid UserId { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<Address> Addresses { get; private set; }

        private UserCreatedEvent() { }
        private UserCreatedEvent(Guid userId, string username, string password, string name, IReadOnlyCollection<Address> addresses)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Name = name;
            Addresses = addresses;
        }

        public static UserCreatedEvent Create(Guid userId, string username, string password, string name, IReadOnlyCollection<Address> addresses)
        {
            userId.ThrowIfEmpty(nameof(userId));
            username.ThrowIfNullOrWhiteSpace(nameof(username));
            password.ThrowIfNullOrWhiteSpace(nameof(password));
            addresses.ThrowIfEmpty(nameof(addresses));

            return new UserCreatedEvent(userId, username, password, name, addresses);
        }
    }
}
