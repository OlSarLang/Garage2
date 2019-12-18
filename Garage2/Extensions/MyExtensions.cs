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
            DateTime rounded = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            Debug.WriteLine($"{dt}");
            Debug.WriteLine($"{rounded}");
            if(dt.Minute < 30)
            {
                rounded = rounded.AddMinutes(-dt.Minute);
            }
            if(dt.Minute >= 30)
            {
                rounded = rounded.AddHours(1);
                rounded = rounded.AddMinutes(-dt.Minute);
            }
            return rounded;
        }
    }
}
