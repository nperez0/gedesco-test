using System.Collections.Generic;

namespace Api.Requests
{
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<AddressRequest> Addresses { get; set; }
    }
}
