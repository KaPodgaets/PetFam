
using PetFam.Application.Volunteers;
using PetFam.Application.Volunteers.Create;
using PetFam.Infrastructure;
using PetFam.Infrastructure.Repositories;

namespace PetFam.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddScoped<ICreateVolunteerService, CreateVolunteerService>();
            builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
