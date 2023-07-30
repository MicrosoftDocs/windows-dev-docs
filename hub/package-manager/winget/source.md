---
title: The winget source command
description: Use the winget source command and subcommands to list and manage the repositories Windows Package Manager accesses.
ms.date: 06/22/2022
ms.topic: reference
ms.localizationpriority: medium
ms.custom: kr2b-contr-experiment
---

# The winget source command

The [winget](index.md) tool **source** command allows you to manage sources for Windows Package Manager. With the **source** command, you can **add**, **list**, **update**, **remove**, **reset**, or **export** repositories.

A source repository provides the data for you to discover and install applications. Only use secure, trusted source locations.

Windows Package Manager specifies the following two default repositories, which you can list by using `winget source list`.

- **msstore** - The Microsoft Store catalog.
- **winget** -  The Windows Package Manager app repository.

## Usage

```cmd
winget source <subcommand> <options>
```
## Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-?, --help** |  Gets additional help on this command. |

The following image shows **help** for the **source** command:

:::image type="content" source="images/source.png" alt-text="Screenshot showing help for the source command.":::

## Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?,--help** | Shows help about the selected command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

## Subcommands

The **source** command supports the following subcommands.

| Subcommand  | Description |
|--------------|-------------|
|  **add** |  Adds a new source. |
|  **list** | Enumerates the list of enabled sources. |
|  **update** | Updates a source. |
|  **remove** | Removes a source. |
|  **reset** | Resets **winget** and **msstore** back to the initial configuration. |
|  **export** |  Exports current sources. |

### add

The **add** subcommand adds a new source. This subcommand requires the **--name** and **--arg** options. Because the command changes user access, using **add** requires administrator privileges.

Usage:

```cmd
winget source add [-n, --name] <name> [-a, --arg] <url> [[-t, --type] <type>]
```

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |
| **-a, --arg** | The URL or UNC of the source. |
| **-t, --type** | The type of source. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **--header** | Optional Windows-Package-Manager REST source HTTP header. |
| **--accept-source-agreements** | Used to accept the source license agreement, and avoid the prompt. |
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

For example,  `winget source add --name Contoso https://www.contoso.com/cache` adds the Contoso repository at URL `https://www.contoso.com/cache`.

#### Optional type parameter

The **add** subcommand supports the optional **type** parameter, which tells the client what type of repository it is connecting to. The following type is supported.

| Type  | Description |
|--------------|-------------|
| **Microsoft.PreIndexed.Package** | The default source type. |

### list

The **list** subcommand enumerates the currently enabled sources, or provides details on a specific source.

Usage:

```cmd
winget source list [[-n, --name] <name>]
```

#### Aliases

The following aliases are available for this subcommand:

- ls

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

#### list all

The **list** subcommand by itself, `winget source list`, provides the complete list of supported sources:

```output
Name   Arg
-----------------------------------------
winget https://winget.azureedge.net/cache
```

#### list source details

To get complete details about a source, pass in the name of the source. For example:

```cmd
winget source list --name Contoso
```

Returns the following output:

```output
Name   : Contoso
Type   : Microsoft.PreIndexed.Package
Arg    : https://pkgmgr-int.azureedge.net/cache
Data   : AppInstallerSQLiteIndex-int_g4ype1skzj3jy
Updated: 2020-4-14 17:45:32.000
```

- `Name` is the name of the source.
- `Type` is the type of repo.
- `Arg` is the URL or path the source uses.
- `Data` is the optional package name, if appropriate.
- `Updated` is the last date and time the source was updated.

### update

The **update** subcommand forces an update to an individual source, or to all sources.

Usage:

```cmd
winget source update [[-n, --name] <name>]
```

#### Aliases

The following aliases are available for this subcommand:

- refresh

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

#### update all

The **update** subcommand by itself, `winget source update`, requests updates to all repos.

#### update source

The **update** subcommand with the **--name** option directs an update to the named source. For example: `winget source update --name Contoso` forces an update to the Contoso repository.

### remove

The **remove** subcommand removes a source. This subcommand requires the **--name** option to identify the source. Because the command changes user access, using **remove** requires administrator privileges.

Usage:

```cmd
winget source remove [-n, --name] <name>
```

#### Aliases

The following aliases are available for this subcommand:

- rm

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

#### Examples

```cmd
winget source remove --name Contoso
```

This command removes the Contoso repository.

### reset

The **reset** subcommand resets the client back to its original configuration, and removes all sources except the default. Only use this subcommand in rare cases. Because the command changes user access, using **reset** requires administrator privileges.

Because the **reset** command removes all sources, you must force the action by using the **--force** option.

Usage:

```cmd
winget source reset --force
```

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

### export

The **export** sub-command exports the specific details for a source to JSON output.

#### Arguments

The following arguments are available.

| Argument  | Description |
|--------------|-------------|
| **-n, --name** | The name to identify the source by. |

#### Options

The following options are available.

| Option  | Description |
|--------------|-------------|
| **-?, --help** |  Get additional help on this command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Open the default logs location. |
| **--verbose, --verbose-logs** | Used to override the logging setting and create a verbose log. |
| **--disable-interactivity** | Disable interactive prompts. |

#### Examples

```cmd
winget source export winget
```

Returns the following output:

```output
{"Arg":"https://winget.azureedge.net/cache","Data":"Microsoft.Winget.Source_8wekyb3d8bbwe","Identifier":"Microsoft.Winget.Source_8wekyb3d8bbwe","Name":"winget","Type":"Microsoft.PreIndexed.Package"}
```

## Source agreement

An individual **source** might request that the user agree to the terms presented before adding or using the repository. If a user doesn't accept or acknowledge the agreement, they won't be able to access the source.

You can use the **--accept-source-agreements** option to accept the source license agreement and avoid the prompt.

:::image type="content" source="images/source-license.png" alt-text="Screenshot showing a source license prompt.":::

## Related topics

- [Use the winget tool to install and manage applications](index.md)
