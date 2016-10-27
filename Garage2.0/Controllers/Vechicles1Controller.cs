using System;
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
        const int GarageSize = 11;  //hardcoded garagesize

        // GET: Vechicles1
        public ActionResult Index()
        {
            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
            
            return View(vehicles.ToList());
        }

        [HttpPost]
        public ActionResult Index(string regNo, string vehicleType = "Exclude")
        {
            //ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vechicle.VehicleTypeId);
            //var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            var searchResult = db.Vehicles.ToList();

            //if (vehicleType != "Exclude")
            //    searchResult = searchResult.Where(v => v.VechicleType.ToString() == vehicleType.ToString()).ToList();
            //x.Name.ToLower().Contains(SearchValue.ToLower())

            if (!String.IsNullOrEmpty(regNo))
                searchResult = searchResult.Where(v => v.RegNo.ToUpper().Contains(regNo.ToUpper())).ToList();
            if (vehicleType != "Exclude")
                searchResult = searchResult.Where(v => v.vehicleType.Type.ToString() == vehicleType.ToString()).ToList(); 


            return View(searchResult);
        }



        public ActionResult Detailed()
        {
            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            var vehicles = db.Vehicles.Include(v => v.GarageMember).Include(v => v.vehicleType);
            return View(vehicles.ToList());
        }

        [HttpPost]
        public ActionResult Detailed(string regNo, string vehicleType = "Exclude")
        {

            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            var searchResult = db.Vehicles.ToList();

            if (!String.IsNullOrEmpty(regNo))
                searchResult = searchResult.Where(v => v.RegNo.ToUpper().Contains(regNo.ToUpper())).ToList();
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
        public ActionResult CheckIn()
        {
            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            int vehiclescounter = db.Vehicles.Count();
            if (vehiclescounter >= GarageSize) //error check for the garage size 
            {
                //break indexing and send message to client to resize the garage constant in visual studio for now
                var Fullmodel = db.Vehicles;
                //ViewBag.Message = "The garage is full. Check out some vehicles";
                TempData["TooFewParkingSpots"] = "The garage is full. Check out some vehicles.";
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Vechicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,RegNo,Color,Brand,NrOfWeels,Model,VehicleTypeId,MemberId")] Vechicle vechicle)
        {
            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

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
                TempData["Message"] = "Vehicle checked in.";
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
        public ActionResult CheckOut(int? id)
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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Vechicle vechicle = db.Vehicles.Find(id);
        //    db.Vehicles.Remove(vechicle);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutConfirmed(int id)
        {
            ReciptViewModel recieptModel;
            Vechicle vehicle = db.Vehicles.Find(id);
            var checkOutTime = DateTime.Now;
            TimeSpan hours = checkOutTime - vehicle.ParkingTime;
            var parkedMin = hours.TotalMinutes;
            var cost = 1 * Convert.ToInt32(parkedMin);
            var hour = hours.TotalHours;

            recieptModel = new ReciptViewModel()
            {
                RegNo = vehicle.RegNo,
                CheckInTime = vehicle.ParkingTime,
                CheckOutTime = checkOutTime,
                ParkedHour = Convert.ToInt32(hour),
                ParkingCost = cost
            };

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return View("CheckOutConfirmed", recieptModel);

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
