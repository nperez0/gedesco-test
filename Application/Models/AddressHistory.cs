using System;

namespace Application.Models
{
    public class AddressHistory
    {
        public Guid UserId { get; set; }

        public Guid AddressId { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
