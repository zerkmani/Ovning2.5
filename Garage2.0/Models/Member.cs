using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Vechicle> Vehicles { get; set; }
    }
}