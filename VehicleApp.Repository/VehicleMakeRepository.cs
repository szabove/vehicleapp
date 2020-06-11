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
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : Repository<VehicleMakeEntity>, IVehicleMakeRepository
    {
        IMapper Mapper;
        IVehicleContext DatabaseContext;

        public VehicleMakeRepository( IMapper mapper, IVehicleContext databaseContext, IUnitOfWork unitOfWork)
            :base(databaseContext, unitOfWork)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException();
            }
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public async Task<int> AddAsync(IVehicleMake vehicleMake)
        {
            if (vehicleMake == null)
            {
                return 0;
            }

            try
            {
                return await AddEntityAsync(Mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            var vehicleMake = await GetAsync(id);

            if (vehicleMake == null)
            {
                return 0;
            }

            return await DeleteAsync(id);
        }

        public async Task<IVehicleMake> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var response = Mapper.Map<IVehicleMake>(await GetEntityAsync(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, ISorter sorter, IPagination pagination)
        {
            IEnumerable<VehicleMakeEntity> filteredData = null;

            IQueryable<VehicleMakeEntity> query = DatabaseContext.Set<VehicleMakeEntity>();

            int querySteps = 0;

            if (!(string.IsNullOrEmpty(filter.Search) && string.IsNullOrWhiteSpace(filter.Search)))
            {
                query = query.Where(x => x.Name.Contains(filter.Search.ToLower()));
                querySteps++;
            }

            if (querySteps > 0)
            {
                filteredData = await query.ToListAsync();
            }

            if (filteredData == null)
            {
                return null;
            }

            var sortedData = sorter.GetSortedData(filteredData, sorter.SortBy, sorter.SortDirection);

            var paginatedData = pagination.GetPaginatedData(sortedData, pagination.RecordsPerPage, pagination.PageNumber);

            return new ResponseCollection<IVehicleMake>(Mapper.Map<IEnumerable<IVehicleMake>>(paginatedData), pagination.PageNumber, pagination.RecordsPerPage);
            
        }

        public async Task<int> UpdateAsync(Guid id, IVehicleMake vehicleMake)
        {
            if (id == Guid.Empty || vehicleMake == null)
            {
                return 0;
            }

            try
            {
                return await UpdateEntityAsync(id, Mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
