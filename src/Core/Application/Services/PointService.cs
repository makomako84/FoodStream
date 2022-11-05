using Amazon.S3;
using Foodstream.Application.DTO;
using Foodstream.Application.Interfaces;
using Foodstream.Domain;
using Foodstream.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Foodstream.Application.Configuration;
using System.Text.RegularExpressions;

namespace Foodstream.Application.Services;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;
    private readonly IAmazonS3 _s3Service;
    private readonly IOptions<S3Options> _s3Options;

    public PointService(
        IPointRepository pointRepository,
        IAmazonS3 s3Service,
        IOptions<S3Options> s3Options)
    {
        _pointRepository = pointRepository;
        _s3Service = s3Service;
        _s3Options = s3Options;
    }

    public async Task<PointResponse> AddAsync(string address)
    {
        var newPoint = new Point()
        {
            Address = address,
        };
        var result = await _pointRepository.AddAsync(newPoint);
        var response = new PointResponse()
        {
            Id = result.Id,
            Address = result.Address
        };
        return response;
    }

    public async Task<FileDownloadResponse> DownloadFileAsync(string key)
    {
        return new FileDownloadResponse
        {
            File = await S3Helper.GetFileAsync(_s3Service, _s3Options.Value.BucketName, key),
            FileName = "that file"
        };
    }

    public async Task<PointResponse> GetAsync(int id)
    {
        var result = await _pointRepository.GetAsync(id);
        return new PointResponse()
        {
            Id = result.Id,
            Address = result.Address
        };
    }

    public async Task<IEnumerable<PointResponse>> ListAsync()
    {
        var result = await _pointRepository.ListAsync();
        return result.Select(p => new PointResponse() { Id = p.Id, Address = p.Address });
    }

    public async Task<PointResponse> UpdateAsync(int id, string address)
    {
        var existingItem = await _pointRepository.GetAsync(id);
        existingItem.Address = address;
        var result = await _pointRepository.UpdateAsync(existingItem);
        return new PointResponse()
        {
            Id = id,
            Address = result.Address
        };
    }

    public async Task<string> UploadfileAsync(int id, IFormFile file)
    {
        var originalName = GetNewFileName("somename", file);
        var key = await S3Helper.PutFileAsync(_s3Service, file, _s3Options.Value.BucketName,
          new S3KeyBuilder(_s3Options.Value.PointPrefix, originalName, true));
        return key;
    }

    private string GetNewFileName(string newName, IFormFile formFile) =>
        Regex.Replace(newName, @"[^a-zA-Z0-9А-Яа-я_ ]+", "", RegexOptions.Compiled).Replace(' ', '_') +
        "_" +
        DateTime.Now.ToString("yyyyMMdd_Hmmss") +
        System.IO.Path.GetExtension(formFile.FileName);
}
