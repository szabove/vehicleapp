using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        VehicleContext _dbContext;
        IMapper _mapper;
        public VehicleMakeRepository(VehicleContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(IVehicleMakeDomainModel vehicleMake)
        {
            _dbContext.VehicleMake.Add(_mapper.Map<IVehicleMakeDomainModel, VehicleMake>(vehicleMake));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var item = await Get(id);//_dbContext.VehicleMake.Single(v => v.VehicleMakelId == id);
            _dbContext.VehicleMake.Remove(_mapper.Map<IVehicleMakeDomainModel, VehicleMake>(item));
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<IVehicleMakeDomainModel> Get(Guid id)
        {
            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);
            return _mapper.Map<VehicleMake, IVehicleMakeDomainModel>(vehicleMake);
        }

        public async Task<ICollection<IVehicleMakeDomainModel>> GetAll()
        {
            var vehicleMakes = await _dbContext.VehicleMake.ToListAsync();
            return _mapper.Map<ICollection<VehicleMake>, ICollection<IVehicleMakeDomainModel>>(vehicleMakes);
        }

        public async Task<int> Update(IVehicleMakeDomainModel vehicleMake)
        {
            var _vehicleMake = await Get(vehicleMake.VehicleMakeId);
            _dbContext.Entry(_vehicleMake).CurrentValues.SetValues(_mapper.Map<IVehicleMakeDomainModel, VehicleMake>(vehicleMake));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
