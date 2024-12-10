using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class ApplicationsTestBase: IClassFixture<TestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture Fixture;
    protected readonly TestsWebAppFactory Factory;
    protected readonly IServiceScope Scope;
    protected readonly ApplicationsWriteDbContext WriteDbContext;
    protected readonly IApplicationsReadDbContext ReadDbContext;


    protected ApplicationsTestBase(TestsWebAppFactory factory)
    {
        Factory = factory;
        Fixture = new Fixture();
        Scope = factory.Services.CreateScope();
        WriteDbContext = Scope.ServiceProvider.GetRequiredService<ApplicationsWriteDbContext>();
        ReadDbContext = Scope.ServiceProvider.GetRequiredService<IApplicationsReadDbContext>();
    }
    protected async Task<Guid> SeedApplication()
    {
        var application = VolunteeringApplication.CreateNewApplication(
            Guid.NewGuid(),
            Fixture.Create<string>())
            .Value;
        
        await WriteDbContext.AddAsync(application);
        await WriteDbContext.SaveChangesAsync();
        
        return application.Id.Value;
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Scope.Dispose();
        await Factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}