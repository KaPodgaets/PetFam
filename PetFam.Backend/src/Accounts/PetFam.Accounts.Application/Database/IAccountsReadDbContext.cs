using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Application.DataModels;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.Database;

public interface IAccountsReadDbContext
{
    public DbSet<UserDataModel> Users { get; }
    public DbSet<RoleDataModel> Roles { get; }
    public DbSet<UserRoleDataModel> UserRoles { get; }
    public DbSet<AdminAccount> AdminAccounts { get; }
    public DbSet<VolunteerAccount> VolunteerAccounts { get; }
    public DbSet<ParticipantAccount> ParticipantAccounts { get; }
}