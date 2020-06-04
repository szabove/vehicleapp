using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    [Table("VehicleModel")]
    public class VehicleModelEntity : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }
        public VehicleMakeEntity VehicleMake { get; set; }
    }
}
