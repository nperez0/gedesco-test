using Core.Extensions;
using System;

namespace Domain.Aggregates.Users
{
    public class Address
    {
        public Guid AddressId { get; }

        public string Street { get; }

        public string ZipCode { get; }

        public string City { get; }

        public string Country { get; }

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
