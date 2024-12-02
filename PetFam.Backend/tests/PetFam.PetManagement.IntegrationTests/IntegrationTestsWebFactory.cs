using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Web;
using Testcontainers.PostgreSql;

namespace PetFam.PetManagement.IntegrationTests;

public class IntegrationTestsWebFactory:WebApplicationFactory<Program>, IAsyncLifetime
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
        var readDbContext = services.SingleOrDefault(s =>
            s.ServiceType == typeof(IReadDbContext));
        var writeDbContext = services.SingleOrDefault(s =>
            s.ServiceType == typeof(WriteDbContext));
        var accountsDbContext = services.SingleOrDefault(s =>
            s.ServiceType == typeof(AccountsWriteDbContext));
        
        if(readDbContext is not null)
            services.Remove(readDbContext);
        if(writeDbContext is not null)
            services.Remove(writeDbContext);
        if(accountsDbContext is not null)
            services.Remove(accountsDbContext);
        
        services.AddScoped<IReadDbContext, ReadDbContext>(_ =>
            new ReadDbContext(_dbContainer.GetConnectionString()));
        services.AddScoped<WriteDbContext>(_ =>
            new WriteDbContext(_dbContainer.GetConnectionString()));
        services.AddScoped<AccountsWriteDbContext>(_ =>
            new AccountsWriteDbContext(_dbContainer.GetConnectionString()));
        
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();
        var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await writeDbContext.Database.EnsureCreatedAsync();
        var accountsDbContext = scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();
        await accountsDbContext.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}