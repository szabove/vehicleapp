using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public abstract class BaseFilterParams
    {
        //Filtering
        public string Search { get; set; }
        //Sorting
        public string SortBy { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";
        //Pagination
        public int PageNumber { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
    }
}
