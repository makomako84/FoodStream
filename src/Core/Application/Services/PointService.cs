using MakoSystems.Foodstream.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace MakoSystems.Foodstream.Application;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    private readonly IS3Service _s3Service;
    private readonly ApplicationSettings _applicationSettings;

    public PointService(
        IPointRepository pointRepository,
        IS3Service s3Service,
        IOptions<ApplicationSettings> appSettings)
    {
        _pointRepository = pointRepository;
        _s3Service = s3Service;
        _applicationSettings = appSettings.Value;
    }

    public async Task<PointDTO> AddAsync(string address)
    {
        var newPoint = new Point()
        {
            Address = address,
        };
        var result = await _pointRepository.AddAsync(newPoint);
        var response = new PointDTO()
        {
            Id = result.Id,
            Address = result.Address
        };
        return response;
    }

    public async Task<FileDownloadDTO> DownloadFileAsync(string key)
    {
        return new FileDownloadDTO
        {
            File = await _s3Service.GetFileAsync(key),
            FileName = "that file"
        };
    }

    public async Task<PointDTO> GetAsync(int id)
    {
        var result = await _pointRepository.GetAsync(id);
        return new PointDTO()
        {
            Id = result.Id,
            Address = result.Address
        };
    }

    public async Task<IEnumerable<PointDTO>> ListAsync()
    {
        var result = await _pointRepository.ListAsync();
        return result.Select(p => new PointDTO() { Id = p.Id, Address = p.Address });
    }

    public async Task<PointDTO> UpdateAsync(int id, string address)
    {
        var existingItem = await _pointRepository.GetAsync(id);
        existingItem.Address = address;
        var result = await _pointRepository.UpdateAsync(existingItem);
        return new PointDTO()
        {
            Id = id,
            Address = result.Address
        };
    }

    public async Task<string> UploadfileAsync(int id, IFormFile file)
    {
        var originalName = GetNewFileName("somename", file);
        var key = await _s3Service.PutFileAsync(file, 
            new KeyBuilder(_applicationSettings.PointPrefix, originalName, true));
        return key;
    }

    private string GetNewFileName(string newName, IFormFile formFile) =>
        Regex.Replace(newName, @"[^a-zA-Z0-9А-Яа-я_ ]+", "", RegexOptions.Compiled).Replace(' ', '_') +
        "_" +
        DateTime.Now.ToString("yyyyMMdd_Hmmss") +
        System.IO.Path.GetExtension(formFile.FileName);
}
