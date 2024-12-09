using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Shared.Options;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;

namespace PetFam.VolunteeringApplications.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationsWriteDbContext>(_ =>
            new ApplicationsWriteDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IApplicationsReadDbContext, ApplicationsReadDbContext>(_ =>
            new ApplicationsReadDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IApplicationsRepository, ApplicationsRepository>();

        return services;
    }
}