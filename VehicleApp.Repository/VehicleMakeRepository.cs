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
        
        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            _dbContext.VehicleMake.Add(_mapper.Map<IVehicleMake, VehicleMakeEntity>(vehicleMake));
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            if (id == null)
            {
                return 0;
            }

            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return 0;
            }

            _dbContext.VehicleMake.Remove(vehicleMake);
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<IVehicleMake> Get(Guid id)
        {
            var vehicleMake = await _dbContext.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return null;
            }
            return _mapper.Map<IVehicleMake>(vehicleMake);
        }

        public async Task<ICollection<IVehicleMake>> GetAllSorted(string abc ="")
        {

            ICollection<VehicleMakeEntity> vehicleMakes = null;
            switch (abc.ToLower())
            {
                case "asc":
                    vehicleMakes = await _dbContext.VehicleMake.OrderBy(x => x.Name).ToListAsync();
                    return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);

                case "desc":
                    vehicleMakes = await _dbContext.VehicleMake.OrderByDescending(x => x.Name).ToListAsync();
                    return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);

                default:
                    vehicleMakes = await _dbContext.VehicleMake.ToListAsync();
                    return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);
                    
            }


            //vehicleMakes = await _dbContext.VehicleMake.ToListAsync();
            //return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);
        }

        public async Task<int> Update(IVehicleMake vehicleMake)
        {
            var _vehicleMake = await _dbContext.VehicleMake.FindAsync(vehicleMake.VehicleMakeId);
            if (_vehicleMake == null)
            {
                return 0;
            }
            _dbContext.Entry(_vehicleMake).CurrentValues.SetValues(_mapper.Map<IVehicleMake, VehicleMakeEntity>(vehicleMake));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
