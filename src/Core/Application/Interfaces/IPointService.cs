using Foodstream.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Foodstream.Application.Interfaces;

public interface IPointService
{
    Task<PointResponse> AddAsync(string address);
    Task<PointResponse> UpdateAsync(int id, string address);
    Task<PointResponse> GetAsync(int id);
    Task<IEnumerable<PointResponse>> ListAsync();
    Task<string> UploadfileAsync(int id, IFormFile file);
    Task<FileDownloadResponse> DownloadFileAsync(string key);
}
