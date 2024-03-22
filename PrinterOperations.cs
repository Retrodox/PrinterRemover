using System.Collections.Generic;

namespace PrinterNamesCLI
{
    public static class PrinterOperations
    {
        public static List<string> ListPrinters() => Utilities.RunScript(Utilities.CombinePathWithExecutable("GetNames.ps1"));

        public static void RemovePrinter(int printerNumber) => Utilities.RunScript(Utilities.CombinePathWithExecutable("RemoveName.ps1"), printerNumber.ToString());

        public static List<string> ListPrinterDrivers() => Utilities.RunScript(Utilities.CombinePathWithExecutable("GetDriverNames.ps1"));

        public static void RemovePrinterDriver(string driverName) => DriverRemove.RemovePrinterDriver(driverName);
    }
}
