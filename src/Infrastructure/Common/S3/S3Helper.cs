using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Foodstream.Infrastructure.Common;

public static class S3Helper
{
    public static async Task<string> PutFileAsync(IAmazonS3 client, 
        IFormFile file, string bucketName, S3KeyBuilder keyBuilder)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = memoryStream
        };
        PutObjectResponse response = await client.PutObjectAsync(putRequest);
        return key;
    }

    public static async Task<string> PutFileAsync(IAmazonS3 client, 
        byte[] rawData, string bucketName, S3KeyBuilder keyBuilder) 
    {
        using var memoryStream = new MemoryStream(rawData);
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = memoryStream
        };
        PutObjectResponse response = await client.PutObjectAsync(putRequest);
        return key;
    }

    public static async Task<string> PutFileAsync(IAmazonS3 client, Stream stream, string bucketName, S3KeyBuilder keyBuilder)
    {
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream
        };
        await client.PutObjectAsync(putRequest);
        return key;
    }

    public static async Task<byte[]> GetFileAsync(IAmazonS3 client, string bucketName, string key)
    {
        GetObjectRequest request = new GetObjectRequest()
        {
            BucketName = bucketName,
            Key = key
        };                        
        using var response = await client.GetObjectAsync(request);
        using var result = new MemoryStream();
        await response.ResponseStream.CopyToAsync(result);
        return result.ToArray();
    }

    public static async Task DeleteFileAsync(IAmazonS3 client, string bucketName, string key)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };            
        await client.DeleteObjectAsync(deleteObjectRequest);
    }

    public static async Task CreateFolderAsync(IAmazonS3 client, string bucketName, string key)
    {
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key
        };
        await client.PutObjectAsync(putRequest);
    }

    public static async Task<List<S3Object>> GetFolderAsync(IAmazonS3 client, string bucketName, string prefix)
    {
        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
            Prefix = prefix,
            StartAfter = prefix
        };

        var response = await client.ListObjectsV2Async(request);

        return response.S3Objects;
    }

    public static async Task CreateBucketAsync(IAmazonS3 client, string bucketName)
    {
        PutBucketRequest request = new()
        {
            BucketName = bucketName
        };
        await client.PutBucketAsync(request);
    }
}