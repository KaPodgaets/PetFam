using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(string connectionString) : DbContext, IAccountsReadDbContext
{
    public DbSet<UserDataModel> Users => Set<UserDataModel>();
    public DbSet<RoleDataModel> Roles => Set<RoleDataModel>();
    public DbSet<UserRoleDataModel> UserRoles => Set<UserRoleDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AccountsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
        
        modelBuilder.HasDefaultSchema("accounts");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}