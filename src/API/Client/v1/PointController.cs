using Microsoft.AspNetCore.Mvc;
using Foodstream.Application.Interfaces;
using Foodstream.Application.DTO;

namespace Foodstream.API.Client;

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
        return await _pointService.GetAsync(id);
    }

    [HttpGet("list")]
    public async Task<IEnumerable<PointResponse>> ListAsync()
    {
        return await _pointService.ListAsync();
    }

    [HttpPost("add")]
    public async Task<PointResponse> AddAsync([FromQuery]string address)
    {
        return await _pointService.AddAsync(address);
    }

    [HttpPost("update")]
    public async Task<PointResponse> UpdateAsync([FromQuery]int id, string address)
    {
        return await _pointService.UpdateAsync(id, address);
    }
}
