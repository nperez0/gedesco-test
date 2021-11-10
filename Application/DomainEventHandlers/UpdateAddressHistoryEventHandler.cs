using Application.Models;
using AutoMapper;
using Core.Events;
using Core.Repositories;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainEventHandlers
{
    public class UpdateAddressHistoryEventHandler
        : IEventHandler<UserCreatedEvent>,
        IEventHandler<UserUpdatedEvent>,
        IEventHandler<UserDeletedEvent>
    {
        private readonly IModelRepository<AddressHistory> _addressHistoryRepository;
        private readonly IMapper _mapper;

        public UpdateAddressHistoryEventHandler(IModelRepository<AddressHistory> addressHistoryRepository, IMapper mapper)
        {
            _addressHistoryRepository = addressHistoryRepository ?? throw new ArgumentNullException(nameof(addressHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            var addresses = _mapper.Map<IEnumerable<AddressHistory>>(@event.Addresses).ToList();

            addresses
                .ForEach(x => x.UserId = @event.UserId);

            addresses
                .ForEach(_addressHistoryRepository.Add);

            await _addressHistoryRepository.SaveChanges(cancellationToken);
        }

        public async Task Handle(UserUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var addresses = _mapper.Map<IEnumerable<AddressHistory>>(@event.Addresses).ToList();

            var storedAddresses = _addressHistoryRepository
                .Query()
                .Where(x => x.UserId == @event.UserId)
                .ToList();

            addresses
                .ForEach(x => x.UserId = @event.UserId);

            addresses
                .Where(x => !storedAddresses.Any(y => y.AddressId == x.AddressId))
                .ToList()
                .ForEach(_addressHistoryRepository.Add);

            storedAddresses
                .Select(x => (storedAddress: x, adddress: addresses.FirstOrDefault(y => y.AddressId == x.AddressId)))
                .Where(x => x.adddress != null)
                .Select(x => _mapper.Map(x.adddress, x.storedAddress))
                .ToList()
                .ForEach(_addressHistoryRepository.Update);

            await _addressHistoryRepository.SaveChanges(cancellationToken);
        }

        public async Task Handle(UserDeletedEvent @event, CancellationToken cancellationToken)
        {
            _addressHistoryRepository
                .Query()
                .Where(x => x.UserId == @event.UserId)
                .ToList()
                .ForEach(_addressHistoryRepository.Delete);

            await _addressHistoryRepository.SaveChanges(cancellationToken);
        }
    }
}
