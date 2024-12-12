using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Options;
using PetFam.Shared.SharedKernel;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;
using PetFam.VolunteeringApplications.Infrastructure.Migrator;

namespace PetFam.VolunteeringApplications.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
            .AddRepositories()
            .AddDatabase();

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
    
    private static IServiceCollection AddDatabase(
        this IServiceCollection services)
    {   
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Applications);
        services.AddScoped<IMigrator, ApplicationsMigrator>();

        return services;
    }
}

