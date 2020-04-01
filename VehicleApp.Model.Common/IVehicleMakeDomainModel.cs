using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model.Common
{
    public interface IVehicleMakeDomainModel
    {
        Guid VehicleMakeId { get; set; }
        string Name{ get; set; }
        string Abrv { get; set; }
        ICollection<IVehicleModelDomainModel> VehicleModel { get; set; }
    }
}
