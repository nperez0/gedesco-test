using Core.Commands;
using Core.Extensions;
using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;

namespace Application.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public Guid UserId { get; }

        public string Name { get; }

        public IReadOnlyCollection<Address> Addresses { get; }

        private UpdateUserCommand(Guid userId, string name, IReadOnlyCollection<Address> addresses)
        {
            UserId = userId;
            Name = name;
            Addresses = addresses;
        }

        public static UpdateUserCommand Create(Guid userId, string name, IReadOnlyCollection<Address> addresses)
        {
            userId.ThrowIfEmpty(nameof(userId));
            addresses.ThrowIfNull(nameof(addresses));

            return new UpdateUserCommand(userId, name, addresses);
        }
    }
}
