using System;
using System.Collections.Generic;

namespace PrinterNamesCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var printerLines = PrinterOperations.ListPrinters();

            foreach (var line in printerLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Enter the number of the printer you want to remove (or press Enter to skip):");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= printerLines.Count)
            {
                var selectedPrinterLine = printerLines[choice - 1];
                var parts = selectedPrinterLine.Split('.'); // Split by the dot to separate the number from the name
                if (parts.Length > 1)
                {
                    // Removed the Trim call so the printer name retains any leading or trailing spaces
                    var printerName = parts[1];
                    PrinterOperations.RemovePrinter(choice); // Here you'd remove the printer using its number or adjust to use the name
                }
            }


            // Correct placement of driver removal section
            // New section for listing and removing printer drivers
            var driverLines = PrinterOperations.ListPrinterDrivers();

            Console.WriteLine("\nAvailable Printer Drivers:");
            foreach (var line in driverLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Enter the number of the printer driver you want to remove (or press Enter to skip):");
            input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= driverLines.Count)
            {
                var selectedDriverLine = driverLines[choice - 1];
                var partsDriver = selectedDriverLine.Split('.'); // Declare a new variable 'partsDriver' for this scope
                if (partsDriver.Length > 1)
                {
                    var driverName = partsDriver[1].Trim(); // Use 'partsDriver' here
                    Console.WriteLine($"Selected driver to remove: {driverName}");
                    PrinterOperations.RemovePrinterDriver(driverName);
                }
            }

            Console.WriteLine("Operation complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
