using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Infrastructure.DbContexts;

namespace PetFam.Volunteers.IntegrationTests;

public class GetVolunteerTest(Factory factory) : IClassFixture<Factory>
{
    [Fact]
    public void Test1()
    {
        using var scope = factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var volunteersDbContext = services.GetRequiredService<WriteDbContext>();
        var volunteers = volunteersDbContext.Volunteers.ToList();
        volunteers.Should().BeEmpty();
    }
}