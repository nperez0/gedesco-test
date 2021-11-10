using FluentAssertions;
using NUnit.Framework;
using System;

namespace Domain.Tests.Users.WhenCreateUser
{
    public class AndUserIdIsEmpty : WhenCreateUser
    {
        protected override void Given()
        {
            RecordAnyExceptionsThrown();

            UserId = Guid.Empty;
        }

        [Test]
        public void ShouldThrowArgumentNullException()
        {
            ThrownException.Should().BeOfType(typeof(ArgumentNullException));
        }

        [Test]
        public void ShouldIndicateUserIdAsNullValue()
        {
            ThrownException.Message.Should().Be("Value cannot be null. (Parameter 'userId')");
        }
    }
}
