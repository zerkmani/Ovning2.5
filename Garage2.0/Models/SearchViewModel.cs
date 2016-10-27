using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class SearchViewModel
    {
        //[StringLength(6)]
        //[RegularExpression(pattern: "^[a-zA-Z]{3}[0-9]{3}")]
        //[Display(Name = "Reg Number")]
        public string RegNo { get; set; }

        //[Display(Name = "Vechicle type")]
        public VechicleTypeS VechicleType { get; set; }

        public WColorS Color { get; set; }

        //[Required]
        //[StringLength(30)]
        public string Brand { get; set; }

        //[Required]
        //[Display(Name = "Check in time")]
        //public DateTime ParkingTime { get; set; }

        //[Required]
        //[Display(Name = "Nr of Weels")]
        //[Range(1, 20)]
        public int NrOfWeels { get; set; }

        //[Required]
        //[StringLength(30)]
        public string Model { get; set; }


    }

    public enum VechicleTypeS
    {
        Exclude,
        Car,
        Bus,
        Boat,
        Airplane,
        Motorcycle
    }

    public enum WColorS
    {
        Exclude,
        Black,
        White,
        Blue,
        Red,
        Green,
        Yellow,
        Grey,
        Orange,
        Pink,
        Silver,
        Gold

    }
}
