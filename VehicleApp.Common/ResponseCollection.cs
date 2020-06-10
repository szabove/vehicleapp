using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common
{
    public class ResponseCollection<T>
    {
        //public ResponseCollection()
        //{
        //}

        public ResponseCollection(IEnumerable<T> data, int pagenumber, int pagesize)
        {
            Data = data;
            PageNumber = pagenumber;
            PageSize = pagesize;
        }

        public IEnumerable<T> Data { get; set; }

        public int? PageNumber { get; set; } = 1;

        public int? PageSize { get; set; } = 10;
    }
}
