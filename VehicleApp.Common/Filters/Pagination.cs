using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters.Contracts;

namespace VehicleApp.Common.Filters
{
    public class Pagination : IPagination
    {
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }

        public IQueryable<T> GetPaginationQuery<T>(IQueryable<T> data, int pageSize, int pageNumber) where T : class
        {
            var itemsToSkip = (pageNumber - 1) * pageSize;

            var items = data.Skip(itemsToSkip).Take(pageSize);

            return items;
        }
    }
}
