using PetFam.Accounts.Infrastructure.Seeding;
using PetFam.Web.Middlewares;
using Serilog;

namespace PetFam.Web.Extensions
{
    public static class AppExtensions
    {
        // public static async Task ApplyMigration(this WebApplication app)
        // {
        //     await using var scope = app.Services.CreateAsyncScope();
        //     var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        //
        //     await dbContext.Database.MigrateAsync();
        // }

        public static async Task SeedAccounts(this WebApplication app)
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
            if (app.Environment.IsDevelopment())
            {
                // await app.ApplyMigration();
                
                // seed permissions, roles and accounts
                await app.SeedAccounts();
                
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
        }
    }
}
