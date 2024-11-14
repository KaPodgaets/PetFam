using PetFam.Volunteers.Application.FileManagement.Upload;

namespace PetFam.Volunteers.Application
{
    public static class DependencyInjection
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
            services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
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
            services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes => classes.AssignableTo(
                    typeof(IQueryHandler<,>)
                ))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
