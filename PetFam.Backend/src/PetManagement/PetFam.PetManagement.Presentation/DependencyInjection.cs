using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Contracts;
using PetFam.PetManagement.Infrastructure;

namespace PetFam.PetManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPetManagementModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IVolunteerContracts, VolunteerContracts>();
        services.AddInfrastructure(configuration);
        return services;
    }
}