using Core.Commands;
using Core.Extensions;
using System;

namespace Application.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid UserId { get; }

        private DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public static DeleteUserCommand Create(Guid userId)
        {
            userId.ThrowIfEmpty(nameof(userId));

            return new DeleteUserCommand(userId);
        }
    }
}
