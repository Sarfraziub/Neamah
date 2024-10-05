namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtwofielddevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DepartmentHead", c => c.String());
            AddColumn("dbo.Devices", "DepartmentHeadEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "DepartmentHeadEmail");
            DropColumn("dbo.Devices", "DepartmentHead");
        }
    }
}
