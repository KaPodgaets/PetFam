using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.VolunteerManagement.Commands.PetUpdate;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Api.Requests.Volunteer;

public record PetUpdateRequest(
    string NickName,
    SpeciesBreedDto SpeciesAndBreed,
    int Status,
    PetGeneralInfoDto GeneralInfo,
    PetHealthInfoDto HealthInfo,
    PetAddressDto Address,
    AccountInfoDto AccountInfo)
{
    public PetUpdateCommand ToCommand(Guid volunteerId, Guid petId)
    {
        return new PetUpdateCommand(
            volunteerId,
            petId,
            NickName,
            SpeciesAndBreed,
            Status,
            GeneralInfo,
            HealthInfo,
            Address,
            AccountInfo);
    }
    
}