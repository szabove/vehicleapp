using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; private set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DateUpdated { get; set; }

        public void SetDateCreated()
        {
            DateCreated = DateTime.Now;
        }
    }
}
