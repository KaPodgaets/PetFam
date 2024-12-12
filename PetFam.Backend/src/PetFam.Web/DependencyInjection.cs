using PetFam.Shared.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PetFam.Accounts.Infrastructure.Options;
using PetFam.Accounts.Presentation;
using PetFam.BreedManagement.Presentation;
using PetFam.Discussions.Presentation;
using PetFam.Files.Presentation;
using PetFam.Framework.Authorization;
using PetFam.PetManagement.Presentation;
using PetFam.VolunteeringApplications.Presentation;

namespace PetFam.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLogging();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();

        services.AddApplicationLayers()
            .AddFilesModule(configuration)
            .AddBreedManagementModule()
            .AddPetManagementModule(configuration)
            .AddAccountsModule(configuration)
            .AddVolunteeringApplicationsModule(configuration)
            .AddDiscussionsModule(configuration);
            
        services.AddAuthorizationServices(configuration);
        
        return services;
    }
    private static IServiceCollection AddLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(o =>
        {
            o.CombineLogs = true;
        });
        return services;
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    private static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(PetFam.PetManagement.Application.DependencyInjection).Assembly,
            typeof(PetFam.BreedManagement.Application.DependencyInjection).Assembly,
            typeof(PetFam.Files.Application.DependencyInjection).Assembly,
            typeof(PetFam.Accounts.Application.DependencyInjection).Assembly,
            typeof(PetFam.VolunteeringApplications.Application.DependencyInjection).Assembly,
        };

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }

    private static IServiceCollection AddAuthorizationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration
                                     .GetSection(JwtOptions.SectionName)
                                     .Get<JwtOptions>()
                                 ?? throw new ApplicationException("missing JwtOptions");
                
                options.TokenValidationParameters = JwtValidationParametersFactory.CreateWithLifeTime(jwtOptions);
            });
        
        services.AddAuthorization();
        
        return services;
    }
}