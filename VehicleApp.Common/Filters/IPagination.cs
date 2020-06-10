using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public interface IPagination
    {
        int RecordsPerPage { get; set; }
        int PageNumber { get; set; }
        IEnumerable<T> GetPaginatedData<T>(IEnumerable<T> data, int pageSize, int pageNumber) where T : class;
    }
}
