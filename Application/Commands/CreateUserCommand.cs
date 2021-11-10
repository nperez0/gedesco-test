using Core.Commands;
using Core.Extensions;
using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;

namespace Application.Commands
{
    public class CreateUserCommand : ICommand
    {
        public Guid UserId { get; }

        public string Username { get; }

        public string Password { get; }

        public string Name { get; }

        public IReadOnlyCollection<Address> Address { get; }

        private CreateUserCommand(Guid userId, string username, string password, string name, IReadOnlyCollection<Address> addresses)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Name = name;
            Address = addresses;
        }

        public static CreateUserCommand Create(Guid userId, string username, string password, string name, IReadOnlyCollection<Address> addresses)
        {
            userId.ThrowIfEmpty(nameof(userId));
            username.ThrowIfNullOrWhiteSpace(nameof(username));
            password.ThrowIfNullOrWhiteSpace(nameof(password));
            addresses.ThrowIfNull(nameof(addresses));

            return new CreateUserCommand(userId, username, password, name, addresses);
        }
    }
}
