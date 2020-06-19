using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Common.Filters.Contracts;
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

            var vehicleModel = await GetAsync(id);

            if (vehicleModel == null)
            {
                return 0;
            }

            return await DeleteEntityAsync(Mapper.Map<VehicleModelEntity>(vehicleModel));
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
            ICollection<VehicleModelEntity> filteredData = null;

            IQueryable<VehicleModelEntity> query = DatabaseContext.Set<VehicleModelEntity>();

            if (!(string.IsNullOrEmpty(filter.Search) && string.IsNullOrWhiteSpace(filter.Search)))
            {
                query = query.Where(x => x.Name.Contains(filter.Search.ToLower()));
            }

            if (filter.VehicleMakeId != Guid.Empty)
            {
                query = query.Where(x => x.VehicleMakeId == filter.VehicleMakeId);
            }

            var sortingQuery = sorter.GetSortingQuery(query, sorter.SortBy, sorter.SortDirection);

            var paginationQuery = pagination.GetPaginationQuery(sortingQuery, pagination.RecordsPerPage, pagination.PageNumber);

            filteredData = await paginationQuery.ToListAsync();

            return new ResponseCollection<IVehicleModel>(Mapper.Map<ICollection<IVehicleModel>>(filteredData), pagination.PageNumber, pagination.RecordsPerPage);

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
