using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Infrastructure;

namespace PetFam.Discussions.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDiscussionsInfrastructure(configuration);
        return services;
    }
}