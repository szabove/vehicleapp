using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.Common
{
    public interface IVehicleModelRepository
    {
        VehicleModel Get(Guid id);
        IEnumerable<VehicleModel> GetAll();
        void Add(VehicleModel vehicleMake);
        void Update(VehicleModel vehicleMake);
        void Delete(VehicleModel vehicleMake);
    }
}
