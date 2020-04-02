﻿using System;
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

        public async Task<int> Add(IVehicleMakeDomainModel vehicleMake)
        {
            return await _makeRepository.Add(vehicleMake);
        }

        public async Task<int> Delete(Guid vehicleMakeID)
        {
            return await _makeRepository.Delete(vehicleMakeID);
        }

        public async Task<IVehicleMakeDomainModel> Get(Guid vehicleMakeID)
        {
            var make = await _makeRepository.Get(vehicleMakeID);
            var models = await _modelRepository.GetAllModelsFromMake(make.VehicleMakeId);
            make.VehicleModel = models;
            return make;
        }

        public async Task<ICollection<IVehicleMakeDomainModel>> GetAll()
        {
            return await _makeRepository.GetAll();
        }

        public async Task<int> Update(IVehicleMakeDomainModel vehicleMake)
        {
            return await _makeRepository.Update(vehicleMake);
        }
    }
}