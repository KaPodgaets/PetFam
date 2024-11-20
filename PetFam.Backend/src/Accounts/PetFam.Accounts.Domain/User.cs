using Microsoft.AspNetCore.Identity;

namespace PetFam.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<Role> _roles = [];

    private User()
    {
    }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

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