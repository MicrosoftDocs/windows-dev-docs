---
title: The winget pin command
description: Use the winget pin command and subcommands to list and manage the repositories Windows Package Manager accesses.
ms.date: 06/16/2023
ms.topic: reference
ms.localizationpriority: medium
ms.custom: kr2b-contr-experiment
---

# The winget pin command

The [winget](index.md) tool **pin** command allows you to limit the Windows Package Manager from upgrading a package to specific ranges of versions, or it can prevent it from upgrading a package altogether. A pinned package may still upgrade on its own and be upgraded from outside the Windows Package Manager. 

WinGet pinning supports three types of Package Pinning:
- **Pinning**:
The package is excluded from winget upgrade --all but allows winget upgrade <WinGet package>. You can use the "–include-pinned” argument to let winget upgrade --all include pinned packages.

- **Blocking**:
The package is blocked from winget upgrade --all or winget upgrade <WinGet package>, you will have to unblock the package to let WinGet perform an upgrade.

- **Gating**:
The package is pinned to specific version(s). For example, if a package is pinned to version 1.2.*, any version between 1.2.0 to 1.2.x is considered valid. To allow user override, “–force” can be used with winget upgrade <WinGet package> to override some of the pinning created above.

## Usage

```cmd
winget pin <subcommand> <options>
```
## Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-?, --help** |  Gets additional help on this command. |

## Subcommands

The **pin** command supports the following subcommands.

| Subcommand  | Description |
|--------------|-------------|
|  **add** |  Adds a new pin. |
|  **remove** | Remove a package pin. |
|  **list** | List current pins. |
|  **reset** | Reset pins |

## add

The **add** subcommand adds a new pin. This subcommand requires that you specify the exact package to pin. If there is any ambiguity, you will be prompted to further filter the **add** subcommand to an exact application.

Usage:

```cmd
winget pin add [[-q] <query>] [<options>]
```

### Arguments

| Argument      | Description |
|-------------|-------------|  
| **-q,--query**  |  The query used to search for an app. |
| **-?, --help** |  Get additional help on this command. |

### Options

The options allow you to customize adding pins to meet your needs.

| Option  | Description |
|-------------|-------------|  
| **--id**    |  Limits the search to the ID of the application.   |  
| **--name**   |  Limits the search to the name of the application. |  
| **--moniker**   | Limits the search to the moniker listed for the application. |  
| **--tag**   | Limits the search to the tag listed for the application. | 
| **--cmd, --command**   | Limits the search to the command of the application. |   
| **-v, --version**  |  Enables you to specify an exact version to pin. The wildcard '*' can be used as the last version part. |  
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |  
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |  
| **--force** | Overrides the installer hash check. Not recommended. |
| **--blocking** | Block from upgrading until the pin is removed, preventing override arguments |
| **--installed** | Pin a specific installed version |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--wait** | Prompts the user to press any key before exiting |
| **-o, --log**  |  Directs the logging to a log file. You must provide a path to a file that you have the write rights to. |
| **--verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example adds a pin for an application. Adding this pin will prevent this package from being upgraded when calling `winget upgrade --all`.

> Use the `--include-pinned` argument with `winget upgrade --all` to include any pinned packages.

```CMD
winget pin add powertoys
```

The following example adds a blocking pin for an application using its ID. Adding a blocking pin will prevent this package from being upgraded when calling `winget upgrade --all` or `winget upgrade`. You will need to unblock the package to let WinGet perform an upgrade.

```CMD
winget pin add --id Microsoft.PowerToys --blocking
```

The following example adds a gating pin for an application using its ID. Adding a gating pin will prevent upgrades that upgrade the package version outside of a specific version or the gated wildcard range. 

```CMD
winget pin add --id Microsoft.PowerToys --version 0.70.*
```

## remove

The **remove** subcommand removes a pin. This subcommand requires that you specify the exact package pin to remove. If there is any ambiguity, you will be prompted to further filter the **remove** subcommand to an exact application.

Usage:

```cmd
winget pin remove [[-q] <query>] [<options>]
```

### Arguments

| Argument      | Description |
|-------------|-------------|  
| **-q,--query**  |  The query used to search for an app. |
| **-?, --help** |  Get additional help on this command. |

### Options

The options allow you to customize removing pins to meet your needs.

| Option  | Description |
|-------------|-------------|  
| **--id**    |  Limits the search to the ID of the application.   |  
| **--name**   |  Limits the search to the name of the application. |  
| **--moniker**   | Limits the search to the moniker listed for the application. |  
| **--tag**   | Limits the search to the tag listed for the application. | 
| **--cmd, --command**   | Limits the search to the command of the application. |   
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |  
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |  
| **--installed** | Pin a specific installed version |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--wait** | Prompts the user to press any key before exiting |
| **-o, --log**  |  Directs the logging to a log file. You must provide a path to a file that you have the write rights to. |
| **--verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example removes a pin for an application. 

```CMD
winget pin remove powertoys
```

The following example removes a pin for an application using its ID.

```CMD
winget pin remove --id Microsoft.PowerToys
```

## list

The **list** subcommand lists all current pins, or full details of a specific pin. 

Usage:

```cmd
winget pin list [[-q] <query>] [<options>]
```

### Arguments

| Argument      | Description |
|-------------|-------------|  
| **-q,--query**  |  The query used to search for an app. |
| **-?, --help** |  Get additional help on this command. |

### Options

The options allow you to customize listing pins to meet your needs.

| Option  | Description |
|-------------|-------------|  
| **--id**    |  Limits the search to the ID of the application.   |  
| **--name**   |  Limits the search to the name of the application. |  
| **--moniker**   | Limits the search to the moniker listed for the application. |  
| **--tag**   | Limits the search to the tag listed for the application. | 
| **--cmd, --command**   | Limits the search to the command of the application. |   
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |  
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |  
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--wait** | Prompts the user to press any key before exiting |
| **-o, --log**  |  Directs the logging to a log file. You must provide a path to a file that you have the write rights to. |
| **--verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example lists all current pins. 

```CMD
winget pin list
```

The following example lists the details for a specific package pin.

```CMD
winget pin list --id Microsoft.PowerToys
```

## reset

The reset subcommand resets all pins.

Using this subcommand without the `--force` argument will show the following pins that would be removed.

To reset all pins, include the `--force` argument.

Usage:

The following example shows all pins that will be reset.

```cmd
winget pin reset
```

The following example resets all existing pins.

```cmd
winget pin reset --force
```