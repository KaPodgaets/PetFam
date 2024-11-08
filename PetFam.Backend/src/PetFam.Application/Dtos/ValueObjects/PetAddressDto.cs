namespace PetFam.Application.VolunteerManagement.ValueObjects
{
    public record PetAddressDto(string Country,
            string City,
            string Street,
            int? Building,
            string? Litteral);
}
