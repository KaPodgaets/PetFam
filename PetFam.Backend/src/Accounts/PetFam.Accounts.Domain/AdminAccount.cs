namespace PetFam.Accounts.Domain;

public class AdminAccount
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}