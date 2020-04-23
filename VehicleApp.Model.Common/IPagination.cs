using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model.Common
{
    public interface IPagination
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
