namespace PetFam.Accounts.Domain;

public class AdminAccount
{
    public const string RoleName = "Admin";
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string FullName { get; init; } = string.Empty;
}