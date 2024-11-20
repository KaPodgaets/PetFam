using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Accounts.Domain;

public class VolunteerAccount
{
    public const string RoleName = "Volunteer";
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Fullname { get; set; } = string.Empty;
}