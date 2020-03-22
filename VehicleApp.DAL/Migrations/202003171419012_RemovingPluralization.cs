namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingPluralization : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VehicleMakes", newName: "VehicleMake");
            RenameTable(name: "dbo.VehicleModels", newName: "VehicleModel");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.VehicleModel", newName: "VehicleModels");
            RenameTable(name: "dbo.VehicleMake", newName: "VehicleMakes");
        }
    }
}
