using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleModelRepository 
    {
        Task<int> AddAsync(IVehicleModel vehicleModel);
        Task<IVehicleModel> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid ID, IVehicleModel vehicleModel);
        Task<int> DeleteAsync(Guid vehicleModelID);
        Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, ISorter sorter, IPagination pagination);
    }
}
