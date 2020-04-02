﻿using System;
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

        public async Task<int> Add(IVehicleModelDomainModel vehicleModel)
        {
            return await _vehicleModelRepository.Add(vehicleModel);
        }

        public async  Task<int> Delete(Guid vehicleModelID)
        {
            return await _vehicleModelRepository.Delete(vehicleModelID);
        }

        public async Task<IVehicleModelDomainModel> Get(Guid vehicleModelID)
        {
            return await _vehicleModelRepository.Get(vehicleModelID);
        }

        public async Task<ICollection<IVehicleModelDomainModel>> GetAll()
        {
            return await _vehicleModelRepository.GetAll();
        }

        public async Task<int> Update(IVehicleModelDomainModel vehicleModel)
        {
            return await _vehicleModelRepository.Update(vehicleModel);
        }
    }
}