using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model.Common
{
    public interface IVehicleModelDomainModel
    {
        Guid VehicleMakeId { get; set; }
        Guid VehicleModelId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        IVehicleMakeDomainModel VehicleMake { get; set; }
    }
}
