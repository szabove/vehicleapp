using System;
using System.Threading.Tasks;
using VehicleApp.Services.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Model.Common;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Common.Filters.Contracts;

namespace VehicleApp.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        IVehicleMakeRepository VehicleMakeRepository;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            VehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            if (vehicleMake == null)
            {
                return 0;
            }
            return await VehicleMakeRepository.AddAsync(vehicleMake);
        }

        public async Task<int> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            return await VehicleMakeRepository.DeleteAsync(id); 
        }

        public async Task<IVehicleMake> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await VehicleMakeRepository.GetAsync(id);
        }


        public async Task<int> Update(Guid id, IVehicleMake vehicleMake)
        {
            if (id == Guid.Empty || vehicleMake == null)
            {
                return 0;
            }

            return await VehicleMakeRepository.UpdateAsync(id, vehicleMake);
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, ISorter sorter, IPagination pagination)
        {
            return await VehicleMakeRepository.FindAsync(filter, sorter, pagination);
        }
    }
}
