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
    public class CarriagesController : Controller
    {
        private RailwayContext db = new RailwayContext();

        // GET: Carriages
        public ActionResult Index()
        {
            var carriages = db.Carriages.Include(c => c.Train);
            return View(carriages.ToList());
        }

        // GET: Carriages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriage carriage = db.Carriages.Find(id);
            if (carriage == null)
            {
                return HttpNotFound();
            }
            return View(carriage);
        }

        // GET: Carriages/Create
        public ActionResult Create()
        {
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber");
            return View();
        }

        // POST: Carriages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarriageId,CarriageNumber,TrainId")] Carriage carriage)
        {
            if (ModelState.IsValid)
            {
                db.Carriages.Add(carriage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", carriage.TrainId);
            return View(carriage);
        }

        // GET: Carriages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriage carriage = db.Carriages.Find(id);
            if (carriage == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", carriage.TrainId);
            return View(carriage);
        }

        // POST: Carriages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarriageId,CarriageNumber,TrainId")] Carriage carriage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carriage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainId = new SelectList(db.Trains, "TrainId", "LineNumber", carriage.TrainId);
            return View(carriage);
        }

        // GET: Carriages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carriage carriage = db.Carriages.Find(id);
            if (carriage == null)
            {
                return HttpNotFound();
            }
            return View(carriage);
        }

        // POST: Carriages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carriage carriage = db.Carriages.Find(id);
            db.Carriages.Remove(carriage);
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
