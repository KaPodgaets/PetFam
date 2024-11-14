using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.PetUpdate;

public record PetUpdateCommand(
    Guid VolunteerId,
    Guid PetId,
    string NickName,
    SpeciesBreedDto SpeciesAndBreed,
    int Status,
    PetGeneralInfoDto GeneralInfo,
    PetHealthInfoDto HealthInfo,
    PetAddressDto Address,
    AccountInfoDto AccountInfo):ICommand;