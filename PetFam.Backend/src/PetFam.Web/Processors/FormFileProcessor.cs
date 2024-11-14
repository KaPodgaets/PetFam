using PetFam.Shared.SharedKernel;
using PetFam.Volunteers.Application.FileProvider;

namespace PetFam.Api.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileData> _files = [];

        public List<FileData> Process(IFormFileCollection files)
        {
            foreach (var item in files)
            {
                var extension = Path.GetExtension(item.FileName);
                var fileMetadata = new FileMetadata(
                    Constants.FileManagementOptions.PHOTO_BUCKET,
                    Guid.NewGuid().ToString() + extension);

                var stream = item.OpenReadStream();
                var fileData = new FileData(stream, fileMetadata);

                _files.Add(fileData);
            }
            return _files;
        }
        public async ValueTask DisposeAsync()
        {
            foreach (var file in _files)
            {
                await file.Stream.DisposeAsync();
            }
        }
    }
}
