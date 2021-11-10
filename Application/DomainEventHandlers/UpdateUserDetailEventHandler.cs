using Application.Models;
using AutoMapper;
using Core.Events;
using Core.Repositories;
using Domain.Aggregates.Users;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainEventHandlers
{
    public class UpdateUserDetailEventHandler
        : IEventHandler<UserCreatedEvent>,
        IEventHandler<UserUpdatedEvent>,
        IEventHandler<UserDeletedEvent>
    {
        private readonly IModelRepository<UserDetail> _userDetailRepository;
        private readonly IMapper _mapper;

        public UpdateUserDetailEventHandler(IModelRepository<UserDetail> userDetailRepository, IMapper mapper)
        {
            _userDetailRepository = userDetailRepository ?? throw new ArgumentNullException(nameof(userDetailRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            var userDetail = _mapper.Map<UserDetail>(@event);

            _userDetailRepository.Add(userDetail);

            await _userDetailRepository.SaveChanges(cancellationToken);
        }

        public async Task Handle(UserUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var userDetail = await _userDetailRepository.Find(@event.UserId, cancellationToken);

            if (userDetail == null)
                throw new InvalidOperationException($"Failed to locate User read model for update user with id {@event.UserId}.");

            userDetail.Name = @event.Name;

            UpdateAddresses(userDetail, @event.Addresses);

            _userDetailRepository.Update(userDetail);

            await _userDetailRepository.SaveChanges(cancellationToken);
        }

        public async Task Handle(UserDeletedEvent @event, CancellationToken cancellationToken)
        {
            var userDetail = await _userDetailRepository.Find(@event.UserId, cancellationToken);

            if (userDetail == null)
                throw new InvalidOperationException($"Failed to locate User read model for delete user with id {@event.UserId}.");

            _userDetailRepository.Delete(userDetail);

            await _userDetailRepository.SaveChanges(cancellationToken);
        }

        private void UpdateAddresses(UserDetail userDetail, IEnumerable<Address> addresses)
        {
            var details = _mapper.Map<IEnumerable<AddressDetail>>(addresses);

            details
                .Where(x => !userDetail.Addresses.Any(y => y.AddressId == x.AddressId))
                .ToList()
                .ForEach(userDetail.Addresses.Add);

            userDetail.Addresses
                .Select(x => (storedAddress: x, adddress: addresses.FirstOrDefault(y => y.AddressId == x.AddressId)))
                .Where(x => x.adddress != null)
                .ToList()
                .ForEach(x => _mapper.Map(x.adddress, x.storedAddress));

            userDetail.Addresses
                .Where(x => !addresses.Any(y => y.AddressId == x.AddressId))
                .Select(userDetail.Addresses.Remove)
                .ToList();
        }
    }
}
