﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.FileManagement.Delete;
using PetFam.Application.FileManagement.GetLink;
using PetFam.Application.FileManagement.Upload;
using PetFam.Application.SpeciesManagement.Create;
using PetFam.Application.SpeciesManagement.CreateBreed;
using PetFam.Application.SpeciesManagement.Delete;
using PetFam.Application.Validation;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Application.VolunteerManagement.Delete;
using PetFam.Application.VolunteerManagement.PetManagement.AddPhotos;
using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Application.VolunteerManagement.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.UpdateRequisites;
using PetFam.Application.VolunteerManagement.UpdateSocialMedia;

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

            services.AddScoped<CreateSpeciesHandler>();
            services.AddScoped<DeleteSpeciesHandler>();
            services.AddScoped<CreateBreedHandler>();

            services.AddScoped<IUploadFileHandler, UploadFileHandler>();
            services.AddScoped<GetFileLinkHandler>();
            services.AddScoped<DeleteFileHandler>();

            services.AddScoped<CreatePetHandler>();

            services.AddScoped<PetAddPhotosHandler>();

            services.AddValidatorsFromAssembly(typeof(CustomValidators).Assembly);

            return services;
        }
    }
}
