using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests.Pets;

public class CreatePetTests : PetManagementTestBase
{
    private readonly ICommandHandler<Guid, CreatePetCommand> _sut;

    public CreatePetTests(TestsWebAppFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreatePetCommand>>();
    }

    [Fact]
    public async Task CreatePet_should_success()
    {
        // Arrange
        _factory.SetupSuccessBreedManagementContractsMock();

        var volunteerId = await SeedVolunteer();
        var command = _fixture.FakeCreatePetCommand(volunteerId);

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
        volunteer.Pets.Should().NotBeEmpty();
        volunteer.Pets.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task CreatePet_WithNotExistingBreed_should_fail()
    {
        // Arrange
        _factory.SetupFailureBreedManagementContractsMock();

        var volunteerId = await SeedVolunteer();
        var command = _fixture.FakeCreatePetCommand(volunteerId);

        // Act
        var result = await _sut.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(false);

        var volunteer = await VolunteersWriteDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == VolunteerId.Create(volunteerId));
        
        volunteer.Should().NotBeNull();
        volunteer.Pets.Should().BeEmpty();
    }
}