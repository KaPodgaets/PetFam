using Microsoft.Extensions.DependencyInjection;
using PetFam.Volunteers.Contracts;

namespace PetFam.Volunteers.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerContract, VolunteerContract>();

        return services;
    }
}