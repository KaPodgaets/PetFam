using PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Presentation.Requests
{
    public record CreatePetRequest(
        string NickName,
        Guid SpeciesId,
        Guid BreedId,
        PetGeneralInfoDto PetGeneralInfoDto,
        PetHealthInfoDto PetHealthInfoDto,
        PetAddressDto AddressDto,
        AccountInfoDto AccountInfoDto)
    {
        public CreatePetCommand ToCommand(Guid volunteerId)
        {
            return new CreatePetCommand(
                volunteerId,
                NickName,
                SpeciesId,
                BreedId,
                PetGeneralInfoDto,
                PetHealthInfoDto,
                AddressDto,
                AccountInfoDto);
        }
    }
}
