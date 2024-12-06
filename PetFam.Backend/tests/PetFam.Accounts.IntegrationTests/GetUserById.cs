using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Application.DataModels;
using PetFam.Accounts.Application.Features.GetUserById;
using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.IntegrationTests;

public class GetUserById:AccountsTestBase
{
    private readonly IQueryHandler<UserDataModel, GetUserByIdQuery> _sut;

    protected GetUserById(AccountsTestsWebAppFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<IQueryHandler<UserDataModel, GetUserByIdQuery>>();
    }

    [Fact]
    public async Task GetUserById_should_return_admin_account_and_roles()
    {
        // Arrange
        var users = _writeDbContext.Users.ToList();
        var userId = users[0].Id;
        var query = new GetUserByIdQuery(userId);
        
        // Act
        var result = await _sut.HandleAsync(query);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        
        result.Value.AdminAccountId.Should().NotBeEmpty();
        result.Value.Roles.Should().NotBeEmpty();
    }
}