using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public class MakePagination : IPagination<IVehicleMake>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ICollection<IVehicleMake> PaginatedResult(ICollection<IVehicleMake> data, int pageSize, int pageNumber)
        {
            var itemsToSkip = (pageNumber - 1) * pageSize;

            var items = data.Skip(itemsToSkip).Take(pageSize).ToList();

            return (ICollection<IVehicleMake>)items;
        }
    }
}
