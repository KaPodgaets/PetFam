using AutoFixture;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public static class FixtureExtensions
{
    public static CreateCommand FakeCreateCommand(this IFixture fixture)
    {
        return fixture.Build<CreateCommand>()
            .Create();
    }
}