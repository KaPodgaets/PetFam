using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Application.DataModels;
using PetFam.Accounts.Application.Features.GetUserById;
using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.IntegrationTests;

public class GetUserById:AccountsTestBase
{
    private readonly IQueryHandler<UserDataModel, GetUserByIdQuery> _sut;

    public GetUserById(AccountsTestsWebAppFactory factory) : base(factory)
    {
        _sut = Scope.ServiceProvider.GetRequiredService<IQueryHandler<UserDataModel, GetUserByIdQuery>>();
    }

    [Fact]
    public async Task GetUserById_should_return_admin_account_and_roles()
    {
        // Arrange
        var users = WriteDbContext.Users.ToList();
        var userId = users[0].Id;
        var query = new GetUserByIdQuery(userId);
        await SeedAdminUser();
        
        // Act
        var result = await _sut.HandleAsync(query);
        var resultList = WriteDbContext.Users.ToList();
        // Assert
        result.Should().NotBeNull();
        resultList.Should().HaveCount(2);
        result.IsSuccess.Should().BeTrue();
        
        result.Value.AdminAccountId.Should().NotBeEmpty();
        result.Value.Roles.Should().NotBeEmpty();
    }
}