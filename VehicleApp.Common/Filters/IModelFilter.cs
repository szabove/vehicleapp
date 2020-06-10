using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public interface IModelFilter 
    {
        string Search { get; set; }
        Guid VehicleMakeId { get; set; }
    }
}
