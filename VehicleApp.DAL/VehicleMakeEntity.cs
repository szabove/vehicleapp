using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    [Table("VehicleMake")]
    public class VehicleMakeEntity
    {
        public VehicleMakeEntity()
        {
            VehicleModel = new HashSet<VehicleModelEntity>();
        }

        [Key]
        public Guid VehicleMakeId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<VehicleModelEntity> VehicleModel { get; set; }
    }
}