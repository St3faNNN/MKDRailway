namespace WebApplication2.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication2.Models.RailwayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication2.Models.RailwayContext context)
        {
            //// Add Trains
            //var train1 = new Train
            //{
            //    TrainId = 1,
            //    LineNumber = "R100",
            //    StationName = "Skopje - Bitola",
            //    Date = DateTime.Today.AddDays(1),
            //    DepartureTime = new TimeSpan(08, 00, 00),
            //    ArrivalTime = new TimeSpan(11, 30, 00)
            //};

            //var train2 = new Train
            //{
            //    TrainId = 2,
            //    LineNumber = "R200",
            //    StationName = "Skopje - Gevgelija",
            //    Date = DateTime.Today.AddDays(1),
            //    DepartureTime = new TimeSpan(14, 00, 00),
            //    ArrivalTime = new TimeSpan(18, 15, 00)
            //};

            //context.Trains.AddOrUpdate(t => t.TrainId, train1, train2);

            //// Add Carriages
            //var carriage1 = new Carriage { CarriageId = 1, CarriageNumber = 1, TrainId = 1 };
            //var carriage2 = new Carriage { CarriageId = 2, CarriageNumber = 2, TrainId = 1 };
            //var carriage3 = new Carriage { CarriageId = 3, CarriageNumber = 1, TrainId = 2 };

            //context.Carriages.AddOrUpdate(c => c.CarriageId, carriage1, carriage2, carriage3);

            //// Add Seats for Train 1, Carriage 1 (10 seats)
            //var seats = new List<Seat>();
            //for (int i = 1; i <= 10; i++)
            //{
            //    seats.Add(new Seat { SeatId = i, SeatNumber = i, CarriageId = 1, IsAvailable = true });
            //}

            //// Add Seats for Train 1, Carriage 2 (10 seats)
            //for (int i = 11; i <= 20; i++)
            //{
            //    seats.Add(new Seat { SeatId = i, SeatNumber = i - 10, CarriageId = 2, IsAvailable = true });
            //}

            //// Add Seats for Train 2, Carriage 1 (10 seats)
            //for (int i = 21; i <= 30; i++)
            //{
            //    seats.Add(new Seat { SeatId = i, SeatNumber = i - 20, CarriageId = 3, IsAvailable = true });
            //}

            //context.Seats.AddOrUpdate(s => s.SeatId, seats.ToArray());

            //// Tickets won’t be seeded – they should be created only when users purchase.
            try
            {
                // Your existing seeding code, e.g. context.Trains.AddOrUpdate(...)
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
