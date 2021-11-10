using System;
using System.Collections.Generic;

namespace Api.Requests
{
    public class UpdateUserRequest
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<AddressRequest> Addresses { get; set; }
    }
}
