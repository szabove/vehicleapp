using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model.Common
{
    public interface IVehicleModel
    {
        Guid VehicleMakeId { get; set; }
        Guid VehicleModelId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        //IVehicleMake VehicleMake { get; set; }
    }
}
