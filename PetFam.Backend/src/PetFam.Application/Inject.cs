using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.Validation;
using PetFam.Application.Volunteers.Create;
using PetFam.Application.Volunteers.Delete;
using PetFam.Application.Volunteers.UpdateMainInfo;
using PetFam.Application.Volunteers.UpdateRequisites;
using PetFam.Application.Volunteers.UpdateSocialMedia;

namespace PetFam.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();
            services.AddScoped<IUpdateMainInfoHandler, UpdateMainInfoHandler>();
            services.AddScoped<IUpdateRequisitesHandler, UpdateRequisitesHandler>();
            services.AddScoped<IUpdateSocialMediaHandler, UpdateSocialMediaHandler>();
            services.AddScoped<IDeleteHandler, DeleteHandler>();
            services.AddValidatorsFromAssembly(typeof(CustomValidators).Assembly);

            return services;
        }
    }
}
