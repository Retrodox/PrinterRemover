$printers = Get-Printer
for ($i = 0; $i -lt $printers.Count; $i++) {
    $printer = $printers[$i]
    Write-Output ("{0}. {1}" -f ($i + 1), $printer.Name)
}