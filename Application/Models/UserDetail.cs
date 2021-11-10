using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class UserDetail
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AddressDetail> Addresses { get; set; }
    }
}
