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
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleModelRepository : Repository<BaseEntity>, IVehicleModelRepository
    {
        IMapper _mapper;

        public VehicleModelRepository(IMapper mapper, IVehicleContext context, IUnitOfWork unitOfWork) :
            base(context, unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(IVehicleModel vehicleModel)
        {
            if (vehicleModel.Id == Guid.Empty || vehicleModel.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await AddAsync(_mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public new async Task<int> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            var vehicleMake = await base.GetAsync(id);

            if (vehicleMake == null)
            {
                return 0;
            }

            return await DeleteAsync(id);
        }

        public new async Task<IVehicleModel> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var response = _mapper.Map<IVehicleModel>(await base.GetAsync(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination<IVehicleModel> pagination, ISorter<IVehicleModel> sorter)
        {
            throw new NotImplementedException();
            //ICollection<IVehicleModel> data = null;

            ////Set filter query based on filter properties passed from service layer
            //var filterQuery = filter.GetFilterQuery();

            //var vehicleMakes = await _repository.WhereQueryAsync(_mapper.MapExpression<Expression<Func<VehicleModelEntity, bool>>>(filterQuery));
            //if (vehicleMakes == null)
            //{
            //    return null;
            //}

            ////Sorting

            //var sortQuery = sorter.GetSortQuery();

            //if (sortQuery != null)
            //{
            //    data = sorter.SortData(_mapper.Map<ICollection<IVehicleModel>>(vehicleMakes), sortQuery);
            //}
            //else
            //{
            //    data = _mapper.Map<ICollection<IVehicleModel>>(vehicleMakes);
            //}
            
            ////Paginating
            
            //var pagedCollection = pagination.PaginatedResult(data, pagination.PageSize, pagination.PageNumber);

            //var responseCollection = new ResponseCollection<IVehicleModel>(pagedCollection);

            //responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            //return responseCollection;
        }

        public async Task<int> UpdateAsync(Guid id, IVehicleModel vehicleModel)
        {

            if (id == Guid.Empty ||
                vehicleModel.Id == Guid.Empty ||
                vehicleModel.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await UpdateAsync(id, _mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
