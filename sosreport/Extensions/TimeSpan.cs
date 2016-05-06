using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosreport.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToProcessorTimeString(this TimeSpan source)
        {
            return source.ToString(@"m\:ss");
        }
    }
}
