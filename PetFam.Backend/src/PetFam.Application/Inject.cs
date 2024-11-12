﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Application.FileManagement.Upload;
using PetFam.Application.Interfaces;
using PetFam.Application.Validation;

namespace PetFam.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddCommands()
                .AddQueries();

            services.AddScoped<IUploadFileHandler, UploadFileHandler>();

            services.AddValidatorsFromAssembly(typeof(CustomValidators).Assembly);

            return services;
        }
        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                    .AddClasses(classes => classes.AssignableToAny(
                        [
                            typeof(ICommandHandler<,>),
                            typeof(ICommandHandler<>)
                        ]
                    ))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
                .AddClasses(classes => classes.AssignableTo(
                    typeof(IQueryHandler<,>)
                ))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }

    
}
