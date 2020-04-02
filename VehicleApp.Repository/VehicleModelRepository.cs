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

        public async Task<int> Add(IVehicleModelDomainModel vehicleModel)
        {
            _dbContext.VehicleModel.Add(_mapper.Map<IVehicleModelDomainModel, VehicleModel>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid vehicleModelID)
        {
            var item = await Get(vehicleModelID);
            _dbContext.VehicleModel.Remove(_mapper.Map<IVehicleModelDomainModel, VehicleModel>(item));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IVehicleModelDomainModel> Get(Guid vehicleModelID)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModelID);
            return _mapper.Map<VehicleModel, IVehicleModelDomainModel>(vehicleModel);
        }

        public async Task<ICollection<IVehicleModelDomainModel>> GetAll()
        {
            var vehicleModels = await _dbContext.VehicleModel.ToListAsync();
            return _mapper.Map<ICollection<VehicleModel>, ICollection<IVehicleModelDomainModel>>(vehicleModels);
        }

        public async Task<ICollection<IVehicleModelDomainModel>> GetAllModelsFromMake(Guid vehicleMakeID)
        {
            var vehicleModels = await _dbContext.VehicleModel.Where(v => v.VehicleMake.VehicleMakelId == vehicleMakeID).ToListAsync();
            var mappedVehicleModels = _mapper.Map<List<VehicleModel>, ICollection<IVehicleMakeDomainModel>>(vehicleModels);
            return (ICollection<IVehicleModelDomainModel>)mappedVehicleModels;
        }

        public async Task<int> Update(IVehicleModelDomainModel vehicleModel)
        {
            var _vehicleModel = await Get(vehicleModel.VehicleModelId);
            _dbContext.Entry(_vehicleModel).CurrentValues.SetValues(_mapper.Map<IVehicleModelDomainModel, VehicleModel>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
