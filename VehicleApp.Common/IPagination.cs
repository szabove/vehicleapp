using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public interface IPagination<T> where T : class
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        ICollection<T> PaginatedResult(ICollection<T> data, int pageSize, int pageNumber);
    }
}
