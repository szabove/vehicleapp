using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.ViewModels
{
    public class VehicleMakeViewModel
    {
        public VehicleMakeViewModel()
        {
            this.VehicleModels = new HashSet<VehicleModelViewModel>();
            if (VehicleMakeId == Guid.Empty)
            {
                VehicleMakeId = Guid.NewGuid();
            }
        }

        [Required]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<VehicleModelViewModel> VehicleModels { get; set; }
    }
}