using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFam.Files.Application.FileProvider;
using PetFam.Files.Infrastructure.BackgroundServices;
using PetFam.Files.Infrastructure.MessageQueues;
using PetFam.Files.Infrastructure.Options;
using PetFam.Files.Infrastructure.Providers;
using PetFam.Shared.Messaging;

namespace PetFam.Files.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddFilesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddSingleton<IMessageQueue, MessageQueue>();
        
        services
            .AddMinio(configuration)
            .AddBackgroundServices();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        var minioOptions = configuration
                               .GetSection(MinioOptions.MINIO)
                               .Get<MinioOptions>()
                           ?? throw new ApplicationException("Missing MinIo configuration");
        
        services.AddMinio(x =>
        {
            x.WithEndpoint(minioOptions.Endpoint);
            x.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            x.WithSSL(minioOptions.WithSsl);
        });

        return services;
    }
    
    private static IServiceCollection AddBackgroundServices(
        this IServiceCollection services)
    {
        services.AddHostedService<FilesSynchronizerService>();
        services.AddHostedService<FilesCleanerService>();

        return services;
    }
}