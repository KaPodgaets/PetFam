using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Application.VolunteerManagement;
using PetFam.PetManagement.Infrastructure.BackgroundServices;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddRepositories();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<PetManagementEntityCleaner>();
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
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}