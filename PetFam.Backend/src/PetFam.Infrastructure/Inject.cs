using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.Volunteers;
using PetFam.Infrastructure.Repositories;

namespace PetFam.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            return services;
        }
    }
}
