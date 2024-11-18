using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Infrastructure;

namespace PetFam.Accounts.Presentation;

public static class DependencyInjection
{
public static IServiceCollection AddAccountsModule(
    this IServiceCollection services, 
    IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }
}