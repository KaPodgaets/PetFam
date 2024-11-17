using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<AccountsWriteDbContext>();
        
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