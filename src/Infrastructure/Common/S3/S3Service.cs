using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Foodstream.Application.Interfaces;
using Foodstream.Application;
using Foodstream.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Foodstream.Infrastructure.Common;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _client;
    private readonly S3Options _s3Options;
    public S3Service(
        IAmazonS3 client,
        IOptions<S3Options> s3Options)
    {
        _client = client;
        _s3Options = s3Options.Value;
    }

    public async Task<string> PutFileAsync(
        IFormFile file, string bucketName, KeyBuilder keyBuilder)
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
        PutObjectResponse response = await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<string> PutFileAsync( 
        byte[] rawData, string bucketName, KeyBuilder keyBuilder) 
    {
        using var memoryStream = new MemoryStream(rawData);
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = memoryStream
        };
        PutObjectResponse response = await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<string> PutFileAsync(Stream stream, string bucketName, KeyBuilder keyBuilder)
    {
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream
        };
        await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<byte[]> GetFileAsync(string bucketName, string key)
    {
        GetObjectRequest request = new GetObjectRequest()
        {
            BucketName = bucketName,
            Key = key
        };                        
        using var response = await _client.GetObjectAsync(request);
        using var result = new MemoryStream();
        await response.ResponseStream.CopyToAsync(result);
        return result.ToArray();
    }

    public async Task DeleteFileAsync(string bucketName, string key)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };            
        await _client.DeleteObjectAsync(deleteObjectRequest);
    }
}