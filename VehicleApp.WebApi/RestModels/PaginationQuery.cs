using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.RestModels
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationQuery(int pagenumber, int pagesize)
        {
            PageNumber = pagenumber;
            PageSize = pagesize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}