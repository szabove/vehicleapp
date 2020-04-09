using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model.Common
{
    public interface IVehicleMake
    {
        Guid VehicleMakeId { get; set; }
        string Name{ get; set; }
        string Abrv { get; set; }
        ICollection<IVehicleModel> VehicleModels { get; set; }
    }
}
