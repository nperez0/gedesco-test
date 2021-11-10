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
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IAggregateRepository<User> _userRepository;

        public UpdateUserCommandHandler(IAggregateRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.GetAndUpdate(
                request.UserId,
                user => user.UpdateUser(request.Name, request.Addresses),
                cancellationToken);

            return Unit.Value;
        }
    }
}
