using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFam.Application.FileProvider;
using PetFam.Domain.Shared;

namespace PetFam.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<string>> UploadFile(
            FileData fileContent,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(fileContent.BucketName);

                var bucketExists = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

                if (bucketExists == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(fileContent.BucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(fileContent.BucketName)
                    .WithStreamData(fileContent.Stream)
                    .WithObjectSize(fileContent.Stream.Length)
                    .WithObject(fileContent.ObjectName.ToString());

                var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                return result.ObjectName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }

        }
    }
}
