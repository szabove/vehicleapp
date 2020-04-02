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
        Task<ICollection<IVehicleModelDomainModel>> GetAll();
        Task<IVehicleModelDomainModel> Get(Guid vehicleModelID);
        Task<int> Add(IVehicleModelDomainModel vehicleModel);
        Task<int> Update(IVehicleModelDomainModel vehicleModel);
        Task<int> Delete(Guid vehicleModelID);
    }
}
