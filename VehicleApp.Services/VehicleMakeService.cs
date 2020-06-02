using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Services.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Model.Common;
using VehicleApp.Model;
using VehicleApp.Common;

namespace VehicleApp.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        IUnitOfWork _unitOFWork;

        public VehicleMakeService(IUnitOfWork unitOFWork)
        {
            _unitOFWork = unitOFWork;
        }

        public async Task<int> Add(IVehicleMake vehicleMake)
        {
            if (vehicleMake.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            var result = await _unitOFWork.Makes.Add(vehicleMake);
            if (result == 0)
            {
                return 0;
            }

            return await _unitOFWork.CommitAsync();
        }

        public async Task<int> Delete(Guid ID)
        {
            if (ID == Guid.Empty)
            {
                return 0;
            }

            var result =  await _unitOFWork.Makes.Delete(ID);
            if (result == 0)
            {
                return 0;
            }

            return await _unitOFWork.CommitAsync();
        }

        public async Task<IVehicleMake> Get(Guid ID)
        {
            if (ID == Guid.Empty)
            {
                return null;
            }

            var make = await _unitOFWork.Makes.Get(ID);
            if (make == null)
            {
                return null;
            }
            return make;
        }

        public async Task<ResponseCollection<IVehicleMake>> FindAsync(IMakeFilter filter, IPagination<IVehicleMake> pagination, ISorter<IVehicleMake> sorter)
        {
            if (filter == null)
            {
                return null;
            }

            return await _unitOFWork.Makes.FindAsync(filter, pagination, sorter);
        }

        public async Task<int> Update(Guid ID, IVehicleMake vehicleMake)
        {
            if (ID == Guid.Empty || vehicleMake.VehicleMakeId == Guid.Empty)
            {
                return 0;
            }

            var result = await _unitOFWork.Makes.Update(ID, vehicleMake);

            if (result == 0)
            {
                return 0;
            }

            return await _unitOFWork.CommitAsync();
        }
    }
}
