using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace CRMRealEstate.Application;

public class StorageService(IMinioClient minio, ILogger<StorageService> logger)
{
    private readonly StorageServiceConfiguration storageConfig = new StorageServiceConfiguration();
    public async Task SaveFileAsync(IFormFile file, string path = null, string fileName = null)
    {
        fileName ??= file.FileName;

        if (file.Length <= 0)
        {
            return;
        }

        await CreateBucketIfNotExistsAsync(storageConfig.DefaultBucketImageStore);

        await using var fileStream = file.OpenReadStream();

        var args = new PutObjectArgs()
            .WithBucket(storageConfig.DefaultBucketImageStore)
            .WithObject($"{path}/{fileName}") // Use the file name or a custom object name
            .WithStreamData(fileStream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType); // Set the content type

        await minio.PutObjectAsync(args);
    }

    public async Task SaveFileAsync(Stream fileStream, string fileName, string contentType, string path = null)
    {
        if (fileStream == null)
        {
            throw new ArgumentException("Invalid file stream or size.");
        }

        await CreateBucketIfNotExistsAsync(storageConfig.DefaultBucketImageStore);

        var args = new PutObjectArgs()
            .WithBucket(storageConfig.DefaultBucketImageStore)
            .WithObject($"{path}/{fileName}")
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        await minio.PutObjectAsync(args);
    }

    public async Task<List<string>> ListFilesAsync(string path = null)
    {
        var files = new List<string>();

        try
        {
            var bucketExists = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(storageConfig.DefaultBucketImageStore));

            if (!bucketExists)
            {
                return files;
            }

            var listArgs = new ListObjectsArgs()
                .WithBucket(storageConfig.DefaultBucketImageStore)
                .WithPrefix(path ?? string.Empty) // Use the path if provided
                .WithRecursive(true);

            var observable = minio.ListObjectsEnumAsync(listArgs);

            await foreach (var item in observable)
            {
                files.Add(item.Key);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return files;
    }

    public async Task<string> GetPresignedUrlAsync(string fileName, string path = null)
    {
        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(storageConfig.DefaultBucketImageStore)
                .WithObject($"{path}/{fileName}")
                .WithExpiry(storageConfig.ImageLifetimeSeconds);

            return await minio.PresignedGetObjectAsync(args);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return null;
        }
    }

    private async Task CreateBucketIfNotExistsAsync(string bucketName)
    {
        // Ensure the bucket exists
        var bucketExists = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

        if (!bucketExists)
        {
            // Create the bucket if it doesn't exist
            await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
    }

    public async Task DeleteFileAsync(string fileName, string path = null)
    {
        try
        {
            // Create the full object name using the path and file name
            var objectName = $"{path}/{fileName}";

            // Delete the object from the MinIO bucket
            await minio.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(storageConfig.DefaultBucketImageStore)
                .WithObject(objectName));
        }
        catch (Exception ex)
        {
            // Handle exceptions, you might want to log or rethrow depending on your needs
            throw new ApplicationException("An error occurred while deleting the file.", ex);
        }
    }

    public async Task RenameFileAsync(string oldFileName, string newFileName, string path = null)
    {
        if (oldFileName.Equals(newFileName, StringComparison.OrdinalIgnoreCase))
        {
            // If the old and new names are the same, do nothing
            return;
        }

        try
        {
            // Create the full object names
            var oldObjectName = $"{path}/{oldFileName}";
            var newObjectName = $"{path}/{newFileName}";

            // Copy the object to the new name
            await minio.CopyObjectAsync(new CopyObjectArgs()
                .WithBucket(storageConfig.DefaultBucketImageStore)
                .WithObject(newObjectName)
                .WithCopyObjectSource(new CopySourceObjectArgs().WithBucket(storageConfig.DefaultBucketImageStore).WithObject(oldObjectName)));

            // Remove the old object
            await minio.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(storageConfig.DefaultBucketImageStore)
                .WithObject(oldObjectName));
        }
        catch (Exception ex)
        {
            // Handle exceptions, you might want to log or rethrow depending on your needs
            throw new ApplicationException("An error occurred while renaming the file.", ex);
        }
    }
}

public class StorageServiceConfiguration
{
    public string DefaultBucketImageStore { get; set; } = "requests";

    public int ImageLifetimeSeconds { get; set; } = 60;
}