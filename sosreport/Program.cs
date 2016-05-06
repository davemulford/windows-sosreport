using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosreport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the case number for this sosreport: ");
            string caseNumber = Console.ReadLine();

            string dateString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string dirName = $"sosreport-windows-{caseNumber}-{dateString}";

            string fullUserDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullDir = Path.Combine(fullUserDir, dirName);

            Console.WriteLine($"Contents will be staged in '{fullDir}'");

            if (Directory.Exists(fullDir))
            {
                Directory.Delete(fullDir);
            }

            Directory.CreateDirectory(fullDir);

            ProcessFinder.GenerateFile(Path.Combine(fullDir, "ps.txt"));
            ServiceFinder.GenerateFile(Path.Combine(fullDir, "services.txt"));
            FreeSpaceFinder.GenerateFile(Path.Combine(fullDir, "df.txt"));
            NetworkFinder.GenerateFile(Path.Combine(fullDir, "netstat.txt"));
            VersionFinder.GenerateFile(Path.Combine(fullDir, "uname.txt"));
            HostnameFinder.GenerateFile(Path.Combine(fullDir, "hostname.txt"));
            EnvironmentVariableFinder.GenerateFile(Path.Combine(fullDir, "user-env-variables.txt"), EnvironmentVariableTarget.User);
            EnvironmentVariableFinder.GenerateFile(Path.Combine(fullDir, "system-env-variables.txt"), EnvironmentVariableTarget.Machine);
            EventLogFinder.GenerateFile(Path.Combine(fullDir, "application-eventlog.txt"), "Application");
            EventLogFinder.GenerateFile(Path.Combine(fullDir, "system-eventlog.txt"), "System");

            // Zip contents and delete staging folder
            ZipFile.CreateFromDirectory(fullDir, $"{fullDir}.zip", CompressionLevel.Fastest, includeBaseDirectory: true);
            Console.WriteLine($"sosreport written to '{fullDir}.zip'");

            // Cleanup
            Directory.Delete(fullDir, recursive: true);

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to exit..");
            Console.ReadLine();
        }
    }
}
