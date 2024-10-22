using PetFam.Application.Volunteers;
using PetFam.Infrastructure;
using PetFam.Infrastructure.Repositories;

namespace PetFam.Api
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
