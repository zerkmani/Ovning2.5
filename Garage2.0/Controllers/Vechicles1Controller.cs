﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0.DAL;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class Vechicles1Controller : Controller
    {
        private VehicleDbContext db = new VehicleDbContext();

        // GET: Vechicles1
        public ActionResult Index()
        {
            
            var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
            
            return View(vehicles.ToList());
        }

        [HttpPost]
        public ActionResult Index(string regNo, string vehicleType = "Exclude")
        {
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
            //var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
           var temp = regNo.ToUpper();
            
       
            var searchResult = db.Vehicles.ToList();

            //if (vehicleType != "Exclude")
            //    searchResult = searchResult.Where(v => v.VechicleType.ToString() == vehicleType.ToString()).ToList();

  
            if (!String.IsNullOrEmpty(temp))
                searchResult = searchResult.Where(v => v.RegNo.Contains(temp)).ToList();
            if (vehicleType != "Exclude")
                searchResult = searchResult.Where(v => v.vehicleType.Type.ToString() == vehicleType.ToString()).ToList(); 


            return View(searchResult);
        }



        public ActionResult Detailed()
        {
            var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
            return View(vehicles.ToList());
        }

        [HttpPost]
        public ActionResult Detailed(string regNo, string vehicleType = "Exclude")
        {
            var temp = regNo.ToUpper();
            
            var searchResult = db.Vehicles.ToList();

            if (!String.IsNullOrEmpty(temp))
                searchResult = searchResult.Where(v => v.RegNo.Contains(temp)).ToList();
            if (vehicleType != "Exclude")
                searchResult = searchResult.Where(v => v.vehicleType.Type.ToString() == vehicleType.ToString()).ToList();
            return View(searchResult);
        }


        // GET: Vechicles1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vechicle vechicle = db.Vehicles.Find(id);
            if (vechicle == null)
            {
                return HttpNotFound();
            }
            return View(vechicle);
        }

        // GET: Vechicles1/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Vechicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegNo,Color,Brand,NrOfWeels,Model,VehicleTypeId,MemberId")] Vechicle vechicle)
        {
            vechicle.ParkingTime = DateTime.Now;
            //behövs kontroll av att regnr inte redan existerar. 
            var tempRegNo = vechicle.RegNo;
            foreach (var v in db.Vehicles)
            {
                if (tempRegNo == v.RegNo) //regnr finns redan i databasen. avbryt incheckning. behövs meddelande av något slag
                {
                    ViewBag.Message = "You tried to check in a vehicle with an already existing registration number in the garage. Please try again.";
                    ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vechicle.MemberId);
                    ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
                    return View();
                }
            }
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vechicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vechicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
            return View(vechicle);
        }

        // GET: Vechicles1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vechicle vechicle = db.Vehicles.Find(id);
            if (vechicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vechicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
            return View(vechicle);
        }

        // POST: Vechicles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNo,Color,Brand,ParkingTime,NrOfWeels,Model,VehicleTypeId,MemberId")] Vechicle vechicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vechicle).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vechicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
            return View(vechicle);
        }

        // GET: Vechicles1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vechicle vechicle = db.Vehicles.Find(id);
            if (vechicle == null)
            {
                return HttpNotFound();
            }
            return View(vechicle);
        }

        // POST: Vechicles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vechicle vechicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vechicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Statistics()
        {

            //antal fordon av varje typ
            //init counters
            int NrOfAirplanes = 0;
            int NrOfBoats = 0;
            int NrOfBuses = 0;
            int NrOfCars = 0;
            int NrOfMotorcycles = 0;

           // (var suit in Enum.GetValues(typeof(Suits)) db.Vehicles)
            foreach (var vehicle in db.Vehicles)
            {
                //måste komma åt vehicletype denna returnerar bara en EF-sträng
                var value = (vehicle.vehicleType.Type);//(Enum.GetValues(typeof(type));

                string newvalue = value.ToString();
                if (newvalue == "Airplane") NrOfAirplanes++;
                if (newvalue == "Boat") NrOfBoats++;
                if (newvalue == "Bus") NrOfBuses++;
                if (newvalue == "Car") NrOfCars++;
                if (newvalue == "Motorcycle") NrOfMotorcycles++;
            }
            ViewData["NrOfAirplanes"] = NrOfAirplanes;
            ViewData["NrOfBoats"] = NrOfBoats;
            ViewData["NrOfBuses"] = NrOfBuses;
            ViewData["NrOfCars"] = NrOfCars;
            ViewData["NrOfMotorcycles"] = NrOfMotorcycles;

            //totalt antal hjul
            int NumberOfWheels = 0;
            foreach (var v in db.Vehicles)
            {
                NumberOfWheels += v.NrOfWeels;
            }
            //present the data to client

            ViewData["NumberOfWheels"] = NumberOfWheels;

            //totalt garagepris
            //init relevant variables
            int TotalCost = 0;
            TimeSpan hours;
            var checkOutTime = DateTime.Now;

            //step through all vehicles. calculate cost and att to totalcost.
            foreach (var vehicle in db.Vehicles)
            {
                hours = checkOutTime - vehicle.ParkingTime;
                var parkedHour = hours.TotalHours;
                var minutes = Convert.ToInt32(parkedHour) * 60;
                TotalCost += minutes; //the cost is 1 kr per minute, so simply add number of minutes for the vehicle to totalcost.
            }
            //present the data to client
            ViewData["TotalCost"] = TotalCost;

            return View();
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
