namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseEntityDateTimeTypeNameSetTodatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleMake", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.VehicleMake", "DateUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.VehicleModel", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.VehicleModel", "DateUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleModel", "DateUpdated", c => c.DateTime());
            AlterColumn("dbo.VehicleModel", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.VehicleMake", "DateUpdated", c => c.DateTime());
            AlterColumn("dbo.VehicleMake", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
