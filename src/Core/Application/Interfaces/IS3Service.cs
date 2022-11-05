using Microsoft.AspNetCore.Http;

namespace Foodstream.Application.Interfaces;

public interface IS3Service
{
    Task<string> PutFileAsync(
        IFormFile file, string bucketName, KeyBuilder keyBuilder);

    Task<string> PutFileAsync(
        byte[] rawData, string bucketName, KeyBuilder keyBuilder);

    Task<string> PutFileAsync(
        Stream stream, string bucketName, KeyBuilder keyBuilder);


    Task<byte[]> GetFileAsync(string bucketName, string key);

    Task DeleteFileAsync(string bucketName, string key);
}
