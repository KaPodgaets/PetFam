using PetFam.Accounts.Infrastructure.Seeding;

namespace PetFam.Api.Extensions
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
    }
}
