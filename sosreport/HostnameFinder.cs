using System;
using System.IO;

namespace sosreport
{
    public static class HostnameFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Getting hostname");

            File.AppendAllLines(filename, new[] {
                Environment.MachineName
            });
        }
    }
}