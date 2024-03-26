
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace PrinterNamesCLI
{
    public static class DriverRemove
    {
        [DllImport("winspool.drv", EntryPoint = "DeletePrinterDriver", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeletePrinterDriver(string pName, string pEnvironment, string pDriverName);

        public static void RemovePrinterDriver(string driverName)
        {
            Console.WriteLine($"Attempting to delete printer driver with name: '{driverName}'");
            if (!AttemptRemoveDriver(driverName))
            {
                HandleError3001(driverName);
            }
        }

        private static bool AttemptRemoveDriver(string driverName)
        {
            string pName = null; // Null for the local machine
            string pEnvironment = "Windows x64"; // Assuming most systems; adjust as needed

            bool success = DeletePrinterDriver(pName, pEnvironment, driverName);
            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine($"Failed to delete printer driver '{driverName}'. Error code: {error}");
                return error != 3001;
            }
            else
            {
                Console.WriteLine($"Printer driver '{driverName}' deleted successfully.");
                return true;
            }
        }

        private static void HandleError3001(string driverName)
        {
            Console.WriteLine("Please wait.. Restarting spooler and testing again..");
            Utilities.RunCommand("net stop spooler && net start spooler");

            // Wait for 10 seconds
            Thread.Sleep(10000);

            // Retry the removal
            if (!AttemptRemoveDriver(driverName))
            {
                Console.WriteLine("Failed to remove the printer driver after retrying.");
            }
        }

        public static void RemoveDriverPackage(string driverName)
        {
            // Find the .inf filename associated with the driver name
            string infFileName = FindDriverInfFilename(driverName);
            if (string.IsNullOrEmpty(infFileName))
            {
                Console.WriteLine($"Could not find the .inf file for driver '{driverName}'.");
                return;
            }

            Console.WriteLine($"Attempting to remove driver package with .inf file: {infFileName}...");

            // Call PowerShell script to remove the driver package
            var scriptPath = Utilities.CombinePathWithExecutable("RemoveDriverPackage.ps1");
            var arguments = $"-infFileName \"{infFileName}\"";
            Utilities.RunScript(scriptPath, arguments);

            Console.WriteLine($"Attempted to remove driver package for '{driverName}'.");
        }

        private static string FindDriverInfFilename(string driverName)
        {
            // Implementation to find the .inf filename
            // This logic needs to be implemented based on your environment and how the drivers are listed
            // For now, this is a placeholder
            return "";
        }

        private static List<string> GetDriverPackageList()
        {
            List<string> driverPackages = new List<string>();
            var outputLines = Utilities.RunCommand("pnputil /enum-drivers");
            foreach (var line in outputLines)
            {
                if (line.Contains("Published Name:"))
                {
                    driverPackages.Add(line.Split(':')[1].Trim());
                }
            }
            return driverPackages;
        }
    }
}
