using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Services.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Model.Common;
using VehicleApp.Model;

namespace VehicleApp.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {

        IVehicleMakeRepository _makeRepository;

        IVehicleModelRepository _modelRepository;

        public VehicleMakeService(IVehicleMakeRepository makeRepository, IVehicleModelRepository modelRepository)
        {
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
        }

        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            return await _makeRepository.Add(vehicleMake);
        }

        public async Task<int> Delete(Guid vehicleMakeID)
        {
            return await _makeRepository.Delete(vehicleMakeID);
        }

        public async Task<IVehicleMake> Get(Guid vehicleMakeID)
        {
            var make = await _makeRepository.Get(vehicleMakeID);

            if (make == null)
            {
                return null;
            }

            //var models = await _modelRepository.GetAllModelsFromMake(make.VehicleMakeId);


            //if (models == null)
            //{
            //    return null;
            //}

            //make.VehicleModels = models;

            return make;
        }

        public async Task<ICollection<IVehicleMake>> GetAll()
        {
            return await _makeRepository.GetAll();
        }

        public async Task<int> Update(IVehicleMake vehicleMake)
        {
            return await _makeRepository.Update(vehicleMake);
        }
    }
}
