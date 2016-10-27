using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{

    public class Vechicle
    {
        public int Id { get; set; }

      //  [Required]
        //[Display(Name = "Vechicle type")]
      //  public VechicleType? VechicleType { get; set; }

        [Required]
        [StringLength(6)]
        [RegularExpression(pattern:"^[a-zA-Z]{3}[0-9]{3}", ErrorMessage ="Only reg number allowed ex.(AAA111)")]  
        [Display(Name ="Reg Number")]
        public string RegNo { get ;set;}

        [Required]
        public WColor? Color { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }

        [Required]
        [Display(Name ="Check in time")]
        public DateTime ParkingTime { get; set; }

        [Required]
        [Display(Name ="Nr of Weels")]
        [Range(0,20)]                         
        public int NrOfWeels { get; set; }

        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Display(Name ="Vehicle type")] //denna går ej igenom. adrian lyckades inte heller.
        public int VehicleTypeId { get; set; }
        public virtual VehicleType vehicleType { get; set; }

        public int MemberId { get; set; } //denna behövs för att EF ska fungera

        public virtual Member GarageMember { get; set; }  // virtual för att få lazy loading
    }

    public enum WColor
    {
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