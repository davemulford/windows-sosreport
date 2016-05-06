using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

using sosreport.Extensions;

namespace sosreport
{
    public static class ProcessFinder
    {
        public static void GenerateFile(string filename)
        {
            Console.WriteLine("Getting process information");

            // Printer header
            File.AppendAllLines(filename, new[] {
                $"{"USER", -30} {"PID", 6} {"START", -8} {"TIME", -5} COMMAND"
            });

            foreach (var process in Process.GetProcesses())
            {
                Tuple<string, string> processInfo = GetProcessInfo(process.Id);

                try
                {
                    File.AppendAllLines(filename, new[] {
                        $"{processInfo.Item1,-30} {process.Id,6} {process.StartTime.ToStartTimeString(),-8} {process.TotalProcessorTime.ToProcessorTimeString(),-5} {processInfo.Item2 ?? process.ProcessName}"
                    });
                }
                catch (InvalidOperationException) { }
                catch (Win32Exception)
                {
                    // Intentionally left blank as any Win32Exceptions here indicate an "access denied" scenario,
                    // like when the tool is run without full admin privileges
                }
            }
        }

        private static Tuple<string, string> GetProcessInfo(int id)
        {
            string query = $"Select * From Win32_Process Where ProcessID = {id}";

            var searcher = new ManagementObjectSearcher(query);
            var processList = searcher.Get();

            try
            {
                foreach (ManagementObject obj in processList)
                {
                    string[] argList = new string[] { string.Empty, string.Empty };
                    int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));

                    if (returnVal == 0)
                    {
                        // return DOMAIN\user
                        return Tuple.Create(
                            $"{argList[1]}\\{argList[0]}",
                            obj["CommandLine"] != null ? obj["CommandLine"].ToString() : null
                        );
                    }
                }
            }
            catch (ManagementException)
            {
                // Intentionally left empty to catch when a process no longer exists
            }

            return Tuple.Create("NONE FOUND", string.Empty);
        }
    }
}
