using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosreport.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStartTimeString(this DateTime source)
        {
            if (source > DateTime.Today)
            {
                return source.ToString("HH:mm");
            }
            else
            {
                return source.ToString("MMMdd");
            }
        }
    }
}
