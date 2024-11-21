using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Accounts.Domain;

public class VolunteerAccount
{
    public int Experience { get; set; }
    public RequisitesDetails Requisites { get; set; }
    public List<string> Certificates { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}