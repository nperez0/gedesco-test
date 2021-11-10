using Core.Commands;
using Core.Repositories;
using Domain.Aggregates.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IAggregateRepository<User> _userRepository;

        public CreateUserCommandHandler(IAggregateRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.UserId, request.Username, request.Password, request.Name, request.Address);

            await _userRepository.Add(user, cancellationToken);

            return Unit.Value;
        }
    }
}
