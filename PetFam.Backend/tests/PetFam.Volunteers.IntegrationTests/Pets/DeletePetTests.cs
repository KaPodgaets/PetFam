using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.DeletePet;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests.Pets;

public class DeletePetTests : PetManagementTestBase
{
    private readonly ICommandHandler<Guid, DeletePetCommand> _sut;

    public DeletePetTests(TestsWebAppFactory factory) : base(factory)
    {
        _sut = Scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, DeletePetCommand>>();
    }

    [Fact]
    public async Task CreatePet_should_success()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();
        var petId = await SeedPet(volunteerId);
        var command = Fixture.FakeDeletePetCommand(volunteerId, petId);

        // Act
        var result = await _sut.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBeEmpty();

        var volunteer = await VolunteersWriteDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == VolunteerId.Create(volunteerId));

        volunteer.Should().NotBeNull();
        volunteer.Pets.Where(p => p.IsDeleted == false).Should().BeEmpty();
    }
}