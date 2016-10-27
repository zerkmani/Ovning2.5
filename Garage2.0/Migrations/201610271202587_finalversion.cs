namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalversion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vechicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNo = c.String(nullable: false, maxLength: 6),
                        Color = c.Int(nullable: false),
                        Brand = c.String(nullable: false, maxLength: 30),
                        ParkingTime = c.DateTime(nullable: false),
                        NrOfWeels = c.Int(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                        VehicleTypeId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vechicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vechicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vechicles", new[] { "MemberId" });
            DropIndex("dbo.Vechicles", new[] { "VehicleTypeId" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vechicles");
            DropTable("dbo.Members");
        }
    }
}
