using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    public class VehicleContext : DbContext
    {
        public DbSet<VehicleModelEntity> VehicleModel { get; set; }
        public DbSet<VehicleMakeEntity> VehicleMake { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //dbModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Configuring for : VehicleMake has many VehicleModels and will delete them on deletion
            dbModelBuilder.Entity<VehicleMakeEntity>().HasMany(x => x.VehicleModels).WithRequired(x => x.VehicleMake).WillCascadeOnDelete(true);
        }
    }
}
