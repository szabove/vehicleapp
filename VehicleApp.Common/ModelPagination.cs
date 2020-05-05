using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public class ModelPagination : IPagination<IVehicleModel>
    {
        public ICollection<IVehicleModel> PaginatedResult(ICollection<IVehicleModel> data, IPagination pagination)
        {
            if (pagination == null)
            {
                return data;
            }

            var itemsToSkip = (pagination.PageNumber - 1) * pagination.PageSize;

            var items = data.Skip(itemsToSkip).Take(pagination.PageSize).ToList();

            return (ICollection<IVehicleModel>)items;
        }
    }
}
