using Domain.Aggregates.Users;
using Domain.Events;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Tests.Users.WhenCreateUser
{
    public class AndParametersAreValid : WhenCreateUser
    {
        protected override void Given()
        {
            UserId = Guid.NewGuid();
            Username = "Ramona";
            Password = "NoTeLoSabes";
            Name = "Glorita";
            Addresses = new[]
            {
                Address.Create(Guid.NewGuid(), "Calle Alejada", "21342", "Vermut", "Uvas")
            };
        }

        [Test]
        public void ShouldPropertiesBeCorrectlyAssigned()
        {
            Result.Id.Should().Be(UserId);
            Result.Username.Should().Be(Username);
            Result.Password.Should().Be(Password);
            Result.Name.Should().Be(Name);
            Result.Addresses.Should().BeEquivalentTo(Addresses);
        }

        [Test]
        public void ShouldEnqueueUserCreatedEvent()
        {
            var @event = Result.DequeueUncommittedEvents().First() as UserCreatedEvent;

            @event.UserId.Should().Be(UserId);
        }
    }
}
