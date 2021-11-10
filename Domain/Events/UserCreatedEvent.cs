using Core.Events;
using Core.Extensions;
using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public class UserCreatedEvent : IEvent
    {
        public Guid UserId { get; }

        public string Username { get; }

        public string Password { get; }

        public string Name { get; }

        public IReadOnlyCollection<Address> Addresses { get; }

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
