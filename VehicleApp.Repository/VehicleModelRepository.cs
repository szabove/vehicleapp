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
        IRepository<VehicleModelEntity> _repository;
        IMapper _mapper;

        public VehicleModelRepository(IRepository<VehicleModelEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Add(IVehicleModel vehicleModel)
        {
            if (vehicleModel.VehicleModelId == Guid.Empty || vehicleModel.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await _repository.AddAsync(_mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> Delete(Guid vehicleModelID)
        {
            if (vehicleModelID == Guid.Empty)
            {
                return 0;
            }

            var vehicleModel = await _repository.GetAsync(vehicleModelID);

            if (vehicleModel == null)
            {
                return 0;
            }

            return await _repository.DeleteAsync(vehicleModelID);
        }

        public async Task<IVehicleModel> Get(Guid ID)
        {
            if (ID == Guid.Empty)
            {
                return null;
            }

            var response = _mapper.Map<IVehicleModel>(await _repository.GetAsync(ID));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination<IVehicleModel> pagination, ISorter<IVehicleModel> sorter)
        {
            ICollection<IVehicleModel> data = null;

            //Set filter query based on filter properties passed from service layer
            var filterQuery = filter.GetFilterQuery();

            var vehicleMakes = await _repository.WhereQueryAsync(_mapper.MapExpression<Expression<Func<VehicleModelEntity, bool>>>(filterQuery));
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
            
            var pagedCollection = pagination.PaginatedResult(data, pagination.PageSize, pagination.PageNumber);

            var responseCollection = new ResponseCollection<IVehicleModel>(pagedCollection);

            responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            return responseCollection;
        }

        public async Task<int> Update(Guid ID, IVehicleModel vehicleModel)
        {

            if (ID == Guid.Empty || 
                vehicleModel.VehicleModelId == Guid.Empty || 
                vehicleModel.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await _repository.UpdateAsync(ID, _mapper.Map<VehicleModelEntity>(vehicleModel));
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
