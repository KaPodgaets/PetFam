using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Application.VolunteerManagement;
using PetFam.PetManagement.Infrastructure.BackgroundServices;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.PetManagement.Infrastructure.Migrator;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Options;

namespace PetFam.PetManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddRepositories()
            .AddBackgroundServices()
            .AddDatabase();

        return services;
    }
    
    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<PetManagementEntityCleaner>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IMigrator, VolunteersMigrator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<VolunteersWriteDbContext>(_ =>
            new VolunteersWriteDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IReadDbContext, ReadDbContext>(_ =>
            new ReadDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        return services;
    }
}