namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSuperAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsSuperAdmin", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsSuperAdmin");
        }
    }
}
