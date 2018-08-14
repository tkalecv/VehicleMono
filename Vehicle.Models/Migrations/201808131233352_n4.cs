namespace Vehicle.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleModels", "VehicleMakeID", "dbo.VehicleMakes");
            DropIndex("dbo.VehicleModels", new[] { "VehicleMakeID" });
            AddColumn("dbo.VehicleModels", "MakeID", c => c.Int());
            CreateIndex("dbo.VehicleModels", "MakeID");
            AddForeignKey("dbo.VehicleModels", "MakeID", "dbo.VehicleMakes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModels", "MakeID", "dbo.VehicleMakes");
            DropIndex("dbo.VehicleModels", new[] { "MakeID" });
            DropColumn("dbo.VehicleModels", "MakeID");
            CreateIndex("dbo.VehicleModels", "VehicleMakeID");
            AddForeignKey("dbo.VehicleModels", "VehicleMakeID", "dbo.VehicleMakes", "ID", cascadeDelete: true);
        }
    }
}
