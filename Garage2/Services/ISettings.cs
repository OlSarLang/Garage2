﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Services
{
    public interface ISettings
    {
        public int Capacity { get; set; }

        public int NrOfParkedVehicle { get; }
    }
}
