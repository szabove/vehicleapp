using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.DAL
{
    public class VehicleContext : DbContext, IVehicleContext
    {
        public DbSet<VehicleModelEntity> VehicleModel { get; set; }
        public DbSet<VehicleMakeEntity> VehicleMake { get; set; }

        public VehicleContext()
        {
            Database.SetInitializer<VehicleContext>(new CreateDatabaseIfNotExists<VehicleContext>());
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            //Configuring for : VehicleMake has many VehicleModels and will delete them on deletion
            dbModelBuilder.Entity<VehicleMakeEntity>().HasMany(x => x.VehicleModel).WithRequired(x => x.VehicleMake).HasForeignKey(x=>x.VehicleMakeId).WillCascadeOnDelete(true);
            dbModelBuilder.Entity<VehicleMakeEntity>().HasIndex(x => x.Name).IsUnique();
            dbModelBuilder.Entity<VehicleModelEntity>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
