using PetFam.Application.FileManagement;
using System.Threading.Channels;

namespace PetFam.Infrastructure.MessageQueues
{
    public class FilesCleanerMessageQueue : IFilesCleanerMessageQueue
    {
        private readonly Channel<string[]> _channel = Channel.CreateUnbounded<string[]>();

        public async Task WriteAsync(string[] paths, CancellationToken cancellationToken)
        {
            await _channel.Writer.WriteAsync(paths, cancellationToken);
        }

        public async Task<string[]> ReadAsync(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
