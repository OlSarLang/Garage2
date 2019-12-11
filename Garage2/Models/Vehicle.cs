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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required] [StringLength(12, ErrorMessage = "RegNr is too long")]
        public string RegNr { get; set; }
        public string Color { get; set; }
        [StringLength(32)]
        public string Model { get; set; }
        [Required] [StringLength(32)]
        public string Manufacturer { get; set; }
        [Range(0, 12)]
        public int NOWheels { get; set; }
        [Required]
        public TypeOfVehicle Type { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Begun parking")]
        public DateTime BeginParking { get; set; }
    }
    public enum TypeOfVehicle
    {
        Sedan,
        SUV,
        MC,
        Bus,
        Limo
    }
}
