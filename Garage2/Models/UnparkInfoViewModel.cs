using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class UnparkInfoViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public string RegNr { get; set; }
        public string Manufacturer { get; set; }
        public int NOWheels { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public TypeOfVehicle? Type { get; set; }
        public DateTime BeginParking { get; set; }
        public DateTime EndParking { get; set; }
        public double Period { get; set; }
        public double Price { get; set; }
    }
}
