using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        public Task<int> Add(IVehicleMake vehicleMake)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(IVehicleMake vehicleMake)
        {
            throw new NotImplementedException();
        }

        public Task<IVehicleMake> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IVehicleMake>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<IVehicleMake>> GetAllModelsFromMaker(IVehicleMake vehicleMake)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(IVehicleMake vehicleMake)
        {
            throw new NotImplementedException();
        }
    }
}
