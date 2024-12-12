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
    protected readonly Fixture Fixture;
    protected readonly TestsWebAppFactory Factory;
    protected readonly IServiceScope Scope;
    protected readonly VolunteersWriteDbContext VolunteersWriteDbContext;


    protected PetManagementTestBase(TestsWebAppFactory factory)
    {
        Factory = factory;
        Fixture = new Fixture();
        Scope = factory.Services.CreateScope();
        VolunteersWriteDbContext = Scope.ServiceProvider.GetRequiredService<VolunteersWriteDbContext>();
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
        
        await VolunteersWriteDbContext.AddAsync(volunteer);
        await VolunteersWriteDbContext.SaveChangesAsync();
        
        return volunteer.Id.Value;
    }

    protected async Task<Guid> SeedPet(Guid volunteerId)
    {
        var volunteer = await VolunteersWriteDbContext.Volunteers.FindAsync(VolunteerId.Create(volunteerId));
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
        
        await VolunteersWriteDbContext.SaveChangesAsync();
        
        return pet.Id.Value;
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