param(
    [int]$PrinterNumber
)

$printers = Get-WmiObject -Query "SELECT * FROM Win32_Printer"
if ($PrinterNumber -le 0 -or $PrinterNumber -gt $printers.Count) {
    Write-Error "Invalid printer number."
    exit
}

$printerToRemove = $printers[$PrinterNumber - 1]
$result = $printerToRemove.Delete()
if ($result.ReturnValue -eq 0) {
    Write-Output ("Removed printer: " + $printerToRemove.Name)
} else {
    Write-Output ("Failed to remove printer: " + $printerToRemove.Name + ". Error code: " + $result.ReturnValue)
}
