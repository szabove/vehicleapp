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
        IRepository<VehicleMakeEntity> _repository;
        IMapper _mapper;

        public VehicleMakeRepository(IRepository<VehicleMakeEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            if (vehicleMake.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await _repository.AddAsync(_mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return 0;
            }

            var vehicleMake = await _repository.GetAsync(id);

            if (vehicleMake == null)
            {
                return 0;
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IVehicleMake> Get(Guid id)
        {
            var response = _mapper.Map<IVehicleMake>(await _repository.GetAsync(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter)
        {
            ICollection<IVehicleMake> data = null;

            if (filter == null)
            {
                return null;
            }

            //Set filter query based on filter properties passed from service layer
            var filterQuery = filter.GetFilterQuery();
            var vehicleMakes = await _repository.WhereQueryAsync(_mapper.MapExpression<Expression<Func<VehicleMakeEntity, bool>>>(filterQuery));

            if (vehicleMakes == null)
            {
                return null;
            }

            //Sorting

            var sortQuery = sorter.GetSortQuery();

            if (sortQuery != null)
            {
                data = sorter.SortData(_mapper.Map<ICollection<IVehicleMake>>(vehicleMakes), sortQuery);
            }
            else
            {
                data = _mapper.Map<ICollection<IVehicleMake>>(vehicleMakes);
            }
            
            //Paginating
            
            var pagedCollection = pagination.PaginatedResult(data, pagination.PageSize, pagination.PageNumber);

            var responseCollection = new ResponseCollection <  IVehicleMake >(pagedCollection);

            responseCollection.SetPagingParams(pagination.PageNumber, pagination.PageSize);

            return responseCollection;

        }

        public async Task<int> Update(Guid ID, IVehicleMake vehicleMake)
        {
            if (ID == Guid.Empty)
            {
                return 0;
            }

            try
            {
                return await _repository.UpdateAsync(ID, _mapper.Map<VehicleMakeEntity>(vehicleMake));
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
