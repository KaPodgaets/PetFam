using Microsoft.OpenApi.Models;
using PetFam.Accounts.Presentation;
using PetFam.BreedManagement.Presentation;
using PetFam.Files.Presentation;
using PetFam.PetManagement.Presentation;
using PetFam.Web.Extensions;
using PetFam.Web.Middlewares;
using Serilog;

namespace PetFam.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // DotNetEnv.Env.Load();
            
            var builder = WebApplication.CreateBuilder(args);
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
