using AutoFixture;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Delete;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public static class FixtureExtension
{
    public static CreateVolunteerCommand FakeCreateVolunteerCommand(this IFixture fixture)
    {
        return fixture.Build<CreateVolunteerCommand>()
            .With(c => c.Email, "email@email.com")
            .Create();
    }
    
    public static DeleteVolunteerCommand FakeDeleteVolunteerCommand(this IFixture fixture, Guid volunteerId)
    {
        return fixture.Build<DeleteVolunteerCommand>()
            .With(c => c.Id, volunteerId)
            .Create();
    }
    
    public static CreatePetCommand FakeCreatePetCommand(this IFixture fixture, Guid volunteerId)
    {
        return fixture.Build<CreatePetCommand>()
            .With(c => c.VolunteerId, volunteerId)
            .Create();
    }
}