using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Services.Common;

namespace VehicleApp.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        IVehicleModelRepository _vehicleModelRepository;

        public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
        }

        public async Task<int> Add(IVehicleModel vehicleModel)
        {
            return await _vehicleModelRepository.Add(vehicleModel);
        }

        public async  Task<int> Delete(Guid vehicleModelID)
        {
            return await _vehicleModelRepository.Delete(vehicleModelID);
        }

        public async Task<IVehicleModel> Get(Guid vehicleModelID)
        {
            var models = await _vehicleModelRepository.Get(vehicleModelID);

            if (models == null)
            {
                throw new Exception("A vehicle model with a given id does not exist!");
            }

            return models;
        }

        public async Task<ICollection<IVehicleModel>> GetAllModelsFromMake(Guid vehicleMakeID, string abc = "")
        {
            return await _vehicleModelRepository.GetAllModelsFromMake(vehicleMakeID, abc);
        }

        public async Task<int> Update(IVehicleModel vehicleModel)
        {
            return await _vehicleModelRepository.Update(vehicleModel);
        }
    }
}
