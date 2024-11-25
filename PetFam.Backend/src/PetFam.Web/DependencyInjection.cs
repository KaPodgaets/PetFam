using System.Text;
using PetFam.Shared.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PetFam.Accounts.Infrastructure;
using PetFam.Accounts.Infrastructure.Options;
using PetFam.Framework.Authorization;

namespace PetFam.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(PetFam.PetManagement.Application.DependencyInjection).Assembly,
            typeof(PetFam.BreedManagement.Application.DependencyInjection).Assembly,
            typeof(PetFam.Files.Application.DependencyInjection).Assembly,
            typeof(PetFam.Accounts.Application.DependencyInjection).Assembly,
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

    public static IServiceCollection AddAuthorizationServices(
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