namespace VehicleApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseEntityDateUpdatedSetToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleMake", "DateUpdated", c => c.DateTime());
            AlterColumn("dbo.VehicleModel", "DateUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleModel", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.VehicleMake", "DateUpdated", c => c.DateTime(nullable: false));
        }
    }
}
