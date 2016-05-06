using System;
using System.IO;
using System.Linq;

namespace sosreport
{
    public static class FreeSpaceFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Finding free space on all drives");

            File.AppendAllLines(filename, new[] {
                $"{"NAME", -20} {"FORMAT", -10} {"TYPE", -10} {"USED_MiB", 10} {"FREE_MiB", 10} {"USE %", 5} ROOT"
            });

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                long usedBytes = (drive.TotalSize - drive.TotalFreeSpace);
                long usedPercent = usedBytes / drive.TotalSize * 100;

                string usedMib = (usedBytes / 1048576).ToString("N0");
                string freeMib = (drive.TotalFreeSpace / 1048576).ToString("N0");

                File.AppendAllLines(filename, new[] {
                    $"{drive.Name, -20} {drive.DriveFormat, -10} {drive.DriveType, -10} {usedMib, 10} {freeMib, 10} {usedPercent, 4}% {drive.RootDirectory}"
                });
            }
        }
    }
}