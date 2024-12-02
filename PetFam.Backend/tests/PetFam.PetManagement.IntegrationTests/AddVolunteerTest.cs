using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.Database;

namespace PetFam.PetManagement.IntegrationTests;

public class AddVolunteerTest(IntegrationTestsWebFactory factory) : IClassFixture<IntegrationTestsWebFactory>
{
    [Fact]
    public void GetVolunteersTest()
    {
        var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IReadDbContext>();
        
        var volunteers = dbContext.Volunteers.ToList();
        volunteers.Should().NotBeNullOrEmpty();
    }
}