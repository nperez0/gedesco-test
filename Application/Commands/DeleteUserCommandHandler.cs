using Core.Commands;
using Core.Extensions;
using Core.Repositories;
using Domain.Aggregates.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IAggregateRepository<User> _userRepository;

        public DeleteUserCommandHandler(IAggregateRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.GetAndUpdate(
                request.UserId,
                user => user.DeleteUser(),
                cancellationToken);

            return Unit.Value;
        }
    }
}
