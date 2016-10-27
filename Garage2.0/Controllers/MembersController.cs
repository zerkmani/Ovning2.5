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
    public class MembersController : Controller
    {
        private VehicleDbContext db = new VehicleDbContext();

        // GET: Members
        public ActionResult Index()
        {
            //if (!String.IsNullOrEmpty(searchString)) //if the searchstring is not null or empty a search is performed else return standard view 
            //{
            //  var memberlist = db.Members.Where(v => v.Name.Contains(searchString)); //returnera endast hittade bilar om sökningen fungerar
            //    memberlist.ToList(); //gör om till en lista - view kräver en lista
            //    return View(memberlist); //presentera i index fönstret
            //}
            //else return View(db.Vehicles.ToList()); //annars returnera alla bilar som är i garaget just nu 
            return View(db.Members.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            
            if (!String.IsNullOrEmpty(searchString)) //if the searchstring is not null or empty a search is performed else return standard view 
            {
                //var temp = searchString.ToUpper();
                //List<string> templist = new List<string>();
                // foreach (var member in db.Members)
                //{
                //    templist.Add(member.Name.ToUpper());
                //}
              

                var memberlist = db.Members.Where(v => v.Name.ToUpper().Contains(searchString.ToUpper())); //returnera endast hittade bilar om sökningen fungerar
                memberlist.ToList(); //gör om till en lista - view kräver en lista
                return View(memberlist); //presentera i index fönstret
            }
            //else return View(db.Vehicles.ToList()); //annars returnera alla bilar som är i garaget just nu 
            return View(db.Members.ToList());
        }
        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
