param(
    [string]$infFileName
)

# Use pnputil to remove the driver package by its .inf filename
$command = "pnputil /delete-driver $infFileName /uninstall /force"
Invoke-Expression $command
