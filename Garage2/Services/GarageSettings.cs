using Garage2.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Services
{
    public class GarageSettings : ISettings
    {
        private readonly Garage2Context context;

        public int Capacity { get; set; }

        public GarageSettings(IConfiguration config, Garage2Context context)
        {
            Capacity = config.GetValue<int>("GarageSettings:Capacity");
            this.context = context;
        }

        public int NrOfParkedVehicle => context.Vehicle.Count();

        public bool GarageIsFull()
        {
            if (this.Capacity == context.Vehicle.Count())
                return true;
            else
                return false;
        }

        
    }
}
