using PetFam.Accounts.Contracts.Dtos;

namespace PetFam.Accounts.Contracts.Responses;

public record UserResponse(
    Guid Id,
    string UserName,
    string Email,
    AdminAccountDto? AdminAccount,
    VolunteerAccountDto? VolunteerAccount,
    ParticipantAccountDto ParticipantAccount);