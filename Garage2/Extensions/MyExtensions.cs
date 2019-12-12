using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Extensions
{
    public class MyExtensions
    {
        public MyExtensions()
        {

        }
        public DateTime RoundDateTime(DateTime dt)
        {
            DateTime rounded = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            if(dt.Minute < 30)
            {
                rounded = rounded.AddHours(-1);
            }
            if(dt.Minute >= 30)
            {
                rounded = rounded.AddHours(1);
            }
            return rounded;
        }
    }
}
