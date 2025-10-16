namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carriages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trains", "NumberOfCarriages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trains", "NumberOfCarriages");
        }
    }
}
