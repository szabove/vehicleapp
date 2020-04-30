using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IVehicleMakeService
    {
        Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination pagination);
        Task<IVehicleMake> Get(Guid vehicleMakeID);
        Task<int> Add(IVehicleMake vehicleMake);
        Task<int> Update(Guid ID, IVehicleMake vehicleMake);
        Task<int> Delete(Guid vehicleMakeID);
    }
}
