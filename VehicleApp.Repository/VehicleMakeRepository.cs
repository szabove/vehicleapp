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
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        VehicleContext _dbContext;
        IMapper _mapper;
        IMakeFilter _filter;
        IPagination<IVehicleMake> _pagination;
        public VehicleMakeRepository(VehicleContext dbContext, IMapper mapper, IMakeFilter filter,IPagination<IVehicleMake> pagination)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _filter = filter;
            _pagination = pagination;
        }
        
        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            _dbContext.VehicleMake.Add(_mapper.Map<IVehicleMake, VehicleMakeEntity>(vehicleMake));

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

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination pagination)
        {
            //Set filter query based on filter properties passed from service layer
            var filterQuery = filter.GetFilterQuery();

            var vehicleMakes = await _dbContext.VehicleMake.Where(_mapper.MapExpression<Expression<Func<VehicleMakeEntity, bool>>>(filterQuery)).ToListAsync();
            if (vehicleMakes == null)
            {
                return null;
            }

            //Paginating
            var data = _mapper.Map<ICollection<IVehicleMake>>(vehicleMakes);

            var paginationParams = _mapper.Map<IPagination>(pagination);

            var pagedCollection = _pagination.PaginatedResult(data, paginationParams);

            var responseCollection = new ResponseCollection <  IVehicleMake >(pagedCollection);

            responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            return responseCollection;

            //return vehicleMakes;

            //filter.SetDataToFilter(_mapper.Map<List<VehicleMake>>(vehicleMakes));

            //var filteredItems = filter.FilterItems();

            //if (filteredItems == null)
            // {
            //     return null;
            //}

            //return _mapper.Map<List<IVehicleMake>>(filteredItems);


            //ICollection<VehicleMakeEntity> vehicleMakes = null;
            //switch (abc.ToLower())
            //{
            //    case "asc":
            //        vehicleMakes = await _dbContext.VehicleMake.OrderBy(x => x.Name).ToListAsync();
            //        return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);

            //    case "desc":
            //        vehicleMakes = await _dbContext.VehicleMake.OrderByDescending(x => x.Name).ToListAsync();
            //        return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);

            //    default:
            //        vehicleMakes = await _dbContext.VehicleMake.ToListAsync();
            //        return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);

            //}


            //vehicleMakes = await _dbContext.VehicleMake.ToListAsync();
            //return _mapper.Map<ICollection<VehicleMakeEntity>, ICollection<IVehicleMake>>(vehicleMakes);
        }

        public async Task<int> Update(Guid ID, IVehicleMake vehicleMake)
        {
            var _vehicleMake = await _dbContext.VehicleMake.FindAsync(ID);
            if (_vehicleMake == null)
            {
                return 0;
            }
            _dbContext.Entry(_vehicleMake).CurrentValues.SetValues(_mapper.Map<IVehicleMake, VehicleMakeEntity>(vehicleMake));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
