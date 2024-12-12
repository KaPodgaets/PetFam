using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application;
using PetFam.Discussions.Application.Database;
using PetFam.Discussions.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Options;
using PetFam.Shared.SharedKernel;

namespace PetFam.Discussions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscussionsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddDatabase()
            .AddRepositories();
            
        return services;
    }
    
    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<DiscussionsWriteDbContext>(_ =>
            new DiscussionsWriteDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));
        services.AddScoped<IDiscussionsReadDbContext, DiscussionsReadDbContext>(_ =>
            new DiscussionsReadDbContext(configuration.GetConnectionString(InfrastructureOptions.DATABASE)!));

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Discussions);
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDiscussionsRepository, DiscussionsRepository>();
        return services;
    }
}