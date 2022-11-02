using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodstream.Application.DTO;

namespace Foodstream.Application.Interfaces;

public interface IPointService
{
    PointResponse Add(string address);
    PointResponse Update(int id, string address);
    List<PointResponse> List();
}
