using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;


namespace WebApplication2.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private RailwayContext db = new RailwayContext();

        // GET: Tickets
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Seat).Include(t => t.Train);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.Name;
            Ticket ticket = db.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Train)
                .FirstOrDefault(t => t.TicketId == id && t.UserId == userId);

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId");
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,UserId,TrainId,SeatId,PurchaseDate")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.UserId = User.Identity.GetUserId();
                ticket.PurchaseDate = DateTime.Now;

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("MyTickets");
            }

            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", ticket.TrainId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            Ticket ticket = db.Tickets.FirstOrDefault(t => t.TicketId == id && t.UserId == userId);

            if (ticket == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", ticket.TrainId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,UserId,TrainId,SeatId,PurchaseDate")] Ticket ticket)
        {
            string userId = User.Identity.GetUserId();
            var existingTicket = db.Tickets.FirstOrDefault(t => t.TicketId == ticket.TicketId && t.UserId == userId);

            if (existingTicket == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                existingTicket.TrainId = ticket.TrainId;
                existingTicket.SeatId = ticket.SeatId;

                db.Entry(existingTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyTickets");
            }

            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", ticket.SeatId);
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", ticket.TrainId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.Name;
            Ticket ticket = db.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Train)
                .FirstOrDefault(t => t.TicketId == id && t.UserId == userId);

            if (ticket == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string userId = User.Identity.Name;
            var ticket = db.Tickets
                .Include(t => t.Seat)
                .FirstOrDefault(t => t.TicketId == id && t.UserId == userId);

            if (ticket == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ticket.Seat != null)
            {
                ticket.Seat.IsAvailable = true;
            }

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return RedirectToAction("MyTickets");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: Tickets/ReserveSeat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveSeat(int trainId, int seatId)
        {
            var userId = User.Identity.GetUserId();

            var seat = db.Seats.Find(seatId);
            if (seat == null || !seat.IsAvailable)
            {
                TempData["Error"] = "Seat is already booked or invalid.";
                return RedirectToAction("Details", "Trains", new { id = trainId });
            }

            var ticket = new Ticket
            {
                UserId = userId,
                TrainId = trainId,
                SeatId = seatId,
                PurchaseDate = DateTime.Now
            };

            seat.IsAvailable = false;
            db.Tickets.Add(ticket);
            db.SaveChanges();

            TempData["Success"] = $"Successfully reserved seat {seat.SeatNumber} in carriage {seat.Carriage.CarriageNumber}.";
            return RedirectToAction("MyTickets");
        }

        // GET: Tickets/MyTickets
        [Authorize]
        public ActionResult MyTickets()
        {
            var userId = User.Identity.Name;

            var tickets = db.Tickets
                .Include(t => t.Train)
                .Include(t => t.Seat.Carriage)
                .Where(t => t.UserId == userId)
                .ToList();

            DateTime now = DateTime.Now;

            var upcomingTickets = tickets
                .Where(t => t.Train.Date.Add(t.Train.DepartureTime) >= now)
                .ToList();

            var pastTickets = tickets
                .Where(t => t.Train.Date.Add(t.Train.DepartureTime) < now)
                .ToList();


            ViewBag.UpcomingTickets = upcomingTickets;
            ViewBag.PastTickets = pastTickets;

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Reserve(int trainId, int seatId)
        {
            var train = db.Trains.Include(t => t.Carriages.Select(c => c.Seats))
                                 .FirstOrDefault(t => t.TrainId == trainId);

            if (train == null)
                return HttpNotFound();

            var seat = db.Seats.FirstOrDefault(s => s.SeatId == seatId);
            if (seat == null || !seat.IsAvailable)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Seat not available");

            seat.IsAvailable = false;

            string userId = User.Identity.Name;


            var ticket = new Ticket
            {
                TrainId = trainId,
                SeatId = seatId,
                UserId = userId,
                PurchaseDate = DateTime.Now
            };

            db.Tickets.Add(ticket);
            db.SaveChanges();

            return RedirectToAction("MyTickets");
        }




    }
}
