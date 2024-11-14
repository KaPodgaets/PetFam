using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using PetFam.Files.Infrastructure.MessageQueues;
using PetFam.Files.Infrastructure.Options;
using PetFam.Files.Infrastructure.Providers;

namespace PetFam.Files.Infrastructure;

public class DependencyInjection
{
    public static IServiceCollection AddFilesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IFileProvider, MinioProvider>();
        services.AddSingleton<IMessageQueue, MessageQueue>();
        services.AddMinio(configuration);
    }
    
    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(options =>
        {
            var minioOptions = configuration
                                   .GetSection(Constants.FileManagementOptions.MINIO)
                                   .Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing MinIo configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });

        return services;
    }
}