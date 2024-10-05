namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attachmentpath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaffChanges", "AttachmentPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StaffChanges", "AttachmentPath");
        }
    }
}
