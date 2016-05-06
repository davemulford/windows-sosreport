using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace sosreport
{
    public static class EventLogFinder
    {
        public static void GenerateFile(string filename, string logName)
        {
            Console.WriteLine($"Getting event logs for {logName}");

            File.AppendAllLines(filename, new[] {
                $"{"LEVEL", -20} {"DATE", -25} {"Source", -30} {"EventID", -8} MESSAGE"
            });

            using (var eventLog = new EventLog(logName))
            {
                EventLogEntry[] entries = new EventLogEntry[eventLog.Entries.Count];
                eventLog.Entries.CopyTo(entries, 0);

                foreach (EventLogEntry entry in entries.Where(e => e.TimeGenerated > DateTime.Today.AddDays(-60)).OrderBy(e => e.TimeGenerated))
                {
                    File.AppendAllLines(filename, new[] {
                        $"{entry.EntryType,-20} {entry.TimeGenerated,-25} {entry.Source,-30} {entry.InstanceId,-8} {entry.Message}"
                    });
                }
            }
        }
    }
}