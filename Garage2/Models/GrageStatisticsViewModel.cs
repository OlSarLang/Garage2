using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class GrageStatisticsViewModel
    {
        public GrageStatisticsViewModel(int vehicleNum, int wheelsNum, double totalVehiclesTime, double totalPrice)
        {
            VehicleNum = vehicleNum;
            WheelsNum = wheelsNum;
            TotalVehiclesPeriod = totalVehiclesTime;
            TotalPrice = totalPrice;
    }

        public int VehicleNum { get; set; } // number of vehicle
        public int WheelsNum { get; set; }
        public double TotalVehiclesPeriod { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
