using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.Validation;
using PetFam.Application.Volunteers.Create;
using PetFam.Application.Volunteers.UpdateName;

namespace PetFam.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();
            services.AddScoped<IVolunteerUpdateNameHandler, VolunteerUpdateNameHandler>();
            services.AddValidatorsFromAssembly(typeof(CustomValidators).Assembly);

            return services;
        }
    }
}
