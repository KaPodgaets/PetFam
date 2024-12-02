using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Web;
using Testcontainers.PostgreSql;

namespace PetFam.Volunteers.IntegrationTests;

public class Factory:WebApplicationFactory<Program>,IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("pet-fam_tests")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();
    
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
        var volunteersDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await volunteersDbContext.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}