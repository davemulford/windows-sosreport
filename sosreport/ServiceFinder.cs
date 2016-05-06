using System;
using System.Linq;
using System.ServiceProcess;

using sosreport.Extensions;
using System.Collections.Generic;
using System.IO;

namespace sosreport
{
    public static class ServiceFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Getting service information");

            File.AppendAllLines(filename, new[] {
                $"{"NAME",-50} {"STATUS",-11}"
            });

            foreach (var service in ServiceController.GetServices().OrderBy(s => s.DisplayName))
            {
                File.AppendAllLines(filename, new[] {
                    $"{service.DisplayName.SafeSubstring(0, 45), -50} {service.Status, -11}"
                });
            }
        }
    }
}