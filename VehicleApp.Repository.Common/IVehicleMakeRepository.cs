using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleMakeRepository
        {
        Task<int> AddAsync(IVehicleMake vehicleMake);
        Task<IVehicleMake> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid ID, IVehicleMake vehicleMake);
        Task<int> DeleteAsync(Guid id);
        Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter);
    }
}
