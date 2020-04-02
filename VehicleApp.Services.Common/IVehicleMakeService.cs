using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IVehicleMakeService
    {
        Task<ICollection<IVehicleMakeDomainModel>> GetAll();
        Task<IVehicleMakeDomainModel> Get(Guid vehicleMakeID);
        Task<int> Add(IVehicleMakeDomainModel vehicleMake);
        Task<int> Update(IVehicleMakeDomainModel vehicleMake);
        Task<int> Delete(Guid vehicleMakeID);
    }
}
