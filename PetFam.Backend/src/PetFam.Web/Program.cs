using PetFam.Web.Extensions;
using Serilog;

namespace PetFam.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            //Я знаю что ты напишешь что оно итак подтягиваться поэтому я отвечу что так будет лучше
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
                    reloadOnChange: true);

            var services = builder.Services;

            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            services.AddDependencyInjections(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureApplication();

            await app.RunAsync();
        }
    }
}