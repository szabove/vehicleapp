using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Services.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Model.Common;
using VehicleApp.Model;
using VehicleApp.Common;

namespace VehicleApp.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
         
        IVehicleMakeRepository _makeRepository;

        public VehicleMakeService(IVehicleMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
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
            return make;
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination pagination)
        {
            return await _makeRepository.FindAsync(filter, pagination);
        }

        public async Task<int> Update(Guid ID, IVehicleMake vehicleMake)
        {
            return await _makeRepository.Update(ID, vehicleMake);
        }
    }
}
