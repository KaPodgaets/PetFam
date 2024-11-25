using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Infrastructure;
using PetFam.Accounts.Presentation.Providers;

namespace PetFam.Accounts.Presentation;

public static class DependencyInjection
{
public static IServiceCollection AddAccountsModule(
    this IServiceCollection services, 
    IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddScoped<HttpContextProvider>();
        services.AddHttpContextAccessor();
        return services;
    }
}