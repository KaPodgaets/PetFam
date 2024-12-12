using Microsoft.Extensions.DependencyInjection;
using PetFam.BreedManagement.Application.Database;
using PetFam.BreedManagement.Infrastructure.BackgroundServices;
using PetFam.BreedManagement.Infrastructure.Contexts;
using PetFam.BreedManagement.Infrastructure.Migrator;
using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBreedManagementInfrastructure(this IServiceCollection services)
    {
        services.AddDbContexts()
            .AddRepositories()
            .AddBackgroundServices()
            .AddDatabase();

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services)
    {
        services.AddScoped<IMigrator, BreedsMigrator>();

        return services;
    }

    private static IServiceCollection AddBackgroundServices(
        this IServiceCollection services)
    {
        services.AddHostedService<BreedManagementEntityCleaner>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
}