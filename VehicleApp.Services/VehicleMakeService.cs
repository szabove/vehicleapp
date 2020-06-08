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
using AutoMapper;

namespace VehicleApp.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        IVehicleMakeRepository VehicleMakeRepository;

        IMapper Mapper;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            VehicleMakeRepository = vehicleMakeRepository;
            Mapper = mapper;
        }

        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            if (vehicleMake.Id == Guid.Empty)
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

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter)
        {
            if (filter == null)
            {
                return null;
            }

            return await VehicleMakeRepository.FindAsync(filter, pagination, sorter);
        }

        public async Task<int> Update(Guid id, IVehicleMake vehicleMake)
        {
            if (id == Guid.Empty || vehicleMake.Id == Guid.Empty)
            {
                return 0;
            }

            return await VehicleMakeRepository.UpdateAsync(id, vehicleMake);
        }
    }
}
