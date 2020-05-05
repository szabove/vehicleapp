using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _dbContext;
        IMapper _mapper;
        IPagination<IVehicleModel> _pagination;

        public VehicleModelRepository(VehicleContext dbContext, IMapper mapper, IPagination<IVehicleModel> pagination)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _pagination = pagination;
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

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination pagination, ISorter<IVehicleModel> sorter)
        {
            ICollection<IVehicleModel> data = null;

            //Set filter query based on filter properties passed from service layer
            var filterQuery = filter.GetFilterQuery();

            var vehicleMakes = await _dbContext.VehicleModel.Where(_mapper.MapExpression<Expression<Func<VehicleModelEntity, bool>>>(filterQuery)).ToListAsync();
            if (vehicleMakes == null)
            {
                return null;
            }


            //Sorting

            var sortQuery = sorter.GetSortQuery();

            if (sortQuery != null)
            {
                data = sorter.SortData(_mapper.Map<ICollection<IVehicleModel>>(vehicleMakes), sortQuery);
            }
            else
            {
                data = _mapper.Map<ICollection<IVehicleModel>>(vehicleMakes);
            }


            //Paginating

            var paginationParams = _mapper.Map<IPagination>(pagination);

            var pagedCollection = _pagination.PaginatedResult(data, paginationParams);

            var responseCollection = new ResponseCollection<IVehicleModel>(pagedCollection);

            responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            return responseCollection;
        }

        public async Task<int> Update(Guid ID, IVehicleModel vehicleModel)
        {
            var _vehicleModel = await _dbContext.VehicleModel.FindAsync(ID);
            if (_vehicleModel == null)
            {
                return 0;
            }
            _dbContext.Entry(_vehicleModel).CurrentValues.SetValues(_mapper.Map<IVehicleModel, VehicleModelEntity>(vehicleModel));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
