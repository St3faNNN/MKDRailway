namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trains", "NumberOfSeats", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trains", "NumberOfSeats");
        }
    }
}
