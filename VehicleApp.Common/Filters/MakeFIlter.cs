using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters.Contracts;

namespace VehicleApp.Common.Filters
{
    public class MakeFilter : IMakeFilter
    {
        public string Search { get; set; }
    }
}
