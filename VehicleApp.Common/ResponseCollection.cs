using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common
{
    public class ResponseCollection<T>
    {
        public ResponseCollection()
        {
        }

        public ResponseCollection(ICollection<T> data)
        {
            Data = data;
        }

        public ICollection<T> Data { get; set; }

        public int? PageNumber { get; set; } = 1;

        public int? PageSize { get; set; } = 10;

        public void SetPagingParams(int pagenumber, int pagesize)
        {
            PageNumber = pagenumber;
            PageSize = pagesize;
        }
    }
}
