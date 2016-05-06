using System;
using System.IO;
using System.Linq;
using System.Management;

namespace sosreport
{
    public static class VersionFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Getting OS version");

            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();

            string osVersion = (name != null)
                ? name.ToString()
                : Environment.OSVersion.ToString();

            string bitness = Environment.Is64BitOperatingSystem
                ? "64-Bit"
                : "32-Bit";

            File.AppendAllLines(filename, new[] {
                $"{osVersion} {bitness}"
            });
        }
    }
}