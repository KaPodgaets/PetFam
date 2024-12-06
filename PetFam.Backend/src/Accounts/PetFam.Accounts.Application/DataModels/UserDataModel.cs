using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.DataModels;

public class UserDataModel
{
    public Guid Id { get; init; }

    public Guid? AdminAccountId { get; init; }
    public Guid? VolunteerAccountId { get; init; }
    public Guid? ParticipantAccountId { get; init; }

    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    
    public AdminAccount? AdminAccount { get; init; }
    public VolunteerAccount? VolunteerAccount { get; init; }
    public ParticipantAccount? ParticipantAccount { get; init; } = null!;

    public List<RoleDataModel> Roles { get; init; } = [];
    public List<UserRoleDataModel> UserRoles { get; init; } = default!;
}