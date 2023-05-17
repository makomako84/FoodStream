using Microsoft.AspNetCore.Http;

namespace MakoSystems.Foodstream.Application;

public interface IS3Service
{
    Task<string> PutFileAsync(
        IFormFile file, KeyBuilder keyBuilder);

    Task<string> PutFileAsync(
        byte[] rawData, KeyBuilder keyBuilder);

    Task<string> PutFileAsync(
        Stream stream, KeyBuilder keyBuilder);


    Task<byte[]> GetFileAsync(string key);

    Task DeleteFileAsync(string key);
}
