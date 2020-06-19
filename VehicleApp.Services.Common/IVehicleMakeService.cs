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
    public interface IVehicleMakeService
    {
        Task<int> Add(IVehicleMake vehicleMake);
        Task<IVehicleMake> Get(Guid ID);
        Task<int> Update(Guid ID, IVehicleMake vehicleMake);
        Task<int> Delete(Guid ID);
        Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, ISorter sorter, IPagination pagination);
    }
}
