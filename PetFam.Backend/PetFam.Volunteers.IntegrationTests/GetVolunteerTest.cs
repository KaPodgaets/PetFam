using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Domain;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public class GetVolunteerTest : IClassFixture<Factory>
{
    private readonly Factory _factory;

    public GetVolunteerTest(Factory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Test1()
    {
        
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        
        CreateVolunteerRecord(dbContext);

        var volunteers = dbContext.Volunteers.ToList();
        
        volunteers.Should().NotBeEmpty();
    }

    private void CreateVolunteerRecord(WriteDbContext context)
    {
        var volunteer = Volunteer.Create(
            VolunteerId.NewId(),
            FullName.Create("Test", "Test", null).Value,
            Email.Create("Test@Test.com").Value,
            null,
            null
        ).Value;
        context.Add(volunteer);
        context.SaveChanges();
    }
}