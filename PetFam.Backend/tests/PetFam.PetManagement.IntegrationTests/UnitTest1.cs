using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.Database;

namespace PetFam.PetManagement.IntegrationTests;

public class UnitTest1:IClassFixture<IntegrationTestsWebFactory>
{
    private readonly IntegrationTestsWebFactory _factory;

    public UnitTest1(IntegrationTestsWebFactory factory)
    {
        _factory = factory;
    }
    [Fact]
    public void Test1()
    {
        var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IReadDbContext>();
        
        var volunteers = dbContext.Volunteers.ToList();
        volunteers.Should().NotBeNullOrEmpty();
    }
}