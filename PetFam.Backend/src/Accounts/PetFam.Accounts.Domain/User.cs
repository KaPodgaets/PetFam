using Microsoft.AspNetCore.Identity;

namespace PetFam.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<Role> _roles = [];
    
    private User()
    {
    }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    public AdminAccount? AdminAccount { get; set; }
    public Guid? AdminAccountId { get; set; }
    public VolunteerAccount? VolunteerAccount { get; set; }
    public Guid? VolunteerAccountId { get; set; }
    public ParticipantAccount? ParticipantAccount { get; set; }
    public Guid? ParticipantAccountId { get; set; }
    public static User CreateAdmin(string email, IEnumerable<Role> roles)
    {
        return new User
        {
            Email = email,
            UserName = email,
            _roles = roles.ToList()
        };
    }
    
    public static User CreateVolunteer(string email, IEnumerable<Role> roles)
    {
        return new User
        {
            Email = email,
            UserName = email,
            _roles = roles.ToList()
        };
    }
    
    public static User CreateParticipant(string email, IEnumerable<Role> roles)
    {
        return new User
        {
            Email = email,
            UserName = email,
            _roles = roles.ToList()
        };
    }
}