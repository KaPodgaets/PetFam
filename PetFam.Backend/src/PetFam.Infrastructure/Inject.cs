using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFam.Application;
using PetFam.Application.FileManagement;
using PetFam.Application.FileProvider;
using PetFam.Application.SpeciesManagement;
using PetFam.Application.VolunteerManagement;
using PetFam.Infrastructure.BackgroundServices;
using PetFam.Infrastructure.MessageQueues;
using PetFam.Infrastructure.Options;
using PetFam.Infrastructure.Providers;
using PetFam.Infrastructure.Repositories;

namespace PetFam.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();

            services.AddMinio(configuration);
            services.AddScoped<IFileProvider, MinioProvider>();

            services.AddHostedService<FilesSynchronizerService>();
            services.AddHostedService<FilesCleanerService>();

            services.AddSingleton<IFilesCleanerMessageQueue, FilesCleanerMessageQueue>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static void AddMinio(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                    ?? throw new ApplicationException("Missing MinIo configuration");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSsl);
            });
        }
    }
}
