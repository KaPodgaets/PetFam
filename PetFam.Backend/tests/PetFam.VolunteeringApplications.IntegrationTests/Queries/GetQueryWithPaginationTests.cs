using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Queries.Get;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class GetQueryWithPaginationTests : ApplicationsTestBase
{
    public GetQueryWithPaginationTests(TestsWebAppFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task GetQueryWithPagination_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        await SeedFewApplicationsAsync(5);
        var applicationsToQuery = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        var query = new GetWithPaginationQuery(applicationsToQuery[1].UserId, null, null, 1, 10);
        var sut = Scope.ServiceProvider.GetRequiredService<GetWithPaginationAndFilters>();

        // Act
        var result = await sut.HandleAsync(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        result.Value.Items.Should().HaveCount(1);
        result.Value.Items[0].Id.Should().Be(applicationsToQuery[1].Id);
    }
}