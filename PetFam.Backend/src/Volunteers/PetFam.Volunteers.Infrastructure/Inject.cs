using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Messaging;
using PetFam.Shared.SharedKernel;
using PetFam.Volunteers.Application.Database;
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
                .AddBackgroundServices();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
