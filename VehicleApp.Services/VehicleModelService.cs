using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Services.Common;

namespace VehicleApp.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        IUnitOfWork _unitOfWork;

        public VehicleModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(IVehicleModel vehicleModel)
        {
            var result = await _unitOfWork.Models.Add(vehicleModel);
            if (result == 0)
            {
                return 0;
            }

            return await _unitOfWork.CommitAsync();
        }

        public async  Task<int> Delete(Guid ID)
        {
            var model = await _unitOfWork.Models.Get(ID);
            if (model == null)
            {
                return 0;
            }

            var result = await _unitOfWork.Models.Delete(ID);
            if (result == 0)
            {
                return 0;
            }

            return await _unitOfWork.CommitAsync();
        }

        public async Task<IVehicleModel> Get(Guid ID)
        {
            var model = await _unitOfWork.Models.Get(ID);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<ResponseCollection<IVehicleModel>> FindAsync(IModelFilter filter, IPagination pagination, ISorter<IVehicleModel> sorter)
        {
            return await _unitOfWork.Models.FindAsync(filter, pagination, sorter);
        }

        public async Task<int> Update(Guid ID, IVehicleModel vehicleModel)
        {
            var result = await _unitOfWork.Models.Update(ID, vehicleModel);

            if (result == 0)
            {
                return 0;
            }

            return await _unitOfWork.CommitAsync();
        }
    }
}
