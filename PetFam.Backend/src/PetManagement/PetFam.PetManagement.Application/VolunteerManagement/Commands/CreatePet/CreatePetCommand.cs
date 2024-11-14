using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet
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
