using PetFam.Shared.Abstractions;
using FluentValidation;

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
}