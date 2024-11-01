namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialupdate17102024 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        DeviceName = c.String(),
                        Category = c.String(),
                        Status = c.Int(),
                        DeviceOwner = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        TagNumber = c.String(),
                        Note = c.String(),
                        StaffId = c.Int(nullable: false),
                        StaffName = c.String(),
                        Department = c.String(),
                        DepartmentHead = c.String(),
                        DepartmentHeadEmail = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.ProductAssociations",
                c => new
                    {
                        AssociationId = c.Int(nullable: false, identity: true),
                        AssignDate = c.DateTime(nullable: false),
                        IsAssigned = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssociationId)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Department = c.String(),
                        IsAdmin = c.Boolean(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.StaffChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        StaffName = c.String(),
                        Department = c.String(),
                        DepartmentHead = c.String(),
                        DepartmentEmail = c.String(),
                        ChangedBy = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        ChangeNote = c.String(),
                        AttachmentPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAssociations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductAssociations", "DeviceId", "dbo.Devices");
            DropIndex("dbo.ProductAssociations", new[] { "DeviceId" });
            DropIndex("dbo.ProductAssociations", new[] { "UserId" });
            DropTable("dbo.StaffChanges");
            DropTable("dbo.Users");
            DropTable("dbo.ProductAssociations");
            DropTable("dbo.Devices");
        }
    }
}
