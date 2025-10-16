using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TrainsController : Controller
    {
        private RailwayContext db = new RailwayContext();

        // GET: Trains
        public ActionResult Index(string searchLine, string searchStation, DateTime? searchDate)
        {
            var trains = db.Trains.AsQueryable();

            if (!string.IsNullOrEmpty(searchLine))
            {
                trains = trains.Where(t => t.LineNumber.Contains(searchLine));
            }

            if (!string.IsNullOrEmpty(searchStation))
            {
                trains = trains.Where(t => t.StationName.Contains(searchStation));
            }

            if (searchDate.HasValue)
            {
                trains = trains.Where(t => t.Date == searchDate.Value.Date);
            }

            DateTime now = DateTime.Now;
            DateTime today = now.Date;
            TimeSpan nowTime = now.TimeOfDay;

            var upcomingTrains = trains
                .Where(t => t.Date > today || (t.Date == today && t.DepartureTime >= nowTime))
                .OrderBy(t => t.Date).ThenBy(t => t.DepartureTime)
                .ToList();

            var pastTrains = trains
                .Where(t => t.Date < today || (t.Date == today && t.DepartureTime < nowTime))
                .OrderByDescending(t => t.Date).ThenByDescending(t => t.DepartureTime)
                .ToList();

            ViewBag.UpcomingTrains = upcomingTrains;
            ViewBag.PastTrains = pastTrains;

            return View();
        }


        // GET: Trains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var train = db.Trains
                .Include(t => t.Carriages.Select(c => c.Seats))
                .FirstOrDefault(t => t.TrainId == id);

            if (train == null)
            {
                return HttpNotFound();
            }

            return View(train);
        }

        // GET: Trains/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Train train)
        {
            if (ModelState.IsValid)
            {
                db.Trains.Add(train);
                db.SaveChanges();

                int seatsPerCarriage = train.NumberOfSeats / train.NumberOfCarriages;
                int remainingSeats = train.NumberOfSeats % train.NumberOfCarriages;

                for (int c = 1; c <= train.NumberOfCarriages; c++)
                {
                    var carriage = new Carriage
                    {
                        CarriageNumber = c,
                        TrainId = train.TrainId
                    };

                    db.Carriages.Add(carriage);
                    db.SaveChanges();

                    int seatsInThisCarriage = seatsPerCarriage + (c == train.NumberOfCarriages ? remainingSeats : 0);

                    var seats = new List<Seat>();
                    for (int s = 1; s <= seatsInThisCarriage; s++)
                    {
                        seats.Add(new Seat
                        {
                            SeatNumber = s,           
                            CarriageId = carriage.CarriageId,
                            IsAvailable = true
                        });
                    }

                    db.Seats.AddRange(seats);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(train);
        }




        // GET: Trains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }

        // POST: Trains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainId,LineNumber,StationName,ArrivalStation,Date,DepartureTime,ArrivalTime")] Train train)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(train);
        }

        // GET: Trains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Train train = db.Trains.Find(id);
            db.Trains.Remove(train);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
