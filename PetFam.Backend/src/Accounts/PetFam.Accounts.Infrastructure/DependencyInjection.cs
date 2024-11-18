using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.Seeding;

namespace PetFam.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AccountsWriteDbContext>();
        
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.JwtOptionsName));
        
        services.AddTransient<ITokenProvider,JwtTokenProvider>();
        
        services.AddSingleton<AccountsSeeder>();
        services.RegisterIdentity();
        
        return services;
    }

    private static void RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options => { options.User.RequireUniqueEmail = true; })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AccountsWriteDbContext>();
        
    }
}