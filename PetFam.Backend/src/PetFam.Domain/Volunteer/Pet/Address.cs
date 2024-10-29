using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
{
    public record Address
    {
        public Address(string country,
            string city,
            string street,
            int? building,
            string? litteral)
        {
            Country = country;
            City = city;
            Street = street;
            Building = building;
            Litteral = litteral;
        }

        public string Country { get; } = null!;
        public string City { get; } = null!;
        public string Street { get; } = null!;
        public int? Building { get; }
        public string? Litteral { get; }


        public static Result<Address> Create(string country,
            string city,
            string street,
            int? building,
            string? litteral)
        {
            if (string.IsNullOrWhiteSpace(country))
                return Errors.General.ValueIsInvalid(nameof(Country));
            if (string.IsNullOrWhiteSpace(city))
                return Errors.General.ValueIsInvalid(nameof(City));
            if (string.IsNullOrWhiteSpace(street))
                return Errors.General.ValueIsInvalid(nameof(Street));

            return new Address(country, city, street, building, litteral);
        }
    }
}
