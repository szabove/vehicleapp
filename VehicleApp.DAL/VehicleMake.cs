using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    public class VehicleMake
    {
        public VehicleMake()
        {
            this.VehicleModels = new HashSet<VehicleModel>();
        }

        [Key]
        public Guid VehicleMakelId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}