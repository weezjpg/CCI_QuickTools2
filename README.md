# CCI QuickTools2

**CCI QuickTools2** is a Windows administration and support tool built for IT teams that manage users, computers, and network issues in corporate environments.

Instead of switching between Active Directory consoles, remote desktop sessions, PowerShell windows, SCCM tools, and network utilities, the application brings everything together into a single interface focused on speed, clarity, and day-to-day support work.

The goal is simple: reduce repetitive tasks, speed up troubleshooting, and make remote support easier.

---

# What the Application Does

CCI_QuickTools2 helps IT technicians and administrators:

- Search and manage Active Directory users, computers, groups, and shared mailboxes.
- Diagnose remote computer problems without connecting through Remote Desktop.
- Run maintenance and repair actions remotely.
- Monitor system health and connectivity.
- Automate common support tasks.
- Extend functionality through custom plugins and scripts.

The application is designed for real-world helpdesk and infrastructure environments where speed and centralized access matter.

---

# Main Features

## Unified Search

The application includes a global search system that allows technicians to quickly find:

- Users
- Computers
- Groups
- Shared mailboxes (SMX)

Shortcuts can be used to speed up searches:

- `@` Users
- `#` Computers
- `$` Shared mailboxes
- `%s` Groups

This reduces the need to manually browse multiple Active Directory consoles.

---

## Active Directory Management

CCI_QuickTools2 provides a simplified interface for common AD operations, including:

- Browsing Organizational Units (OUs)
- Searching users and computers
- Checking locked accounts
- Viewing group memberships
- Managing mail delegations
- Comparing group permissions

The application also predicts account auto-unlock times when lockout policies are configured.

---

## Remote Diagnostics

The application can remotely inspect computers and detect common problems such as:

- Stopped services
- SCCM issues
- WMI failures
- DNS problems
- Group Policy issues

Health information is displayed in real time, including:

- CPU usage
- RAM usage
- Disk activity
- Network status

When problems are detected, contextual repair actions become available automatically.

Examples:
- Restart SCCM services
- Reset DNS configuration
- Flush network settings
- Trigger Group Policy updates

---

## Network Tools

Built-in network utilities allow technicians to quickly diagnose connectivity problems.

Included tools:
- Ping and connectivity testing
- Trace Route
- Port testing
- DNS flush
- DHCP renewal
- Winsock reset
- TCP/IP stack repair

The application can also monitor:
- Gateway connectivity
- Internal network access
- Internet availability

---

## Remote Management

CCI_QuickTools2 supports several remote administration actions without requiring full remote desktop access.

Examples include:
- Running `gpupdate`
- Triggering SCCM inventory cycles
- Sending remote messages
- Restarting computers
- Browsing administrative shares (`C$`)
- Launching SCCM Remote Control sessions

---

## Driver Installation and User Backups

The application includes support utilities for workstation preparation and migration tasks.

Features include:
- Installing drivers from shared folders using `.inf` files
- Creating user profile backups over network paths
- Using centralized UNC locations for deployment resources

---

# Plugin System

CCI_QuickTools2 includes a modular plugin system located in the `Plugins/` folder.

Plugins are made of:
- A JSON manifest file (`.plugin.json`)
- A PowerShell script (`.ps1`)

The JSON file defines:
- Plugin metadata
- Input fields
- Parameters
- Execution settings

The application automatically generates the required user interface based on the manifest.

Plugins can also be loaded from a shared network location, allowing centralized distribution across teams.

---

# Technologies Used

The application is built using modern Windows desktop technologies.

## Core Stack

- **C#**
- **WPF**
- **.NET 8**
- **MVVM Architecture**

## Main Libraries

- `CommunityToolkit.Mvvm`
- `Microsoft.Extensions.DependencyInjection`
- `FluentWPF`

---

# Infrastructure Integrations

CCI_QuickTools2 interacts directly with standard Windows enterprise technologies:

- **Active Directory (LDAP)**
- **WMI**
- **PowerShell**
- **SCCM / Microsoft Configuration Manager**
- **Windows Registry**

This allows the application to retrieve system information, execute remote operations, and automate administrative tasks.

---

# Configuration

Application settings are stored locally in:

```text
%LocalAppData%\CCI_QuickTools2\settings.json
```

Configuration options include:
- Default Active Directory search locations
- Plugin paths
- Driver repositories
- Backup locations

Both local folders and network paths are supported.

---

# Project Structure

```text
CCI_QuickTools2/
 ├── Models/        # Shared data structures
 ├── ViewModels/    # Application logic
 ├── Views/         # User interface
 ├── Services/      # AD, Network, SCCM and diagnostics logic
 ├── Plugins/       # External scripts and modules
 ├── Converters/    # UI converters
 ├── Resources/     # Styles and assets
 └── App.xaml       # Application startup and configuration
```

---

# Requirements

## Development

- Visual Studio 2022
- .NET Desktop Development workload
- .NET 8 SDK

## Runtime

Most features require:
- Windows environment
- Domain connectivity
- Administrative permissions

---

# Technical Notes

- Active Directory searches are asynchronous to keep the UI responsive.
- The application uses the current Windows user context for authentication.
- Release builds are configured as single-file self-contained executables.

---

# Purpose of the Project

CCI_QuickTools2 was created to reduce operational overhead in IT support environments.

Instead of relying on multiple disconnected tools and manual scripts, the application provides a centralized workspace focused on:
- Faster troubleshooting
- Reduced repetitive work
- Easier remote support
- Better operational visibility

It is intended for helpdesk teams, system administrators, and infrastructure technicians working in Windows enterprise environments.
