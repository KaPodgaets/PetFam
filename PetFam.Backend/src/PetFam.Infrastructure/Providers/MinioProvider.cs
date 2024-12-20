﻿using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFam.Application.FileProvider;
using PetFam.Domain.Shared;

namespace PetFam.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private const int MAX_THREADS = 5;
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<List<string>>> GetFiles(string bucketName)
        {
            var listObjectsArg = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithRecursive(true);

            var objects = _minioClient.ListObjectsAsync(listObjectsArg);
            var tcs = new TaskCompletionSource<bool>();

            List<string> paths = [];

            using var subscription = objects.Subscribe(
                item => paths.Add(item.Key),
                ex => 
                    {
                        _logger.LogError(ex, "Failed to list files");
                        tcs.SetResult(true);
                    },
                () =>
                    {
                        _logger.LogInformation("Successfully listed files");
                        tcs.SetResult(true);
                    }
                );

            await tcs.Task;
            return paths;
        }

        private async Task<Result<bool>> IsBucketExistAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExistArgs = new BucketExistsArgs()
                                .WithBucket(bucketName);

                var bucketExists = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
                return bucketExists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request to not existing bucket");
                return Error.Failure("bucket.not.exists", "Request to not existing bucket").ToErrorList();
            }
        }

        public async Task<Result> CreateBucket(string bucketName, CancellationToken cancellationToken = default)
        {
            try
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to create new bucket");
                return Error.Failure("bucket.create", "Fail to create new bucket").ToErrorList();
            }


        }

        public async Task<Result> UploadFiles(
            Content content,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExists = await IsBucketExistAsync(content.BucketName, cancellationToken);

                if (bucketExists.Value == false)
                {
                    var createBucketResult = await CreateBucket(content.BucketName, cancellationToken);
                    if (createBucketResult.IsFailure)
                    {
                        return Error.Failure("minio.bucket.not.exist", "Can not create bucket").ToErrorList();
                    }
                }
                
                var options = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = MAX_THREADS, // Limit the number of parallel tasks
                            CancellationToken = cancellationToken // Propagate cancellation tokens
                        };

                await Parallel.ForEachAsync(content.FilesData, options, async (file, ct) =>
                {
                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(content.BucketName)
                        .WithStreamData(file.Stream)
                        .WithObjectSize(file.Stream.Length)
                        .WithObject(file.FileMetadata.ObjectName);

                    // Perform the upload
                    await _minioClient.PutObjectAsync(putObjectArgs, ct);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio").ToErrorList();
            }

            return Result.Success();
        }

        public async Task<Result<string>> GetDownloadLink(
            FileMetadata fileMetadata,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExists = await IsBucketExistAsync(fileMetadata.BucketName, cancellationToken);

                if (bucketExists.IsFailure)
                {
                    return Error.Failure("minio.bucket.not.exist", "This bucket does not exist in minio").ToErrorList();
                }

                var statObjectArgs = new StatObjectArgs()
                                       .WithBucket(fileMetadata.BucketName)
                                       .WithObject(fileMetadata.ObjectName.ToString());

                var objectStat = await _minioClient.StatObjectAsync(statObjectArgs, cancellationToken);

                if (objectStat.Size == 0)
                {
                    _logger.LogError("Fail to get file in minio");
                    return Error.Failure("minio.file.not.exist", "This file does not exist in minio").ToErrorList();
                }

                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileMetadata.BucketName)
                    .WithObject(fileMetadata.ObjectName.ToString())
                    .WithExpiry(60 * 60 * 24);

                var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to upload file in minio");
                return Error.Failure("file.get.link", "Fail to gey link to file in minio").ToErrorList();
            }
        }


        public async Task<Result> DeleteFile(
            FileMetadata fileMetadata,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExists = await IsBucketExistAsync(fileMetadata.BucketName, cancellationToken);

                if (bucketExists.IsFailure)
                {
                    return Error.Failure("minio.bucket.not.exist", "This bucket does not exist in minio").ToErrorList();
                }

                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(fileMetadata.BucketName)
                    .WithObject(fileMetadata.ObjectName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to delete file in minio");
                return Error.Failure("file.delete", "Fail to delete file in minio").ToErrorList();
            }
        }

        
    }
}
