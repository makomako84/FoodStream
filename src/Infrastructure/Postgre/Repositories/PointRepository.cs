using MakoSystems.Foodstream.Application;
using MakoSystems.Foodstream.Domain;

namespace MakoSystems.Foodstream.Postgre;

public class PointRepository : GenericRepository<Point>, IPointRepository
{
    public PointRepository(PostgreContext context) : base(context)
    {
    }
}
