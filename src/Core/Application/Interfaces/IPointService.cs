﻿using Foodstream.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Foodstream.Application.Interfaces;

public interface IPointService
{
    Task<PointDTO> AddAsync(string address);
    Task<PointDTO> UpdateAsync(int id, string address);
    Task<PointDTO> GetAsync(int id);
    Task<IEnumerable<PointDTO>> ListAsync();
    Task<string> UploadfileAsync(int id, IFormFile file);
    Task<FileDownloadDTO> DownloadFileAsync(string key);
}
