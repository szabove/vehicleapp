using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.ViewModels
{
    public class VehicleMakeViewModel
    {
        public Guid VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<VehicleModelViewModel> VehicleModel { get; set; }
    }
}