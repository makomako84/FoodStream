using Foodstream.Application.DTO;

namespace Foodstream.Application.Interfaces;

public interface IPointService
{
    Task<PointResponse> AddAsync(string address);
    Task<PointResponse> UpdateAsync(int id, string address);
    Task<PointResponse> GetAsync(int id);
    Task<IEnumerable<PointResponse>> ListAsync();
}
