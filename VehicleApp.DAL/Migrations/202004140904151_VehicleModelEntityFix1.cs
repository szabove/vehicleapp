namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleModelEntityFix1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.VehicleModel", name: "VehicleMake_VehicleMakeId", newName: "VehicleMakeId");
            RenameIndex(table: "dbo.VehicleModel", name: "IX_VehicleMake_VehicleMakeId", newName: "IX_VehicleMakeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.VehicleModel", name: "IX_VehicleMakeId", newName: "IX_VehicleMake_VehicleMakeId");
            RenameColumn(table: "dbo.VehicleModel", name: "VehicleMakeId", newName: "VehicleMake_VehicleMakeId");
        }
    }
}
