using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetFam.Accounts.Presentation;
using PetFam.BreedManagement.Presentation;
using PetFam.Files.Presentation;
using PetFam.PetManagement.Presentation;
using PetFam.Web.Middlewares;
using Serilog;

namespace PetFam.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            

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
                .AddFilesModule(builder.Configuration)
                .AddBreedManagementModule()
                .AddPetManagementModule(builder.Configuration)
                .AddAccountsModule(builder.Configuration);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = false
                    };
                });
            
            services.AddAuthorization();
            
            var app = builder.Build();

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
