using AutoMapper;
using System;
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
        IVehicleModelRepository VehicleModelRepository;
        IMapper Mapper;

        public VehicleModelService(IVehicleModelRepository vehicleModelRepository, IMapper mapper)
        {
            VehicleModelRepository = vehicleModelRepository;
            Mapper = mapper;
        }

        public async Task<int> Add(IVehicleModel vehicleModel)
        {
            if (vehicleModel == null)
            {
                return 0;
            }
            return await VehicleModelRepository.AddAsync(vehicleModel);
        }

        public async  Task<int> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            return await VehicleModelRepository.DeleteAsync(id);
        }

        public async Task<IVehicleModel> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return Mapper.Map<IVehicleModel>(await VehicleModelRepository.GetAsync(id));
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, ISorter sorter, IPagination pagination)
        {
            if (filter == null ||
                sorter == null ||
                pagination == null)
            {
                return null;
            }

            return await VehicleModelRepository.FindAsync(filter, sorter, pagination);
        }

        public async Task<int> Update(Guid id, IVehicleModel vehicleModel)
        {
            if (id == Guid.Empty || vehicleModel == null)
            {
                return 0;
            }

            return await VehicleModelRepository.UpdateAsync(id, vehicleModel);
        }
    }
}
