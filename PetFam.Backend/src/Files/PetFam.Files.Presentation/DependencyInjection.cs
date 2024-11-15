using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Files.Contracts;
using PetFam.Files.Infrastructure;

namespace PetFam.Files.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddFilesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddFilesInfrastructure(configuration);
        
        services.AddScoped<IFilesContracts, FilesContracts>();
        
        return services;
    }
}