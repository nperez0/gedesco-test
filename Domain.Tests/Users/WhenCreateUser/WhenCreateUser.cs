using Domain.Aggregates.Users;
using System;
using System.Collections.Generic;
using Tests;

namespace Domain.Tests.Users.WhenCreateUser
{
    public class WhenCreateUser : TestBase<User>
    {
        protected Guid UserId { get; set; }

        protected string Username { get; set; }

        protected string Password { get; set; }

        protected string Name { get; set; }

        protected IReadOnlyCollection<Address> Addresses { get; set; }

        protected User Result { get; set; }

        protected override User CreateSystemUnderTest()
        {
            return null;
        }

        protected override void When()
        {
            Result = User.Create(UserId, Username, Password, Name, Addresses);
        }
    }
}
