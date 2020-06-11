using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleModelRepository : Repository<VehicleModelEntity>, IVehicleModelRepository
    {
        IVehicleContext DatabaseContext;
        IMapper Mapper;

        public VehicleModelRepository(IMapper mapper, IVehicleContext databaseContext, IUnitOfWork unitOfWork) :
            base(databaseContext, unitOfWork)
        {
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public async Task<int> AddAsync(IVehicleModel vehicleModel)
        {
            if (vehicleModel == null)
            {
                return 0;
            }

            try
            {
                return await AddEntityAsync(Mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            var vehicleMake = await GetEntityAsync(id);

            if (vehicleMake == null)
            {
                return 0;
            }

            return await DeleteEntityAsync(id);
        }

        public async Task<IVehicleModel> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var response = Mapper.Map<IVehicleModel>(await GetEntityAsync(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, ISorter sorter, IPagination pagination)
        {
            IEnumerable<VehicleModelEntity> filteredData = null;

            IQueryable<VehicleModelEntity> query = DatabaseContext.Set<VehicleModelEntity>();

            int querySteps = 0;

            if (!(string.IsNullOrEmpty(filter.Search) && string.IsNullOrWhiteSpace(filter.Search)))
            {
                query = query.Where(x => x.Name.Contains(filter.Search.ToLower()));
                querySteps++;
            }

            if (filter.VehicleMakeId != Guid.Empty)
            {
                query = query.Where(x => x.VehicleMakeId == filter.VehicleMakeId);
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

            return new ResponseCollection<IVehicleModel>(Mapper.Map<IEnumerable<IVehicleModel>>(paginatedData), pagination.PageNumber, pagination.RecordsPerPage);

        }

        public async Task<int> UpdateAsync(Guid id, IVehicleModel vehicleModel)
        {

            if (id == Guid.Empty || vehicleModel == null)
            {
                return 0;
            }

            try
            {
                return await UpdateEntityAsync(id, Mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
