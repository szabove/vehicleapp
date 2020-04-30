using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        public async Task<int> Delete(Guid vehicleModelID)
        {
            if (vehicleModelID == null)
            {
                return 0;
            }

            var vehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModelID);

            if (vehicleModel == null)
            {
                return 0;
            }

            _dbContext.VehicleModel.Remove(vehicleModel);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IVehicleModel> Get(Guid vehicleModelID)
        {
            var vehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModelID);
            if (vehicleModel == null)
            {
                return null;
            }
            return _mapper.Map<VehicleModelEntity, IVehicleModel>(vehicleModel);
        }

        //public async Task<ICollection<IVehicleModel>> GetAllSorted(string abc = "")
        //{
        //    ICollection<VehicleModelEntity> vehicleModels = null;
        //    switch (abc.ToLower())
        //    {
        //        case "asc":
        //            vehicleModels = await _dbContext.VehicleModel.OrderBy(x => x.Name).ToListAsync();
        //            return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

        //        case "desc":
        //            vehicleModels = await _dbContext.VehicleModel.OrderByDescending(x => x.Name).ToListAsync();
        //            return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

        //        default:
        //            vehicleModels = await _dbContext.VehicleModel.ToListAsync();
        //            return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

        //    }
        //    //        vehicleModels = await _dbContext.VehicleModel.ToListAsync();
        //    //return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);
        //}

        public async Task<ICollection<IVehicleModel>> GetAllModelsFromMake(Guid vehicleMakeID, string abc = "")
        {
            ICollection<VehicleModelEntity> vehicleModels = await _dbContext.VehicleModel.Where(v => v.VehicleMakeId == vehicleMakeID).ToListAsync();
            if (vehicleModels == null)
            {
                return null;
            }

            //ICollection<VehicleModelEntity> vehicleModels = null;
            switch (abc.ToLower())
            {
                case "asc":
                    vehicleModels = vehicleModels.OrderBy(x => x.Name).ToList();
                    return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

                case "desc":
                    vehicleModels = vehicleModels.OrderByDescending(x => x.Name).ToList();
                    return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

                default:
                    vehicleModels = vehicleModels.OrderBy(x => x.Name).ToList();
                    return _mapper.Map<ICollection<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);

            }

            //var mappedVehicleModels = _mapper.Map<List<VehicleModelEntity>, ICollection<IVehicleModel>>(vehicleModels);
            //return (ICollection<IVehicleModel>)mappedVehicleModels;
        }

        public async Task<int> Update(Guid ID, IVehicleModel vehicleModel)
        {
            //var _vehicleModel = await Get(vehicleModel.VehicleModelId);
            var _vehicleModel = await _dbContext.VehicleModel.FindAsync(vehicleModel.VehicleModelId);
            if (_vehicleModel == null)
            {
                return 0;
            }
            _dbContext.Entry(_vehicleModel).CurrentValues.SetValues(_mapper.Map<IVehicleModel, VehicleModelEntity>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
