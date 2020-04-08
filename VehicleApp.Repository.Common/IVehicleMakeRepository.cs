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
        Task<IVehicleMake> Get(Guid id);
        Task<ICollection<IVehicleMake>> GetAll();
        Task<int> Add(IVehicleMake vehicleMake);
        Task<int> Update(IVehicleMake vehicleMake);
        Task<int> Delete(Guid id);
    }
}
