namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavigationPropertyVehicleModelsMarkedAsVirtual : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleModel", "VehicleMake_VehicleMakelId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "VehicleMake_VehicleMakelId" });
            AlterColumn("dbo.VehicleModel", "VehicleMake_VehicleMakelId", c => c.Guid(nullable: false));
            CreateIndex("dbo.VehicleModel", "VehicleMake_VehicleMakelId");
            AddForeignKey("dbo.VehicleModel", "VehicleMake_VehicleMakelId", "dbo.VehicleMake", "VehicleMakelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModel", "VehicleMake_VehicleMakelId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "VehicleMake_VehicleMakelId" });
            AlterColumn("dbo.VehicleModel", "VehicleMake_VehicleMakelId", c => c.Guid());
            CreateIndex("dbo.VehicleModel", "VehicleMake_VehicleMakelId");
            AddForeignKey("dbo.VehicleModel", "VehicleMake_VehicleMakelId", "dbo.VehicleMake", "VehicleMakelId");
        }
    }
}
