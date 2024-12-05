using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Accounts.Infrastructure.Options;
using PetFam.Accounts.Infrastructure.Seeding;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Options;
using PetFam.Shared.SharedKernel;

namespace PetFam.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddContexts(configuration);
        
        
        services.AddTransient<ITokenProvider,JwtTokenProvider>();
        
        services
            .RegisterOptions(configuration)
            .RegisterIdentity()
            .AddAccountsSeeding();

        return services;
    }

    private static IServiceCollection AddContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AccountsWriteDbContext>(_ =>
            new AccountsWriteDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IAccountsReadDbContext, AccountsReadDbContext>(_ =>
            new AccountsReadDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);
        
        return services;
    }

    private static IServiceCollection AddAccountsSeeding(this IServiceCollection services)
    {
        services.AddSingleton<AccountsSeeder>();
        services.AddScoped<AccountsSeedingService>();
        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));
        services.Configure<AdminOptions>(
            configuration.GetSection(AdminOptions.SectionName));
        services.Configure<SoftDeleteOptions>(
            configuration.GetSection(SoftDeleteOptions.SectionName));
        return services;
    }

    private static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options => { options.User.RequireUniqueEmail = true; })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AccountsWriteDbContext>()
            .AddRoleManager<RoleManager<Role>>();

        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<AdminAccountsManager>();
        services.AddScoped<IParticipantAccountsManager, ParticipantAccountsManager>();
        services.AddScoped<IRefreshSessionsManager, RefreshSessionsManager>();
        
        
        return services;
    }
}