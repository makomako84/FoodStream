using Foodstream.Application.Interfaces;
using Foodstream.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodstream.Persistence.Postgre;

public class PointRepository : GenericRepository<Point>, IPointRepository
{
    public PointRepository(PostgreContext context) : base(context)
    {
    }
}
