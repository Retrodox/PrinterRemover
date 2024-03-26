
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PrinterNamesCLI
{
    public static class Utilities
    {
        public static List<string> RunScript(string scriptPath, string arguments = "")
        {
            List<string> outputLines = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\" {arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    outputLines.Add(line);
                }
                process.WaitForExit();
            }
            return outputLines;
        }

        public static string CombinePathWithExecutable(string fileName) => Path.Combine(GetCurrentDirectory(), fileName);

        public static string GetCurrentDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static List<string> RunCommand(string command)
        {
            List<string> outputLines = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    outputLines.Add(line);
                }
                process.WaitForExit();
            }
            return outputLines;
        }
    }
}
