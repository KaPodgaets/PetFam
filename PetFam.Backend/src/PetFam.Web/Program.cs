using PetFam.Api.Extensions;
using PetFam.Api.Middlewares;
using PetFam.Application;
using PetFam.Infrastructure;
using Serilog;

namespace PetFam.Api
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
            services.AddSwaggerGen();

            services.AddInfrastructure(builder.Configuration).AddApplication();

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            app.UseExceptionCustomHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.ApplyMigration();
            }

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
