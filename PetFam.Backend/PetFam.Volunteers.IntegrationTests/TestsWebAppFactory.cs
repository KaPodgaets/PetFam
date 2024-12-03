using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Web;
using Respawn;
using Testcontainers.PostgreSql;

namespace PetFam.Volunteers.IntegrationTests;

public class TestsWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("pet-fam_tests")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private Respawner _respawner = default!;
    private DbConnection _dbConnection = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(ConfigureDefaultServices);
    }

    protected virtual void ConfigureDefaultServices(IServiceCollection services)
    {
        services.RemoveAll(typeof(WriteDbContext));

        var connectionString = _dbContainer.GetConnectionString();

        services.AddScoped<WriteDbContext>(_ =>
            new WriteDbContext(connectionString));
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
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
            SchemasToInclude = ["public"]
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}