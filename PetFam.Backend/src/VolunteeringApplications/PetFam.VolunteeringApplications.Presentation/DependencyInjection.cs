using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Infrastructure;

namespace PetFam.VolunteeringApplications.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteeringApplicationsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationsInfrastructure(configuration);
        return services;
    }
}