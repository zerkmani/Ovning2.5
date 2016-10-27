using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class ReciptViewModel 
    {
        [Display(Name ="Reg Number")]
        public string RegNo { get; set; }

        [Display(Name ="Check in time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name ="Check out time")]
        public DateTime CheckOutTime { get; set; }

        [Display(Name ="Hours parked")]
        public int ParkedHour { get; set; }

        [Display(Name ="Total cost")]
        public int ParkingCost { get; set; }

    }
}