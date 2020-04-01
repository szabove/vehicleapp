using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.ViewModels
{
    public class VehicleModelViewModel
    {
        public Guid VehicleMakeId { get; set; }
        public Guid VehicleModelId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeViewModel VehicleMake { get; set; }
    }
}