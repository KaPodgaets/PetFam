using AutoFixture;
using PetFam.Discussions.Application.Commands.AddMessage;
using PetFam.Discussions.Application.Commands.Close;
using PetFam.Discussions.Application.Commands.Create;
using PetFam.Discussions.Application.Commands.DeleteMessage;
using PetFam.Discussions.Application.Commands.EditeMessage;

namespace PetFam.Discussions.IntegrationTests;

public static class FixtureExtensions
{
    public static CreateDiscussionCommand FakeCreateCommand(this IFixture fixture)
    {
        return fixture.Build<CreateDiscussionCommand>()
            .Create();
    }

    public static CloseDiscussionCommand FakeCloseDiscussionCommand(this IFixture fixture, Guid id)
    {
        return fixture.Build<CloseDiscussionCommand>()
            .With(x => x.Id, id)
            .Create();
    }

    public static AddMessageCommand FakeAddMessageCommand(
        this IFixture fixture,
        Guid discussionId,
        Guid userId)
    {
        return fixture.Build<AddMessageCommand>()
            .With(x => x.DiscussionId, discussionId)
            .With(x => x.UserId, userId)
            .Create();
    }
    
    public static DeleteMessageCommand FakeDeleteMessageCommand(
        this IFixture fixture,
        Guid discussionId,
        Guid messageId,
        Guid userId)
    {
        return fixture.Build<DeleteMessageCommand>()
            .With(x => x.DiscussionId, discussionId)
            .With(x => x.MessageId, messageId)
            .With(x => x.UserId, userId)
            .Create();
    }
    
    public static EditMessageCommand FakeEditMessageCommand(
        this IFixture fixture,
        Guid discussionId,
        Guid messageId,
        Guid userId)
    {
        return fixture.Build<EditMessageCommand>()
            .With(x => x.DiscussionId, discussionId)
            .With(x => x.MessageId, messageId)
            .With(x => x.UserId, userId)
            .Create();
    }
}