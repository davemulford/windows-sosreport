using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosreport.Extensions
{
    public static class StringExtensions
    {
        public static string SafeSubstring(this string source, int startIndex, int length)
        {
            return new string((source ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }
    }
}
