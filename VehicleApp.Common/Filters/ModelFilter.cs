using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters.Contracts;

namespace VehicleApp.Common.Filters
{
    public class ModelFilter : IModelFilter
    {
        public string Search { get; set; }
        public Guid VehicleMakeId { get; set; }
    }
}
