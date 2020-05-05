using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
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
                return null;
            }

            return models;
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination pagination, ISorter<IVehicleModel> sorter)
        {
            return await _vehicleModelRepository.FindAsync(filter, pagination, sorter);
        }

        public async Task<int> Update(Guid ID, IVehicleModel vehicleModel)
        {
            return await _vehicleModelRepository.Update(ID,vehicleModel);
        }
    }
}
