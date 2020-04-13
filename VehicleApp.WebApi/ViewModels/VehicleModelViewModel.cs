﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.ViewModels
{
    public class VehicleModelViewModel
    {
        public VehicleModelViewModel()
        {
            if (VehicleModelId == Guid.Empty)
            {
                VehicleModelId = Guid.NewGuid();
            }
        }

        [Required]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public Guid VehicleModelId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeViewModel VehicleMake { get; set; }
    }
}