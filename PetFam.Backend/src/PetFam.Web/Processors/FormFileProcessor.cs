using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel;

namespace PetFam.Web.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileDataDto> _files = [];

        public List<FileDataDto> Process(IFormFileCollection files)
        {
            foreach (var item in files)
            {
                var extension = Path.GetExtension(item.FileName);
                var fileMetadata = new FileMetadata(
                    Constants.FileManagementOptions.PHOTO_BUCKET,
                    Guid.NewGuid().ToString() + extension);

                var stream = item.OpenReadStream();
                var fileData = new FileDataDto(stream, fileMetadata);

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
