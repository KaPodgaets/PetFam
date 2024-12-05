using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Application.Database;

public interface IAccountsReadDbContext
{
    public DbSet<UserDataModel> Users { get; }
    public DbSet<RoleDataModel> Roles { get; }
    public DbSet<UserRoleDataModel> UserRoles { get; }
}