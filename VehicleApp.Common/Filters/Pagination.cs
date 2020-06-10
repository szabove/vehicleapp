using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public class Pagination : IPagination
    {
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }

        public IEnumerable<T> GetPaginatedData<T>(IEnumerable<T> data, int pageSize, int pageNumber) where T : class
        {
            var itemsToSkip = (pageNumber - 1) * pageSize;

            var items = data.Skip(itemsToSkip).Take(pageSize);

            return items;
        }
    }
}
