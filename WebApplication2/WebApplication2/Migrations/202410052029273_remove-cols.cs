namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecols : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Devices", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "FileName", c => c.String());
        }
    }
}
