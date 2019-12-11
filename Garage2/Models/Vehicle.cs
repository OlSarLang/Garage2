using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
    [Table("vehicledetails")]
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "RegNr is too long")]
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int NOWheels { get; set; }
        public int Type { get; set; }
        public enum TypeOfVehicle
        {
            Sedan,
            SUV,
            MC,
            Bus,
            Limo
        }

    }
}
