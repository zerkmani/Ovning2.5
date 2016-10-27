using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual List<Vechicle> Vehicles { get; set; } //detta förklarar relationen till EF
    }
}