using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleModelRepository
    {
        Task<IVehicleModelDomainModel> Get(Guid vehicleModelID);
        Task<ICollection<IVehicleModelDomainModel>> GetAll();
        Task<int> Add(IVehicleModelDomainModel vehicleModel);
        Task<int> Update(IVehicleModelDomainModel vehicleModel);
        Task<int> Delete(Guid vehicleModelID);
        Task<ICollection<IVehicleModelDomainModel>> GetAllModelsFromMake(Guid vehicleMakeID);

    }
}
