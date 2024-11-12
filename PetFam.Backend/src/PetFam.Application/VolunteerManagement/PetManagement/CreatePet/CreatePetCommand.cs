using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.PetManagement.CreatePet
{
    public record CreatePetCommand(
        Guid VolunteerId,
        string NickName,
        string SpeciesName,
        string BreedName,
        PetGeneralInfoDto PetGeneralInfoDto,
        PetHealthInfoDto PetHealthInfoDto,
        PetAddressDto AddressDto,
        AccountInfoDto AccountInfoDto):ICommand;
}
