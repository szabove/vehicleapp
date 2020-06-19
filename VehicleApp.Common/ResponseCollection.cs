using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common
{
    public class ResponseCollection<T>
    {
        public ResponseCollection(ICollection<T> data, int pagenumber, int recordsPerPage)
        {
            Data = data;
            PageNumber = pagenumber;
            RecordsPerPage = recordsPerPage;
        }

        public ICollection<T> Data { get; set; }

        public int? PageNumber { get; set; } = 1;

        public int? RecordsPerPage { get; set; } = 10;
    }
}
