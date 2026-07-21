---
title: '`dscv3` Command'
description: DSC v3 Command based resource.
ms.date: 07/19/2026
ms.topic: overview
no-loc: [winget, dsc, --disable-interactivity, --help, --ignore-warnings, --logs, --manifest, --no-proxy, --nowarn, --open-logs, --output, --proxy, --verbose, --verbose-logs, --wait]
---

# dscv3 command (winget)

The **`dscv3`** command of [`winget`](index.md) represents [Microsoft Desired State Configuration version 3](/powershell/dsc/overview?view=dsc-3.0&preserve-view=true) (DSC v3) command-based resource implementation.


## Usage

```cmd
winget dscv3 [<command>] [<options>]
```

:::image type="content" source="./images/dscv3.png" alt-text="Screenshot listing winget dsv3 command options." lightbox="./images/dscv3.png":::

## sub-commands

The following sub-commands are available.

|         Argument         |           Description           |
|--------------------------|---------------------------------|
| **`package`**            | Manages package state.          |
| **`source`**             | Manages source configuration.   |
| **`user-settings-file`** | Manages user settings file.     |
| **`admin-settings`**     | Manages administrator settings. |

## Options

The options allow you to customize the install experience to meet your needs.

|                 Option                  |                              Description                              |
|-----------------------------------------|-----------------------------------------------------------------------|
| **`-m`, `--manifest`**                  | Displays the resource JSON manifest.                                  |
| **`-o`, `--output`**                    | Specifies the directory where the resource JSON file will be written. |
| **`-?`, `--help`**                      | Get additional help on this command.                                  |
| **`--wait`**                            | Prompts the user to press any key before exiting.                     |
| **`--logs`, `--open-logs`**             | Open the default logs location.                                       |
| **`--verbose`, `--verbose-logs`**       | Used to override the logging setting and create a verbose log.        |
| **`--nowarn`**, **`--ignore-warnings`** | Suppresses warning outputs.                                           |
| **`--disable-interactivity`**           | Disable interactive prompts.                                          |
| **`--proxy`**                           | Set a proxy to use for this execution.                                |
| **`--no-proxy`**                        | Disable the use of proxy for this execution.                          |

## Related topics

* [WinGet Configuration](../configuration/index.md)
