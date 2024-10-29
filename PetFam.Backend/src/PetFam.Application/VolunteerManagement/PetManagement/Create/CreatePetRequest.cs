using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.PetManagement.Create
{
    public record CreatePetRequest(
        Guid VolunteerId,
        CreatePetDto CreatePetDto);
    public record CreatePetDto(
        string NickName,
        string SpeciesName,
        string BreedName,
        PetGeneralInfoDto PetGeneralInfoDto,
        PetHealthInfoDto PetHealthInfoDto,
        PetAddressDto AddressDto,
        AccountInfoDto AccountInfoDto);
}
