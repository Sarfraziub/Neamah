namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Status", c => c.Int());
            DropColumn("dbo.Devices", "IsRejected");
            DropColumn("dbo.Devices", "IsStored");
            DropColumn("dbo.Devices", "IsReserved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "IsReserved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "IsStored", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "IsRejected", c => c.Boolean(nullable: false));
            DropColumn("dbo.Devices", "Status");
        }
    }
}
