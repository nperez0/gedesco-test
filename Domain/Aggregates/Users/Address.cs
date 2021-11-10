using Core.Extensions;
using System;

namespace Domain.Aggregates.Users
{
    public class Address
    {
        public Guid AddressId { get; private set; }

        public string Street { get; private set; }

        public string ZipCode { get; private set; }

        public string City { get; private set; }

        public string Country { get; private set; }

        private Address() { }

        private Address(Guid addressId, string street, string zipCode, string city, string country)
        {
            AddressId = addressId;
            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        public static Address Create(Guid addressId, string street, string zipCode, string city, string country)
        {
            addressId.ThrowIfEmpty(nameof(addressId));
            street.ThrowIfNullOrWhiteSpace(nameof(street));
            zipCode.ThrowIfNullOrWhiteSpace(nameof(zipCode));
            city.ThrowIfNullOrWhiteSpace(nameof(city));
            country.ThrowIfNullOrWhiteSpace(nameof(country));

            return new Address(
                addressId,
                street,
                zipCode,
                city,
                country);
        }

    }
}
