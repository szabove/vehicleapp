namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMake",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Abrv = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.VehicleModel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Abrv = c.String(),
                        VehicleMakeId = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMake", t => t.VehicleMakeId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.VehicleMakeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModel", "VehicleMakeId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "VehicleMakeId" });
            DropIndex("dbo.VehicleModel", new[] { "Name" });
            DropIndex("dbo.VehicleMake", new[] { "Name" });
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
        }
    }
}
