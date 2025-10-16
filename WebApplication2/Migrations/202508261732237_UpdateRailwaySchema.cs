namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateRailwaySchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriages",
                c => new
                {
                    CarriageId = c.Int(nullable: false, identity: true),
                    CarriageNumber = c.Int(nullable: false),
                    TrainId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CarriageId)
                .ForeignKey("dbo.Trains", t => t.TrainId, cascadeDelete: true)
                .Index(t => t.TrainId);

            CreateTable(
                "dbo.Seats",
                c => new
                {
                    SeatId = c.Int(nullable: false, identity: true),
                    SeatNumber = c.Int(nullable: false),
                    IsAvailable = c.Boolean(nullable: false),
                    CarriageId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Carriages", t => t.CarriageId, cascadeDelete: true)
                .Index(t => t.CarriageId);

            CreateTable(
                "dbo.Trains",
                c => new
                {
                    TrainId = c.Int(nullable: false, identity: true),
                    LineNumber = c.String(nullable: false, maxLength: 50),
                    StationName = c.String(nullable: false, maxLength: 100),
                    Date = c.DateTime(nullable: false),
                    DepartureTime = c.Time(nullable: false, precision: 7),
                    ArrivalTime = c.Time(nullable: false, precision: 7),
                })
                .PrimaryKey(t => t.TrainId);

            CreateTable(
                "dbo.Tickets",
                c => new
                {
                    TicketId = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false),
                    TrainId = c.Int(nullable: false),
                    SeatId = c.Int(nullable: false),
                    PurchaseDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Seats", t => t.SeatId, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.TrainId, cascadeDelete: false) // <-- changed here
                .Index(t => t.TrainId)
                .Index(t => t.SeatId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Carriages", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Tickets", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Seats", "CarriageId", "dbo.Carriages");
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "TrainId" });
            DropIndex("dbo.Seats", new[] { "CarriageId" });
            DropIndex("dbo.Carriages", new[] { "TrainId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Trains");
            DropTable("dbo.Seats");
            DropTable("dbo.Carriages");
        }
    }
}
