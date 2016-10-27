using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Garage2._0.DAL
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Models.Vechicle> Vehicles { get; set; }

        public DbSet<Models.Member> Members { get; set; }

        //public DbSet<Course> Courses { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }

    }

}