using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.Volunteers;
using PetFam.Application.Volunteers.Create;

namespace PetFam.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<CreateVolunteerHandler>();

            return services;
        }
    }
}
