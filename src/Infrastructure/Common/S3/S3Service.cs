using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using MakoSystems.Foodstream.Application;
using Microsoft.Extensions.Options;

namespace MakoSystems.Foodstream.Common;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _client;
    private readonly InfrastractureSettings _s3Options;
    public S3Service(
        IAmazonS3 client,
        IOptions<InfrastractureSettings> s3Options)
    {
        _client = client;
        _s3Options = s3Options.Value;
    }

    public async Task<string> PutFileAsync(
        IFormFile file, KeyBuilder keyBuilder)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = _s3Options.S3BucketName,
            Key = key,
            InputStream = memoryStream
        };
        PutObjectResponse response = await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<string> PutFileAsync( 
        byte[] rawData, KeyBuilder keyBuilder) 
    {
        using var memoryStream = new MemoryStream(rawData);
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = _s3Options.S3BucketName,
            Key = key,
            InputStream = memoryStream
        };
        PutObjectResponse response = await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<string> PutFileAsync(Stream stream, KeyBuilder keyBuilder)
    {
        string key = keyBuilder.Key;
        var putRequest = new PutObjectRequest()
        {
            BucketName = _s3Options.S3BucketName,
            Key = key,
            InputStream = stream
        };
        await _client.PutObjectAsync(putRequest);
        return key;
    }

    public async Task<byte[]> GetFileAsync(string key)
    {
        GetObjectRequest request = new GetObjectRequest()
        {
            BucketName = _s3Options.S3BucketName,
            Key = key
        };                        
        using var response = await _client.GetObjectAsync(request);
        using var result = new MemoryStream();
        await response.ResponseStream.CopyToAsync(result);
        return result.ToArray();
    }

    public async Task DeleteFileAsync(string key)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _s3Options.S3BucketName,
            Key = key
        };            
        await _client.DeleteObjectAsync(deleteObjectRequest);
    }
}