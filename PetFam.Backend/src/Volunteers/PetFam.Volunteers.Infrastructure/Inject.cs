using PetFam.Volunteers.Infrastructure.BackgroundServices;
using PetFam.Volunteers.Infrastructure.DbContexts;
using PetFam.Volunteers.Infrastructure.MessageQueues;

namespace PetFam.Volunteers.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContexts()
                .AddRepositories()
                .AddMinio(configuration)
                .AddBackgroundServices();

            services.AddScoped<IFileProvider, MinioProvider>();

            services.AddSingleton<IMessageQueue, MessageQueue>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
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
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();

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
}
