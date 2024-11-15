using Microsoft.Extensions.DependencyInjection;
using PetFam.BreedManagement.Contracts;
using PetFam.BreedManagement.Infrastructure;

namespace PetFam.BreedManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddBreedManagementModule(this IServiceCollection services)
    {
        services.AddScoped<IBreedManagementContracts, BreedManagementContracts>();
        
        services.AddBreedManagementInfrastructure();
        
        return services;
    }
}