using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IVehicleMakeDomainModel> Get(Guid id);
        Task<ICollection<IVehicleMakeDomainModel>> GetAll();
        Task<int> Add(IVehicleMakeDomainModel vehicleMake);
        Task<int> Update(IVehicleMakeDomainModel vehicleMake);
        Task<int> Delete(Guid id);
    }
}
