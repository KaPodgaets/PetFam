using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Contracts;

namespace PetFam.PetManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerContract, VolunteerContract>();

        return services;
    }
}