using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public interface IMakeFilter
    {
        string Search { get; set; }
        Expression<Func<IVehicleMake, bool>> GetFilterQuery();
    }
}
