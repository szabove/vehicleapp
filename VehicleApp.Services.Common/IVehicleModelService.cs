using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Common.Filters.Contracts;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IVehicleModelService
    {
        Task<int> Add(IVehicleModel vehicleModel);
        Task<IVehicleModel> Get(Guid ID);
        Task<int> Update(Guid ID, IVehicleModel vehicleModel);
        Task<int> Delete(Guid ID);
        Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, ISorter sorter, IPagination pagination);
    }
}
