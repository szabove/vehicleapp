namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMakes",
                c => new
                    {
                        VehicleMakelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Abrv = c.String(),
                    })
                .PrimaryKey(t => t.VehicleMakelId);
            
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        VehicleModelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Abrv = c.String(),
                        VehicleMake_VehicleMakelId = c.Guid(),
                    })
                .PrimaryKey(t => t.VehicleModelId)
                .ForeignKey("dbo.VehicleMakes", t => t.VehicleMake_VehicleMakelId)
                .Index(t => t.VehicleMake_VehicleMakelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModels", "VehicleMake_VehicleMakelId", "dbo.VehicleMakes");
            DropIndex("dbo.VehicleModels", new[] { "VehicleMake_VehicleMakelId" });
            DropTable("dbo.VehicleModels");
            DropTable("dbo.VehicleMakes");
        }
    }
}
