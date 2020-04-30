using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IVehicleMake> Get(Guid id);
        Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination pagination);
        Task<int> Add(IVehicleMake vehicleMake);
        Task<int> Update(Guid ID, IVehicleMake vehicleMake);
        Task<int> Delete(Guid id);
    }
}
