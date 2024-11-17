using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Infrastructure;

namespace PetFam.Accounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services)
    {
        services.AddInfrastructure();
        return services;
    }
}