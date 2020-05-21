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
        Task<int> Add(IVehicleMake vehicleMake);
        Task<IVehicleMake> Get(Guid id);
        Task<int> Update(Guid ID, IVehicleMake vehicleMake);
        Task<int> Delete(Guid id);
        Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter);
    }
}
