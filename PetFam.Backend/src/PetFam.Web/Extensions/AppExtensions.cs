using PetFam.Accounts.Infrastructure.Seeding;
using PetFam.Shared.Abstractions;
using PetFam.Web.Middlewares;
using Serilog;

namespace PetFam.Web.Extensions
{
    public static class AppExtensions
    {
        private static async Task ApplyMigrations(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var migrators = scope.ServiceProvider.GetServices<IMigrator>();
            foreach (var migrator in migrators)
            {
                await migrator.MigrateAsync();
            }
        }

        private static async Task SeedAccounts(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var accountsSeeder = scope.ServiceProvider.GetRequiredService<AccountsSeeder>();
            await accountsSeeder.SeedAsync();
        }

        public static async Task ConfigureApplication(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            
            app.UseExceptionCustomHandler();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
            {
                await app.ApplyMigrations();
                
                // seed permissions, roles and accounts
                await app.SeedAccounts();
                
                app.UseSwagger();
                app.UseSwaggerUI();
                
                if (app.Environment.IsEnvironment("Docker"))
                {
                    app.MapGet("/", () => "Hello World!");
                }
            }
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
        }
    }
}
