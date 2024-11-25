using Microsoft.OpenApi.Models;
using PetFam.Accounts.Infrastructure.Seeding;
using PetFam.Accounts.Presentation;
using PetFam.BreedManagement.Presentation;
using PetFam.Files.Presentation;
using PetFam.PetManagement.Presentation;
using PetFam.Web.Extensions;
using PetFam.Web.Middlewares;
using Serilog;

namespace PetFam.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            // Add services to the container.
            services.AddHttpLogging(o =>
            {
                o.CombineLogs = true;
            });
            
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
           
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


            services.AddApplicationLayers()
                .AddFilesModule(configuration)
                .AddBreedManagementModule()
                .AddPetManagementModule(configuration)
                .AddAccountsModule(configuration);
            
            services.AddAuthorizationServices(configuration);
            
            var app = builder.Build();

            // seed permissions, roles and accounts
            app.SeedAccounts();
            
            app.UseSerilogRequestLogging();
            
            app.UseExceptionCustomHandler();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // await app.ApplyMigration();
            }
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
