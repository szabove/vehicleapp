using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : Repository<VehicleMakeEntity>, IVehicleMakeRepository 
    {
        IMapper _mapper;

        public VehicleMakeRepository( IMapper mapper, IVehicleContext context, IUnitOfWork unitOfWork):
            base(context, unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(IVehicleMake vehicleMake)
        {
            if (vehicleMake.Id == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await base.AddAsync(_mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception ex)
            {
                throw ex;
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

        public new async Task<IVehicleMake> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var response = _mapper.Map<IVehicleMake>(await base.GetAsync(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter)
        {
            throw new NotImplementedException();




            //ICollection<IVehicleMake> data = null;

            //if (filter == null)
            //{
            //    return null;
            //}

            ////Set filter query based on filter properties passed from service layer
            //var filterQuery = filter.GetFilterQuery();
            //var vehicleMakes = await WhereQueryAsync(_mapper.MapExpression<Expression<Func<VehicleMakeEntity, bool>>>(filterQuery));

            //if (vehicleMakes == null)
            //{
            //    return null;
            //}

            ////Sorting

            //var sortQuery = sorter.GetSortQuery();

            //if (sortQuery != null)
            //{
            //    data = sorter.SortData(_mapper.Map<ICollection<IVehicleMake>>(vehicleMakes), sortQuery);
            //}
            //else
            //{
            //    data = _mapper.Map<ICollection<IVehicleMake>>(vehicleMakes);
            //}

            ////Paginating

            //var pagedCollection = pagination.PaginatedResult(data, pagination.PageSize, pagination.PageNumber);

            //var responseCollection = new ResponseCollection <  IVehicleMake >(pagedCollection);

            //responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            //return responseCollection;

        }

        public async Task<int> UpdateAsync(Guid id, IVehicleMake vehicleMake)
        {
            if (id == Guid.Empty || vehicleMake.Id == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await base.UpdateAsync(id, _mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
