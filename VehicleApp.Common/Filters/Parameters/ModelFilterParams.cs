using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters.Parameters;

namespace VehicleApp.Common.Filters.Parameters
{
    public class ModelFilterParams : BaseFilterParams
    {
        public Guid VehicleMakeId { get; set; }
    }
}
