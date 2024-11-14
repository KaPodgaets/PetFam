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
            services.AddSwaggerGen();

            services.AddApplicationLayers();

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

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
