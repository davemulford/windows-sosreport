using System;
using System.IO;
using System.Diagnostics;

namespace sosreport
{
    public static class NetworkFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Getting netstat info");

            var process = new Process();
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false // required for RedirectStandardOutput=true
            };

            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit(1);

            File.AppendAllLines(filename, new[] {
                process.StandardOutput.ReadToEnd()
            });
        }
    }
}