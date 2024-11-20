using Microsoft.AspNetCore.Identity;

namespace PetFam.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } = [];
}