using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Npgsql;
using NSubstitute;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.BreedManagement.Contracts;
using PetFam.Web;
using Respawn;
using Testcontainers.PostgreSql;

namespace PetFam.Accounts.IntegrationTests;
// TODO: check is it possible to change environment for integration tests?
// TODO: run AccountsSeeder manually in test fabric
// TODO: substitute AccountsSeeder to NonOpService to prevent errors 
public class AccountsTestsWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("pet-fam_tests")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly IBreedManagementContracts _breedManagementContractsMock =
        Substitute.For<IBreedManagementContracts>();

    private Respawner _respawner = default!;
    private DbConnection _dbConnection = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(ConfigureDefaultServices);
    }

    protected virtual void ConfigureDefaultServices(IServiceCollection services)
    {
        // Delete all Background services
        services.RemoveAll(typeof(IHostedService));

        // change VolunteerDbContext
        services.RemoveAll(typeof(AccountsWriteDbContext));

        var connectionString = _dbContainer.GetConnectionString();

        services.AddScoped<AccountsWriteDbContext>(_ =>
            new AccountsWriteDbContext(connectionString));

        // substitute external resources
        services.RemoveAll(typeof(IBreedManagementContracts));
        services.AddScoped<IBreedManagementContracts>(_ => _breedManagementContractsMock);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await InitializeRespawner();
        await Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();

        await Task.CompletedTask;
    }

    private async Task InitializeRespawner()
    {
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["accounts"]
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}