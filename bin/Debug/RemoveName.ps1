param(
    [int]$PrinterNumber
)

$printers = Get-Printer
if ($PrinterNumber -le 0 -or $PrinterNumber -gt $printers.Count) {
    Write-Error "Invalid printer number."
    exit
}

$printerToRemove = $printers[$PrinterNumber - 1]
Remove-Printer -Name $printerToRemove.Name
Write-Output ("Removed printer: " + $printerToRemove.Name)