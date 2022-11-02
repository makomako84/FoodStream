using Foodstream.Application.DTO;
using Foodstream.Application.Interfaces;
using Foodstream.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodstream.Application.Services;

public class PointService : IPointService
{
    private readonly IPointRepository _pointRepository;

    public PointService(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
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
}
