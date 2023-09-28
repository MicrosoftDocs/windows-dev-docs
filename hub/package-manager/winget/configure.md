---
title: configure Command
description: Uses a winget configuration file to begin setting up your Windows machine to a desired development environment state.
ms.date: 05/23/2023
ms.topic: overview
ms.localizationpriority: medium
---

# configure command (winget)

The **configure** command of the [winget](./index.md) tool uses a [WinGet Configuration file](../configuration/index.md) to begin setting up your Windows machine to a desired development environment state.

> [!WARNING]
> Do not run a WinGet Configuration file without first reviewing the contents of the file and verifying the credibility of the related resources. See [How to check the trustworthiness of a WinGet Configuration file](../configuration/check.md).

## Prerequisites

- Windows 10 RS5 or later, and Windows 11.
- WinGet version [v1.6.2631 or later](https://github.com/microsoft/winget-cli/releases).

## Usage

`winget configure -f <C:/Users/<username>/winget-configs/config-file-name.dsc.yaml>`

Once you have identified the winget configuration file that you are interested in using, confirmed the safety and trustworthiness of that file, and downloaded the file to your local machine, you can use the `winget configure` command to  initiate the set up of your configuration.

## Arguments and options

The following arguments are available:

| Argument  | Description |
|--------------|-------------|
|-f,--file |  The path to the winget configuration file. |
|-?, --help |  Gets additional help on this command. |
|--wait | Prompts the user to press any key before exiting. |
|--verbose, --verbose-logs | Enables verbose logging for winget. |
|--disable-interactivity | Disable interactive prompts. |

## configure subcommands

The `winget configure` command includes a number of subcommands, including:

- **`winget configure show`**: Displays the details of a configuration file. Usage: `winget configure show -f <C:/Users/<username>/winget-configs/config-file-name.dsc.yaml>`. Running the command: `winget configuration show configuration.dsc.yaml` will display the details of the configuration in the current working directory.
- `winget configure test`: Checks the system against the desired state, displaying whether the current system state conforms with the desired state defined in the associated configuration file. Usage: `winget configure test -f <C:/Users/<username>/winget-configs/config-file-name.dsc.yaml>`.
- `winget configure validate`: Validates a configuration file. Usage: `winget configure validate [-f] <file> [<options>]`.

## Related topics

- [WinGet Configuration overview](../configuration/index.md)
- [How to author a WinGet Configuration file](../configuration/create.md)
- [How to check the trustworthiness of a WinGet Configuration file](../configuration/check.md)
- [Use the winget tool to install and manage applications](index.md)
- [Submit packages to Windows Package Manager](../package/index.md)
