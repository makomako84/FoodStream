using Microsoft.AspNetCore.Mvc;
using Foodstream.Application.Interfaces;
using Foodstream.Presentation.Contracts;

namespace Foodstream.Presentation.App;

[ApiController]
[Route("[controller]")]
public class PointController : ControllerBase
{
    private readonly IPointService _pointService;

    public PointController(IPointService pointService)
    {
        _pointService = pointService;
    }

    [HttpGet("get")]
    public async Task<PointResponse> GetAsync(int id)
    {
        var result = await _pointService.GetAsync(id);
        return new PointResponse()
        {
            Id = result.Id,
            Address = result.Address
        };
    }

    [HttpGet("list")]
    public async Task<IEnumerable<PointResponse>> ListAsync()
    {
        var result = await _pointService.ListAsync();
        return result.Select(p => new PointResponse());
    }

    [HttpPost("add")]
    public async Task<PointResponse> AddAsync([FromQuery]string address)
    {
        var result = await _pointService.AddAsync(address);
        return new PointResponse()
        {
            Id = result.Id,
            Address = result.Address
        };
    }

    [HttpPost("update")]
    public async Task<PointResponse> UpdateAsync([FromQuery]int id, string address)
    {
        var result = await _pointService.UpdateAsync(id, address);
        return new PointResponse()
        {
            Id = result.Id,
            Address = result.Address
        };
    }

    [HttpPost("upload")]
    public async Task<string> UploadfileAsync(int id, IFormFile file)
    {
        return await _pointService.UploadfileAsync(id, file);
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile(string key)
    {
        var document = await _pointService.DownloadFileAsync(key);
        return File(document.File, "application/octet-stream", document.FileName);
    }
}
