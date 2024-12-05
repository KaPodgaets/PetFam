namespace PetFam.Accounts.Application.DataModels;

public class UserRoleDataModel
{
    public Guid RoleId { get; init; }
    public RoleDataModel Role { get; init; } = default!;

    public Guid UserId { get; init; }
    public UserDataModel User { get; init; } = default!;
}