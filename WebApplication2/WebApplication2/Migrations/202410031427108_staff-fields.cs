namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stafffields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffChanges", "DepartmentHead", c => c.String());
            AddColumn("dbo.StaffChanges", "DepartmentEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffChanges", "DepartmentEmail");
            DropColumn("dbo.StaffChanges", "DepartmentHead");
        }
    }
}
