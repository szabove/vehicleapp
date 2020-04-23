using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;

namespace VehicleApp.Services
{
    public class VehicleMakePaginationService : IPaginationService<IVehicleMake>
    {
        public ICollection<IVehicleMake> PaginatedResult(ICollection<IVehicleMake> data, IPagination pagination)
        {
            if (pagination == null)
            {
                return data;
            }

            var itemsToSkip = (pagination.PageNumber - 1) * pagination.PageSize;

            var items = data.Skip(itemsToSkip).Take(pagination.PageSize).ToList();

            return (ICollection<IVehicleMake>)items;
        }
    }
}
