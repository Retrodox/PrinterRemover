
# Printer Management CLI Tool

This command-line interface (CLI) tool is designed for managing printers and printer drivers on Windows systems. It provides functionality to list available printers and drivers, remove printers, remove printer drivers, and handle specific errors related to driver removal.

## Features

- List all printers connected to the system.
- Remove a selected printer.
- List all printer drivers installed on the system.
- Remove a selected printer driver.
- Automatically restarts the spooler service if certain errors occur during driver removal.

## Prerequisites

Before running this tool, ensure that you have PowerShell installed on your Windows machine as the tool executes PowerShell scripts for several operations.

## Getting Started

To use this tool, simply clone or download the "Installer" folder which contains the MSI installer along with a Setup.exe

## Manual Removal Advice

The tool advises on manual removal steps if a printer remains visible in the Control Panel or if a driver needs to be completely purged from the system to prevent automatic reinstallation.

## Scripts and Operations

- `GetNames.ps1`: Lists all printers.
- `RemoveName.ps1`: Removes a specified printer by number.
- `GetDriverNames.ps1`: Lists all printer drivers.
- `RemoveDriverPackage.ps1`: Removes a driver package by its .inf filename.

## Developer Notes

This tool is designed with extensibility in mind. Developers can easily update scripts or the .NET code to expand functionality or adapt to different requirements.
