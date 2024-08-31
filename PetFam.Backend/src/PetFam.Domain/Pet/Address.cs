using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record Address
    {
        private Address()
        {

        }

        private Address(string country,
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

        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public int? Building { get; }
        public string? Litteral { get; }


        public static Result<Address> Create(string country,
            string city,
            string street,
            int? building,
            string? litteral)
        {
            if (string.IsNullOrWhiteSpace(country))
                return "Country can not be empty";
            if (string.IsNullOrWhiteSpace(city))
                return "City can not be empty";
            if (string.IsNullOrWhiteSpace(street))
                return "Street can not be empty";

            return new Address(country, city, street, building, litteral);
        }

    }
}
