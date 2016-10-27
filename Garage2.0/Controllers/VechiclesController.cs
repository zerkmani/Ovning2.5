using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Garage2._0.DAL;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class VechiclesController : Controller
    {
        public VehicleDbContext db = new VehicleDbContext();

        const int GarageSize = 11;  //hardcoded garagesize
       // Vechicle[] Parkingplaces; //initiate an array in the controller





        /// <summary>
        /// Search method for vechicles using registration number
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        // GET: Vechicles
        //public ActionResult Index()
        //{
        //    return View(db.Vehicles.ToList());
        //}
        public ActionResult Index(string searchString, string sort)
        {

            int NrOfFreeSpots = GarageSize - db.Vehicles.Count();

            TempData["GarageSize"] = GarageSize;
            TempData["NrOfFreeSpots"] = NrOfFreeSpots;

            ViewBag.VechicleSortParm = sort == "VechicleType" ? "VechicleTypeAsc" : "VechicleType";
            ViewBag.RegNoSortParm = sort == "RegNo" ? "RegNoAsc" : "RegNo";
            ViewBag.BrandSortParm = sort == "Brand" ? "BrandAsc" : "Brand";
            ViewBag.ParkingTimeSortParm = sort == "ParkingTime" ? "ParkingTimeAsc" : "ParkingTime";
            var model = db.Vehicles.ToList();

            switch (sort)
            {
                //case "VechicleType":
                //    model = model.OrderByDescending(m => m.VechicleType).ToList();

                //    return View(model);

                //case "VechicleTypeAsc":
                //    model = model.OrderBy(m => m.VechicleType).ToList();

                //    return View(model);

                case "RegNo":
                    model = model.OrderByDescending(m => m.RegNo).ToList();
                    return View(model);


                case "RegNoAsc":
                    model = model.OrderBy(m => m.RegNo).ToList();
                    return View(model);

                case "Brand":

                    model = model.OrderByDescending(m => m.Brand).ToList();
                    return View(model);

                case "BrandAsc":

                    model = model.OrderBy(m => m.Brand).ToList();
                    return View(model);

                case "ParkingTime":

                    model = model.OrderByDescending(m => m.ParkingTime).ToList();
                    return View(model);

                case "ParkingTimeAsc":

                    model = model.OrderBy(m => m.ParkingTime).ToList();
                    return View(model);
            }


            if (!String.IsNullOrEmpty(searchString)) //if the searchstring is not null or empty a search is performed else return standard view 
            {
                var vechicle = db.Vehicles.Where(v => v.RegNo.Contains(searchString)); //returnera endast hittade bilar om sökningen fungerar
                vechicle.ToList(); //gör om till en lista - view kräver en lista
                return View(vechicle); //presentera i index fönstret
            }
            else return View(db.Vehicles.ToList()); //annars returnera alla bilar som är i garaget just nu 
        }


    

        public ActionResult Search()
        {
            return View();
            }

        
        


        [HttpPost]
        public ActionResult Search(string vehicleType= "Exclude", string color = "Exclude", string regNo = "", string brand = "", int nrOfWeels = 0, string model = "")
        {
            var searchResult = db.Vehicles.ToList();

            //if (vehicleType != "Exclude")
            //    searchResult = searchResult.Where(v => v.VechicleType.ToString() == vehicleType.ToString()).ToList();

            if (color != "Exclude")
                searchResult = searchResult.Where(v => v.Color.ToString() == color.ToString()).ToList();

            if (!String.IsNullOrEmpty(regNo))
                searchResult = searchResult.Where(v => v.RegNo.Contains(regNo)).ToList();

            if (!String.IsNullOrEmpty(brand))
                searchResult = searchResult.Where(v => v.Brand.Contains(brand)).ToList();

            if (nrOfWeels != 0)
                searchResult = searchResult.Where(v => v.NrOfWeels == nrOfWeels).ToList();

            if (!String.IsNullOrEmpty(model))
                searchResult = searchResult.Where(v => v.Brand.Contains(model)).ToList();

            return View("SearchResult", searchResult);
        }

       
        

        //public ActionResult Search(string searchString, string SelectedAttribute)  //denna behöver en vy
        //{

        //    //if SelectedAttribute, perform default search which is search on Regno
        //    if (SelectedAttribute == null || SelectedAttribute == "RegNo") 
        //    {
        //        Index(searchString); //perform search based on RegNo redirect to regnosearch.
        //    }

        //    //else search on SelectedAttribute:Brand,Nr of Weels(exact match),Model
        //    switch (SelectedAttribute)
        //    {
        //        case ("Brand"):
        //            {
        //                var vechicle = db.Vehicles.Where(v => v.Brand.Contains(searchString)); //returnera endast hittade bilar om sökningen fungerar
        //                vechicle.ToList(); //gör om till en lista - view kräver en lista
        //                return View(vechicle); //presentera i index fönstret
        //            }
        //        case ("NrOfWeels"): //denna måste testas. matchar nu exakt antal hjul men hur många träffar får man?
        //            {
        //                //var vechicle = db.Vehicles.Where(v => v.NrOfWeels.(searchString)); 
        //                //vechicle.ToList(); //gör om till en lista - view kräver en lista
        //                //return View(vechicle); //presentera i index fönstret

        //                var vehicle = db.Vehicles.Where(v => v.NrOfWeels == int.Parse(searchString));
        //                vehicle.ToList();
        //                return View(vehicle);
        //            }
        //        case ("Model"):
        //            {
        //                var vechicle = db.Vehicles.Where(v => v.Model.Contains(searchString)); //returnera endast hittade bilar om sökningen fungerar
        //                vechicle.ToList(); //gör om till en lista - view kräver en lista
        //                return View(vechicle); //presentera i index fönstret
        //            }
        //    }
        //    return View(db.Vehicles.ToList()); //annars returnera alla bilar som är i garaget just nu 
        //}

        // GET: Vechicles/Details/5
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

        //GET: Vechicles/CheckIn
        //public ActionResult CheckIn()
        //{
        //    return View();
        //}

        //GET: Vechicles/CheckIn
        public ActionResult CheckIn()
        {

            int vehiclescounter = db.Vehicles.Count();
            if (vehiclescounter >= GarageSize) //error check for the garage size 
            {
                //break indexing and send message to client to resize the garage constant in visual studio for now
                var Fullmodel = db.Vehicles;
                //ViewBag.Message = "The garage is full. Check out some vehicles";
                TempData["TooFewParkingSpots"] = "The garage is full. Check out some vehicles";
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Vechicles/CheckIn
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,VechicleType,RegNo,Brand,Color,NrOfWeels,Model")] Vechicle vechicle)
        {
            vechicle.ParkingTime = DateTime.Now;
            
            //behövs kontroll av att regnr inte redan existerar. 
            var tempRegNo = vechicle.RegNo;
            foreach (var v in db.Vehicles)
            {
                if(tempRegNo==v.RegNo) //regnr finns redan i databasen. avbryt incheckning. behövs meddelande av något slag
                {
                    ViewBag.Message = "You tried to check in a vehicle with an already existing registration number in the garage. Please try again.";
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

            return View(vechicle);
        }

        // GET: Vechicles/Edit/5
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
            return View(vechicle);
        }

        // POST: Vechicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VechicleType,RegNo,Brand,Color,NrOfWeels,Model,ParkingTime")] Vechicle vechicle)
        {

            if (ModelState.IsValid)
            {
               var old = db.Vehicles.AsNoTracking().Where(v => v.Id == vechicle.Id).FirstOrDefault();
               vechicle.ParkingTime = old.ParkingTime;
                db.Entry(vechicle).State = System.Data.Entity.EntityState.Modified; 
                db.SaveChanges(); //den här slänger ett datetime fel vid uppdatering av databasen trots att den inte includerar parkingtime
                                  //datetime sätts till 01-01....

                TempData["Message"] = "Vehicle edited.";
                return RedirectToAction("Index"); //glöm ej sticka in viewbag
            }
            return View(vechicle);
        }

        // GET: Vechicles/CheckIn/5
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

        // POST: Vechicles/CheckOut/5
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

                   recieptModel =  new ReciptViewModel()
                    {
                        RegNo = vehicle.RegNo,
                        CheckInTime = vehicle.ParkingTime,
                        CheckOutTime = checkOutTime,
                        ParkedHour = Convert.ToInt32(hour),
                        ParkingCost = cost
                    };

            db.Vehicles.Remove(vehicle);                         
            db.SaveChanges();                                    
            return View("CheckOutConfirmed",recieptModel);

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

            //(var suit in Enum.GetValues(typeof(Suits)) db.Vehicles)
            //foreach (var type in db.Vehicles)
            //{
            //    var value = (type.VechicleType);//(Enum.GetValues(typeof(type));
                
            //    string newvalue = value.ToString();
            //    if (newvalue =="Airplane") NrOfAirplanes++;
            //    if (newvalue == "Boat") NrOfBoats++;
            //    if (newvalue == "Bus") NrOfBuses++;
            //    if (newvalue == "Car") NrOfCars++;
            //    if (newvalue == "Motorcycle") NrOfMotorcycles++;
            //}
            ViewData["NrOfAirplanes"] = NrOfAirplanes;
            ViewData["NrOfBoats"] = NrOfBoats;
            ViewData["NrOfBuses"] = NrOfBuses;
            ViewData["NrOfCars"] = NrOfCars;
            ViewData["NrOfMotorcycles"] = NrOfMotorcycles;
            
            //totalt antal hjul
            int NumberOfWheels = 0;
            foreach(var v in db.Vehicles)
            {
                NumberOfWheels += v.NrOfWeels;
            }
            //present the data to client

            ViewData["NumberOfWheels"] = NumberOfWheels;

            //totalt garagepris
            //init relevant variables
            int TotalCost = 0;
            TimeSpan hours ;
            var checkOutTime = DateTime.Now;

            //step through all vehicles. calculate cost and att to totalcost.
            foreach (var vehicle in db.Vehicles)
            {
                hours = checkOutTime - vehicle.ParkingTime;
                var parkedHour = hours.TotalHours;
                var minutes = Convert.ToInt32(parkedHour)*60;
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


