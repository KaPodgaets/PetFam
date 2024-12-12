using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Queries.GetById;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class GetByIdQueryTests : ApplicationsTestBase
{
    public GetByIdQueryTests(TestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetByIdQuery_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var query = new GetByIdQuery(applicationId);
        var sut = Scope.ServiceProvider.GetRequiredService<GetByIdHandler>();

        // Act
        var result = await sut.HandleAsync(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(applicationId);
    }
}