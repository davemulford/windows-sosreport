using System;
using System.IO;
using System.Collections;

namespace sosreport
{
    public static class EnvironmentVariableFinder
    {
        public static void GenerateFile(string filename, EnvironmentVariableTarget target)
        {
            Console.WriteLine($"Getting environment variables for {target}");

            foreach (DictionaryEntry env in Environment.GetEnvironmentVariables(target))
            {
                File.AppendAllLines(filename, new[] {
                    $"{env.Key} {env.Value}"
                });
            }
        }
    }
}