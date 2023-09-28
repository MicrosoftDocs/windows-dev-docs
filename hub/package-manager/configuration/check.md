---
title: How to check a WinGet Configuration
description: Learn how to check a WinGet Configuration.
ms.date: 05/23/2023
ms.topic: overview
---

# How to check the trustworthiness of a WinGet Configuration file

Prior to running a WinGet Configuration file, it is recommended to review and evaluate each resource listed in the file, ensuring that you are fully aware of what is being installed, changed, or applied to your operating system, and that it is coming from a credible and secure source.

## Security notifications and approvals

Before running a configuration, the user is prompted (unless they explicitly pass the configuration agreement acceptance parameter) to review and acknowledge their responsibility to verify a configuration.

Due to the unattended setup benefit that WinGet Configuration files enables, the number of explicit installation notifications and approvals is significantly reduced. Instead, using a WinGet Configuration file requires a diligent security check of the file upfront, prior to running the configuration with the `winget configure` command. You are responsible for reviewing each package that will be installed and each [PowerShell Desired State Configuration (DSC)](/powershell/dsc/overview) module that will be utilized to ensure that it's coming from a reliable source.

Be aware that:

- Users who run a configuration via `winget configure` in an administrative shell will not be prompted for changes to the system made in administrative context.

- Users who run a configuration via `winget configure` in user context may only receive a single User Account Control (UAC) prompt for elevation for the entire configuration.

## Review configuration resources

WinGet Configuration leverages [PowerShell DSC](/powershell/dsc/overview) to apply a configuration to the users system. The configuration file specifies which PowerShell DSC resources will be used to apply the desired state. Each DSC Resource should be reviewed before agreeing to run the configuration file.

To review PowerShell DSC Resources:

- The PowerShell [`Get-PSRepository`](/powershell/module/powershellget/get-psrepository) cmdlet can be used to view configured repositories and determine where resources will be sourced prior to executing the file.

When reviewing configuration resources, be aware that:

- [PowerShell DSC Resources](/powershell/dsc/concepts/resources) can be configured to run any arbitrary code, including but not limited to pulling down and executing additional DSC Resources and binaries to the local machine. This requires a diligent integrity check of the resource and credibility of the publisher. For example, the [DSC Script Resource](/powershell/dsc/reference/resources/windows/scriptresource) provides a mechanism to run Windows PowerShell script blocks on target nodes (using Get, Set, and Test scripts). Do not run script resources from untrusted publishers without reviewing the scripts contents.

- The [PowerShell Gallery](https://www.powershellgallery.com/) is a central repository for discovering, sharing, and acquiring PowerShell modules, scripts, and DSC Resources. This repository is **not** verified by Microsoft and contains resources from a variety of authors and publishers that should not be trusted by default. Each package has a specific page in the Gallery with associated metadata with `Owner` field being strongly tied to the Gallery account (more trustworthy than the Author field). If you discover a package that you feel isn't published in good faith, select "Report Abuse" on that package's page. [Learn more about the PowerShell Gallery](/powershell/gallery/getting-started).

## Test configuration files

We recommend testing all WinGet Configuration files in a clean and isolated environment. A few testing options include:

- [Download a Windows 11 virtual machine to run via virtualization](https://developer.microsoft.com/windows/downloads/virtual-machines/)

- [Create a Windows virtual machine in the Azure portal](/azure/virtual-machines/windows/quick-create-portal)

- [Run Windows Sandbox - a lightweight, isolated, temporary desktop environment](/windows/security/threat-protection/windows-sandbox/windows-sandbox-overview),
