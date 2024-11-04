using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record CreatePetRequest(
        string NickName,
        string SpeciesName,
        string BreedName,
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
                SpeciesName,
                BreedName,
                PetGeneralInfoDto,
                PetHealthInfoDto,
                AddressDto,
                AccountInfoDto);
        }
    }
}
