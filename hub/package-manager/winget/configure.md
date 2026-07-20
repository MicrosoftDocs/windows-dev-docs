---
title: configure Command
description: Uses a winget configuration file to begin setting up your Windows machine to a desired development environment state.
ms.date: 07/19/2026
no-loc: [winget, configuration, configure, dsc, export, import, install, --accept-configuration-agreements, --all, --disable, --disable-interactivity, --enable, --file, --help, --history, --ignore-warnings, --logs, --module, --module-path, --no-proxy, --nowarn, --open-logs, --package-id, --processor-path, --proxy, --resource, --suppress-initial-details, --verbose, --verbose-logs, --wait]
ms.topic: overview
---

# configure command (winget)

The **configure** command of the [winget](./index.md) tool uses a [WinGet Configuration file](../configuration/index.md) to begin setting up your Windows machine to a desired development environment state. A configuration file can specify a collection of packages to install alongside other system settings, making it the most complete approach for reproducible environment setup.

> [!TIP]
> For simpler scenarios, you can install multiple packages in a single command (`winget install A B C`), or use [**winget export**](export.md) and [**winget import**](import.md) to save and restore a package list.

> [!WARNING]
> Do not run a WinGet Configuration file without first reviewing the contents of the file and verifying the credibility of the related resources. See [How to check the trustworthiness of a WinGet Configuration file](../configuration/check.md).

## Prerequisites

- Windows 10 version 1809 (build 17763) or later, or Windows 11.
- WinGet version [v1.6.2631 or later](https://github.com/microsoft/winget-cli/releases).

## Aliases

The following aliases are available for this command:

- `configuration`
- `dsc`

## Usage

`winget configure -f <C:/Users/<username>/winget-configs/config-file-name.dsc.winget>`

Once you have identified the WinGet configuration file that you are interested in using, confirmed the safety and trustworthiness of that file, and downloaded the file to your local machine, you can use the `winget configure` command to initiate the set up of your configuration.

:::image type="content" source="./images/configure.png" alt-text="Screenshot listing winget configure command options." lightbox="./images/configure.png":::

## Arguments and options

The following arguments are available:

| Argument | Description |
|--------------|-------------|
| -f,--file | The path to the winget configuration file. |
| --module-path | Specifies the location on the local computer to store modules. Default %LOCALAPPDATA%\Microsoft\WinGet\Configuration\Modules. |
| --processor-path | Specifies the path to the configuration processor. |
| -h,--history | Select items from history. |
| --accept-configuration-agreements | Accepts the configuration warning, preventing an interactive prompt. |
| --suppress-initial-details | Suppress showing initial configuration details when possible. |
| --enable | Enable configuration components. Requires store access. |
| --disable | Disable configuration components. Requires store access. |
| -?,--help | Shows help about the selected command. |
| --wait | Prompts the user to press any key before exiting. |
| --logs,--open-logs | Open the default logs location. |
| --verbose,--verbose-logs | Enables verbose logging for winget. |
| --nowarn,--ignore-warnings | Suppresses warning outputs. |
| --disable-interactivity | Disable interactive prompts. |
| --proxy | Set a proxy to use for this execution. |
| --no-proxy | Disable the use of proxy for this execution. |

## configure subcommands

The `winget configure` command includes a number of subcommands, including:

- **`winget configure show`**: Displays the details of a configuration file. Usage: `winget configure show -f <file>`. Running the command: `winget configuration show configuration.dsc.yaml` will display the details of the configuration in the current working directory.
- **`winget configure list`**: Shows the high level details for configurations that have been applied to the system. This data can then be used with other `configure` commands to get more details. Usage: `winget configure list [<options>]`
- **`winget configure test`**: Checks the system against the desired state, displaying whether the current system state conforms with the desired state defined in the associated configuration file. Usage: `winget configure test -f <file>`.
- **`winget configure validate`**: Validates a configuration file. Usage: `winget configure validate [-f] <file> [<options>]`.
- **`winget configure export`**: Exports configuration resources to a configuration file. When used with `--all`, exports all package configurations. When used with `--package-id`, exports a WinGetPackage resource of the given package identifier. When used with `--module` and `--resource`, exports the settings of the specified resource. If the output configuration file already exists, appends the exported configuration resources. Usage: `winget configure export -o <output file> [<options>]`

## Related topics

- [WinGet Configuration overview](../configuration/index.md)
- [How to author a WinGet Configuration file](../configuration/create.md)
- [How to check the trustworthiness of a WinGet Configuration file](../configuration/check.md)
- [Use the winget tool to install and manage applications](index.md)
- [Submit packages to Windows Package Manager](../package/index.md)
