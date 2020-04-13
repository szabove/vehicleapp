namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyFix2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.VehicleModel", name: "VehicleMake_VehicleMakeId", newName: "VehicleMakeId");
            RenameIndex(table: "dbo.VehicleModel", name: "IX_VehicleMake_VehicleMakeId", newName: "IX_VehicleMakeId");
            DropColumn("dbo.VehicleMake", "VehicleModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleMake", "VehicleModelId", c => c.Guid(nullable: false));
            RenameIndex(table: "dbo.VehicleModel", name: "IX_VehicleMakeId", newName: "IX_VehicleMake_VehicleMakeId");
            RenameColumn(table: "dbo.VehicleModel", name: "VehicleMakeId", newName: "VehicleMake_VehicleMakeId");
        }
    }
}
