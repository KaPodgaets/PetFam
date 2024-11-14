using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Shared.Abstractions;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Application.VolunteerManagement;
using PetFam.PetManagement.Infrastructure.BackgroundServices;
using PetFam.PetManagement.Infrastructure.DbContexts;

namespace PetFam.PetManagement.Infrastructure
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
