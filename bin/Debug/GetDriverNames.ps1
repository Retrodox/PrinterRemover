$drivers = Get-PrinterDriver
for ($i = 0; $i -lt $drivers.Count; $i++) {
    $driver = $drivers[$i]
    Write-Output ("{0}. {1}" -f ($i + 1), $driver.Name)
}