using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Domain;
using PetFam.PetManagement.Domain.Entities;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Species;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public class PetManagementTestBase : IClassFixture<TestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture _fixture;
    protected readonly TestsWebAppFactory _factory;
    protected readonly IServiceScope _scope;
    protected readonly WriteDbContext _writeDbContext;


    protected PetManagementTestBase(TestsWebAppFactory factory)
    {
        _factory = factory;
        _fixture = new Fixture();
        _scope = factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
    }

    protected async Task<Guid> SeedVolunteer()
    {
        var volunteer = Volunteer.Create(
            VolunteerId.NewId(),
            FullName.Create("Test", "Test", null).Value,
            Email.Create("Test@Test.com").Value,
            null,
            null
        ).Value;
        
        await _writeDbContext.AddAsync(volunteer);
        await _writeDbContext.SaveChangesAsync();
        
        return volunteer.Id.Value;
    }

    protected async Task<Guid> SeedPet(Guid volunteerId)
    {
        var volunteer = await _writeDbContext.Volunteers.FindAsync(VolunteerId.Create(volunteerId));
        if (volunteer is null)
            return Guid.Empty;

        var pet = Pet.Create(
            PetId.NewPetId(),
            "Nickname",
            SpeciesBreed.Create(SpeciesId.NewId(), Guid.NewGuid()).Value,
            PetStatus.LookingForHome,
            PetGeneralInfo.Create("comment", "color", 11.11, 22.22, "55065432").Value,
            PetHealthInfo.Create("comment", true, DateTime.UtcNow, true, 1).Value,
            Address.Create("Country", "City", "Street", 1, null).Value,
            AccountInfo.Create("test", "test").Value,
            DateTime.Now.ToUniversalTime(),
            volunteer.Pets.Count).Value;
        
        volunteer.AddPet(pet);
        
        await _writeDbContext.SaveChangesAsync();
        
        return pet.Id.Value;
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await _factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}