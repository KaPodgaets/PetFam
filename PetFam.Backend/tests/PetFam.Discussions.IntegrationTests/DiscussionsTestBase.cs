using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Database;
using PetFam.Discussions.Domain;
using PetFam.Discussions.Infrastructure.DbContexts;

namespace PetFam.Discussions.IntegrationTests;

public class DiscussionsTestBase: IClassFixture<DiscussionsTestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture Fixture;
    protected readonly DiscussionsTestsWebAppFactory Factory;
    protected readonly IServiceScope Scope;
    protected readonly DiscussionsWriteDbContext WriteDbContext;
    protected readonly IDiscussionsReadDbContext ReadDbContext;

    protected DiscussionsTestBase(DiscussionsTestsWebAppFactory factory)
    {
        Factory = factory;
        Fixture = new Fixture();
        Scope = factory.Services.CreateScope();
        WriteDbContext = Scope.ServiceProvider.GetRequiredService<DiscussionsWriteDbContext>();
        ReadDbContext = Scope.ServiceProvider.GetRequiredService<IDiscussionsReadDbContext>();
    }
    protected async Task<Discussion> SeedDiscussion()
    {
        var discussion = CreateEmptyDiscussion();

        await WriteEntityInDatabase(discussion);
        
        return discussion;
    }

    private async Task WriteEntityInDatabase(Discussion discussion)
    {
        await WriteDbContext.AddAsync(discussion);
        await WriteDbContext.SaveChangesAsync();
    }
    
    private async Task WriteRangeInDatabase(List<Discussion> discussions)
    {
        await WriteDbContext.AddRangeAsync(discussions);
        await WriteDbContext.SaveChangesAsync();
    }

    private Discussion CreateEmptyDiscussion()
    {
        var users = new List<User>
        {
            User.Create(Guid.NewGuid(), Fixture.Create<string>()).Value,
            User.Create(Guid.NewGuid(), Fixture.Create<string>()).Value
        };
        var discussion = Discussion.Create(
                Guid.NewGuid(),
                users)
            .Value;
        return discussion;
    }

    protected async Task<List<Guid>> SeedFewDiscussionsAsync(int count)
    {
        var discussionsToSeed = new List<Discussion>();
        var guids = new List<Guid>();
        for (var i = 0; i < count; i++)
        {
            var discussion = CreateEmptyDiscussion();
            discussionsToSeed.Add(discussion);
            guids.Add(discussion.Id.Value);
        }
        await WriteRangeInDatabase(discussionsToSeed);
        return guids;
    }
    
    protected async Task<Discussion> SeedDiscussionWithMessagesAsync()
    {
        var discussion = CreateEmptyDiscussion();
        var message = Message.Create(Fixture.Create<string>(), discussion.Users[0].UserId).Value;
        discussion.AddMessage(message);
        
        await WriteEntityInDatabase(discussion);
        
        return discussion;
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Scope.Dispose();
        await Factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}