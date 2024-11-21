using Microsoft.AspNetCore.Identity;

namespace PetFam.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private List<Role> _roles = [];

    private User()
    {
    }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    public static User CreateUser(string email)
    {
        return new User
        {
            UserName = email,
            Email = email,
        };
    }

    public static User CreateAdmin(string email, IEnumerable<Role> roles)
    {
        return new User
        {
            Email = email,
            UserName = email,
            _roles = roles.ToList()
        };
    }
}