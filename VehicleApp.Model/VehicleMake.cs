using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Model
{
    public class VehicleMake : BaseModel, IVehicleMake
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<IVehicleModel> VehicleModels { get; set; }
    }
}
