using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PrinterNamesCLI
{
    public static class DriverRemove
    {
        [DllImport("winspool.drv", EntryPoint = "DeletePrinterDriver", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeletePrinterDriver(string pName, string pEnvironment, string pDriverName);

        public static void RemovePrinterDriver(string driverName)
        {
            Console.WriteLine($"Attempting to delete printer driver with name: '{driverName}'");
            string pName = null; // Null for the local machine
            string pEnvironment = "Windows x64"; // Assuming most systems; adjust as needed

            bool success = DeletePrinterDriver(pName, pEnvironment, driverName);
            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine($"Failed to delete printer driver '{driverName}'. Error code: {error}");
            }
            else
            {
                Console.WriteLine($"Printer driver '{driverName}' deleted successfully.");
            }
        }

        public static void RemoveDriverPackage(string infFileName)
        {
            var driversBeforeRemoval = GetDriverPackageList();
            Utilities.RunCommand($"pnputil /delete-driver {infFileName} /uninstall /force");
            var driversAfterRemoval = GetDriverPackageList();

            if (!driversAfterRemoval.Contains(infFileName) && driversBeforeRemoval.Contains(infFileName))
            {
                Console.WriteLine($"Driver package {infFileName} removed successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to remove driver package {infFileName} or it was not present.");
            }
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
