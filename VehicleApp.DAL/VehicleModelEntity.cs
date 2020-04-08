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
    public class VehicleModelEntity
    {
        
        public VehicleModelEntity()
        {
            if (VehicleModelId == Guid.Empty)
            {
                VehicleModelId = Guid.NewGuid();
            }
        }

        [Key]
        public Guid VehicleModelId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual VehicleMakeEntity VehicleMake { get; set; }
    }
}
