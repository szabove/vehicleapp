namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingMaxLengthAndUniqueOnDbEntities : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleMake", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.VehicleModel", "Name", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.VehicleMake", "Name", unique: true);
            CreateIndex("dbo.VehicleModel", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.VehicleModel", new[] { "Name" });
            DropIndex("dbo.VehicleMake", new[] { "Name" });
            AlterColumn("dbo.VehicleModel", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleMake", "Name", c => c.String(nullable: false));
        }
    }
}
