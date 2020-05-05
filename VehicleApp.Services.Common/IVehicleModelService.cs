using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IVehicleModelService
    {
        Task<IVehicleModel> Get(Guid vehicleModelID);
        Task<int> Add(IVehicleModel vehicleModel);
        Task<int> Update(Guid ID, IVehicleModel vehicleModel);
        Task<int> Delete(Guid vehicleModelID);
        Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination pagination, ISorter<IVehicleModel> sorter);
    }
}
