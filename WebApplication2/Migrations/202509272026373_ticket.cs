namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "IsConfirmed");
        }
    }
}
