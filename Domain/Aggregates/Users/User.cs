using Core.Aggregates;
using Domain.Events;
using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Users
{
    public class User : Aggregate
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public UserStatus Status { get; private set; }

        public IReadOnlyCollection<Address> Addresses { get; private set; } = default;

        public static User Create(Guid id, string username, string password, string name, IReadOnlyCollection<Address> addresses)
            => new User(
                id,
                username,
                password,
                name,
                addresses);

        private User() { }

        private User(Guid id, string username, string password, string name, IReadOnlyCollection<Address> addresses)
        {
            var @event = UserCreatedEvent.Create(
                id,
                username,
                password,
                name,
                addresses);

            Enqueue(@event);
            Apply(@event);
        }

        public void UpdateUser(string name, IReadOnlyCollection<Address> addresses)
        {
            var @event = UserUpdatedEvent.Create(
                Id,
                name,
                addresses);

            Enqueue(@event);
            Apply(@event);
        }

        public void DeleteUser()
        {
            var @event = UserDeletedEvent.Create(Id);

            Enqueue(@event);
            Apply(@event);
        }

        public void Apply(UserCreatedEvent @event)
        {
            Version++;

            Id = @event.UserId;
            Username = @event.Username;
            Password = @event.Password;
            Name = @event.Name;
            Status = UserStatus.Active;
            Addresses = @event.Addresses;
        }

        public void Apply(UserUpdatedEvent @event)
        {
            Version++;

            Name = @event.Name;
            Addresses = @event.Addresses;
        }

        public void Apply(UserDeletedEvent @event)
        {
            Version++;

            Status = UserStatus.Deleted;
        }

        public override void When(object @event)
        {
            switch(@event)
            {
                case UserCreatedEvent userCreatedEvent:
                    Apply(userCreatedEvent);
                    break;
                case UserUpdatedEvent userUpdatedEvent:
                    Apply(userUpdatedEvent);
                    break;
                case UserDeletedEvent userDeletedEvent:
                    Apply(userDeletedEvent);
                    break;
            }
        }
    }
}
