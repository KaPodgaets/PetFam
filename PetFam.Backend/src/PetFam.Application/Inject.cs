using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.Volunteers.Create;

namespace PetFam.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();

            return services;
        }
    }
}
