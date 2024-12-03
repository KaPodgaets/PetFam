using AutoFixture;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
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
}