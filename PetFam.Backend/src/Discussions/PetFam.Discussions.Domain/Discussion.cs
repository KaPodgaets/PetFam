using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Domain;

// TODO: think to be able to change admin user in discussion
public class Discussion : Entity<DiscussionId>
{
    private readonly List<User> _users = [];
    private readonly List<Message> _messages = [];

    private Discussion(DiscussionId id) : base(id)
    {
    }

    private Discussion(
        DiscussionId id,
        Guid relationId,
        List<User> users) : base(id)
    {
        RelationId = relationId;
        _users = users;
    }

    public Guid RelationId { get; private set; }
    public List<User> Users => _users;
    public IReadOnlyList<Message> Messages => _messages.AsReadOnly();
    public bool IsClosed { get; private set; }

    public static Result<Discussion> Create(Guid relationId, List<User> users)
    {
        if (users.Count != 2)
            return Errors.Discussions.IncorrectNumberOfParticipants().ToErrorList();

        return new Discussion(
            DiscussionId.NewId(),
            relationId,
            users);
    }

    public Result<Guid> AddMessage(Message message)
    {
        if (IsClosed is true)
            return Errors.Discussions.CannotAddMessageToClosedDiscussion().ToErrorList();

        if (_users.Select(x => x.UserId).Contains(message.UserId) is false)
            return Errors.Discussions.CannotAddNewMessageFromNonParticipants().ToErrorList();

        _messages.Add(message);
        return message.Id.Value;
    }

    public Result DeleteMessage(Guid messageId, Guid userId)
    {
        if (IsClosed is true)
            return Errors.Discussions.CannotAddMessageToClosedDiscussion().ToErrorList();

        var message = _messages.FirstOrDefault(x => x.Id.Value == messageId);
        if (message is null)
            return Errors.General.NotFound(messageId).ToErrorList();
        if (message.UserId != userId)
            return Errors.Discussions.OnlyAuthorCanRemoveMessage().ToErrorList();

        _messages.Remove(message);
        return Result.Success();
    }

    public Result EditMessage(Guid messageId, Guid userId, string newText)
    {
        if (IsClosed is true)
            return Errors.Discussions.CannotAddMessageToClosedDiscussion().ToErrorList();

        var message = _messages.FirstOrDefault(x => x.Id.Value == messageId);
        if (message is null)
            return Errors.General.NotFound(messageId).ToErrorList();
        if (message.UserId != userId)
            return Errors.Discussions.OnlyAuthorCanRemoveMessage().ToErrorList();

        var result = message.Edit(newText);
        return result;
    }

    public void Close()
    {
        IsClosed = false;
    }
}