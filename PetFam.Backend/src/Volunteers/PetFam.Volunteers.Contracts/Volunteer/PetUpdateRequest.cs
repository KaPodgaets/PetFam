namespace PetFam.Volunteers.Contracts.Volunteer;

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