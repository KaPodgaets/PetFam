namespace PetFam.Shared.Messaging
{
    public interface IMessageQueue
    {
        Task<string[]> ReadAsync(CancellationToken cancellationToken);
        Task WriteAsync(string[] paths, CancellationToken cancellationToken);
    }
}