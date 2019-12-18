using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Extensions
{
    public static class MyExtensions
    {
        public static DateTime RoundDateTime(this DateTime dt)
        {
            DateTime rounded = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            Debug.WriteLine($"{dt}");
            Debug.WriteLine($"{rounded}");
            if(dt.Second < 30)
            {
                rounded = rounded.AddSeconds(-dt.Second);
            }
            if(dt.Second >= 30)
            {
                rounded = rounded.AddMinutes(1);
                rounded = rounded.AddSeconds(-dt.Second);
            }
            return rounded;
        }
    }
}
