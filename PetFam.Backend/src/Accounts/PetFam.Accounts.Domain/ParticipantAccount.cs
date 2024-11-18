namespace PetFam.Accounts.Domain;

public class ParticipantAccount
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}