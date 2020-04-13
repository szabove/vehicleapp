namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMake",
                c => new
                    {
                        VehicleMakeId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Abrv = c.String(),
                    })
                .PrimaryKey(t => t.VehicleMakeId);
            
            CreateTable(
                "dbo.VehicleModel",
                c => new
                    {
                        VehicleModelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Abrv = c.String(),
                        VehicleMake_VehicleMakeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleModelId)
                .ForeignKey("dbo.VehicleMake", t => t.VehicleMake_VehicleMakeId, cascadeDelete: true)
                .Index(t => t.VehicleMake_VehicleMakeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModel", "VehicleMake_VehicleMakeId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "VehicleMake_VehicleMakeId" });
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
        }
    }
}
