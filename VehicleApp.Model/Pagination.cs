using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Model
{
    public class Pagination : IPagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
