using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _dbContext;
        IMapper _mapper;

        public VehicleModelRepository(VehicleContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(IVehicleModel vehicleModel)
        {
            _dbContext.VehicleModel.Add(_mapper.Map<IVehicleModel, VehicleModelEntity>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid vehicleModelID)
        {
            var item = await Get(vehicleModelID);
            _dbContext.VehicleModel.Remove(_mapper.Map<IVehicleModel, VehicleModelEntity>(item));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IVehicleModel> Get(Guid vehicleModelID)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModelID);
            return _mapper.Map<VehicleModelEntity, IVehicleModel>(vehicleModel);
        }

        public async Task<ICollection<IVehicleModel>> GetAll()
        {
            var vehicleModels = await _dbContext.VehicleModel.ToListAsync();
            return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);
        }

        public async Task<ICollection<IVehicleModel>> GetAllModelsFromMake(Guid vehicleMakeID)
        {
            var vehicleModels = await _dbContext.VehicleModel.Where(v => v.VehicleMake.VehicleMakeId == vehicleMakeID).ToListAsync();
            if (vehicleModels == null)
            {
                return null;
            }
            var mappedVehicleModels = _mapper.Map<List<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);
            return (ICollection<IVehicleModel>)mappedVehicleModels;
        }

        public async Task<int> Update(IVehicleModel vehicleModel)
        {
            var _vehicleModel = await Get(vehicleModel.VehicleModelId);
            _dbContext.Entry(_vehicleModel).CurrentValues.SetValues(_mapper.Map<IVehicleModel, VehicleModelEntity>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
