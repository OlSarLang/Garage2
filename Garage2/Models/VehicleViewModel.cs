using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string RegNr { get; set; }
        public string Manufacturer { get; set; }
        public int Type { get; set; }
    }
}
