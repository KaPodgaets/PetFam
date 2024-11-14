using Microsoft.Extensions.DependencyInjection;

namespace PetFam.Volunteers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerContract, VolunteerContract>();

        return services;
    }
}