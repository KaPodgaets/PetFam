using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Application.VolunteerManagement;
using PetFam.PetManagement.Infrastructure.BackgroundServices;
using PetFam.PetManagement.Infrastructure.DbContexts;
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
            .AddRepositories();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // services.AddHostedService<PetManagementEntityCleaner>();
        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<WriteDbContext>(_ =>
            new WriteDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IReadDbContext, ReadDbContext>(_ =>
            new ReadDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));

        return services;
    }

    private static void AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
    }
}