using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel()
        {

        }
        public int AmountSedan { get; set; }
        public int AmountSUV { get; set; }
        public int AmountMC { get; set; }
        public int AmountBus { get; set; }
        public int AmountLimo { get; set; }
        public int TotalWheels { get; set; }
        public int TotalVehicles { get; set; }
        public int TotalMinutes { get; set; }
        public int TotalPrice { get; set; }
    }
}
