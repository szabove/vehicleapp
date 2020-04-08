using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Services.Common
{
    public interface IVehicleModelService
    {
        Task<ICollection<IVehicleModel>> GetAll();
        Task<IVehicleModel> Get(Guid vehicleModelID);
        Task<int> Add(IVehicleModel vehicleModel);
        Task<int> Update(IVehicleModel vehicleModel);
        Task<int> Delete(Guid vehicleModelID);
    }
}
