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
        Task<IVehicleModel> Get(Guid vehicleModelID);
        //Task<ICollection<IVehicleModel>> GetAllSorted(string abc ="");
        Task<int> Add(IVehicleModel vehicleModel);
        Task<int> Update(IVehicleModel vehicleModel);
        Task<int> Delete(Guid vehicleModelID);
        Task<ICollection<IVehicleModel>> GetAllModelsFromMake(Guid vehicleMakeID, string abc = "");

    }
}
