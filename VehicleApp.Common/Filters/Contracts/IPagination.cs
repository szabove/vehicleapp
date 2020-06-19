using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters.Contracts
{
    public interface IPagination
    {
        int RecordsPerPage { get; set; }
        int PageNumber { get; set; }
        IQueryable<T> GetPaginationQuery<T>(IQueryable<T> data, int pageSize, int pageNumber) where T : class;
    }
}
