---
title: pin Command
description: Pin a package to a specific version or within a specific version range using WinGet, the Windows Package Manager client.
ms.date: 07/05/2023
ms.topic: reference
---

# pin command (winget)

The [winget](index.md) **pin** command allows you to limit the Windows Package Manager from upgrading a package to specific ranges of versions, or it can prevent it from upgrading a package altogether. A pinned package may still upgrade on its own and be upgraded from outside the Windows Package Manager.

## Pin Types

WinGet supports three types of package pins:

- **Pinning**: The package is excluded from `winget upgrade --all` but allows `winget upgrade <package>`. You can use the `--include-pinned` argument to let `winget upgrade --all` include pinned packages.

- **Blocking**: The package is blocked from `winget upgrade --all` or `winget upgrade <package>`, you will have to unpin the package to let WinGet perform an upgrade. The `--force` option can be used to override the pin's behavior.

- **Gating**: The package is pinned to a specific version or version range. You can specify an exact version you want a package to be pinned to or you can utilize the wildcard character `*` as the last version part to specify a version range. For example, if a package is pinned to version `1.2.*`, any version between `1.2.0` to `1.2.x` is considered valid. The `--force` option can be used to override the pin's behavior.

## Usage

```cmd
winget pin <subcommand> <options>
```

## Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Gets additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

## Subcommands

The **pin** command supports the following subcommands.

| Subcommand  | Description |
|--------------|-------------|
|  **add** |  Add a new pin. |
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

### Options

The options allow you to customize adding pins to meet your needs.

| Option  | Description |
|-------------|-------------|
| **--id**    |  Limits the search to the ID of the application.   |
| **--name**   |  Limits the search to the name of the application. |
| **--moniker**   | Limits the search to the moniker listed for the application. |
| **--tag**   | Limits the search to the tag listed for the application. |
| **--cmd, --command**   | Limits the search to the command of the application. |
| **-v, --version**  |  Enables you to specify an exact version to pin. The wildcard * can be used as the last version part. Changes pin behavior to be [`gating`](#pin-types). |
| **-s, --source**   |  Restricts the search to the source name provided. Must be followed by the source name. |
| **-e, --exact**   |   Uses the exact string in the query, including checking for case-sensitivity. It will not use the default behavior of a substring. |
| **--force** | Direct run the command and continue with non security related issues. |
| **--blocking** | Block from upgrading until the pin is removed, preventing override arguments. Changes pin behavior to be [`blocking`](#pin-types). |
| **--installed** | Pin a specific installed version |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting |
| **--logs, --open-logs**  | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example adds a pin for an application. Adding this pin will prevent this package from being upgraded when calling `winget upgrade --all`. Use the `--include-pinned` argument with `winget upgrade --all` to include any pinned packages.

```cmd
winget pin add powertoys
```

The following example adds a blocking pin for an application using its ID. Adding a blocking pin will prevent this package from being upgraded when calling `winget upgrade --all` or `winget upgrade <package>`. You will need to unblock the package to let WinGet perform an upgrade.

```cmd
winget pin add --id Microsoft.PowerToys --blocking
```

The following example adds a gating pin for an application using its ID. Adding a gating pin will prevent upgrades that upgrade the package version outside of a specific version or the gated wildcard range.

```cmd
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
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting |
| **--logs, --open-logs**  | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example removes a pin for an application.

```cmd
winget pin remove powertoys
```

The following example removes a pin for an application using its ID.

```cmd
winget pin remove --id Microsoft.PowerToys
```

## list

The **list** subcommand lists all current pins.

Usage:

```cmd
winget pin list [[-q] <query>] [<options>]
```

### Arguments

| Argument      | Description |
|-------------|-------------|
| **-q,--query**  |  The query used to search for an app. |

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
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting |
| **--logs, --open-logs**  | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### Examples

The following example lists all current pins.

```cmd
winget pin list
```

The following example lists a specific package pin.

```cmd
winget pin list --id Microsoft.PowerToys
```

## reset

The reset subcommand resets all pins.

Using this subcommand without the `--force` argument will show the pins that would be removed.

To reset all pins, include the `--force` argument.

Usage:

The following example shows all pins that would be reset.

```cmd
winget pin reset
```

The following example resets all existing pins.

```cmd
winget pin reset --force
```
