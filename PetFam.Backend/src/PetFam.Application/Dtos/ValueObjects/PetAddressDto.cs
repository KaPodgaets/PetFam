namespace PetFam.Application.Dtos.ValueObjects
{
    public record PetAddressDto(string Country,
            string City,
            string Street,
            int? Building,
            string? Litteral);
}
