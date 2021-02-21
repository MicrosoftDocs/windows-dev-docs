---
title: PowerToys Run utility for Windows 10
description: A quick launcher for power users that contains some additional features without sacrificing performance.
ms.date: 12/02/2020
ms.topic: article
ms.localizationpriority: medium
---

# PowerToys Run utility

PowerToys Run is a quick launcher for power users that contains some additional features without sacrificing performance. It is open source and modular for additional plugins.

To use PowerToys Run, select <kbd>Alt</kbd>+<kbd>Space</kbd> and start typing!

*If that shortcut isn't what you like, don't worry, it is fully configurable in the settings.*

![PowerToys Run demo opening apps](../images/pt-powerrun-demo.gif)

## Requirements

- Windows 10 version 1903 or higher
- After installing, PowerToys must be enabled and running in the background for this utility to work

## Features

PowerToys Run features include:

- Search for applications, folders, or files

- Search for running processes (previously known as [WindowWalker](https://github.com/betsegaw/windowwalker/))

- Clickable buttons with keyboard shortcuts (such as *Open as administrator* or *Open containing folder*)

- Invoke Shell Plugin using `>`  (for example, `> Shell:startup` will open the Windows startup folder)

- Do a simple calculation using calculator

## Settings

The following Run options are available in the PowerToys settings menu.

  | **Settings** |**Action** |
  | --- | --- |
  | Open PowerToys Run | Define the keyboard shortcut to open/hide PowerToys Run |
  | Ignore shortcuts in Fullscreen mode |  When in full-screen (F11), Run won't be engaged with the shortcut |
  | Maximum number of results |  Maximum number of results shown without scrolling |
  | Clear the previous query on launch | When launched, previous searches will not be highlighted |
  | Disable drive detection warning | The warning, if all of your drives aren't indexed, will no longer be visible |

## Keyboard shortcuts

  | **Shortcuts** | **Action** |
  | --- | --- |
  | Alt+Space | Open or hide PowerToys Run |
  | Esc | Hide PowerToys Run |
  | Ctrl+Shift+Enter | (Only applicable to applications) Open the selected application as administrator |
  | Ctrl+Shift+E | (Only applicable to applications and files) Open containing folder in File Explorer |
  | Ctrl+C | (Only applicable to folders and files) Copy path location |
  | Tab | Navigate through the search result and context menu buttons |

## Action key

These will force PowerToys run into only targeted plug-ins.

  | **Action key** | **Action** |
  | --- | --- |
  | `=` | Calculator only. Example `=2+2` |
  | `?` | File searching only. Example `?road` to find `roadmap.txt` |
  | `.` | Installed app searching only. Example `.code` to get Visual Studio Code |
  | `//` | URLs only. Example `//docs.microsoft.com` to have your default browser go to https://docs.microsoft.com |
  | `<` | Running processes only. Example `<outlook` to find all processes that contain outlook |
  | `>` | Shell command only. Example `>ping localhost` to do a ping query |
  | `:` | Registry keys only. Example `:hkcu` to search for the HKEY_CURRENT_USER registry key |
  | `!` | Windows services only. Example `!alg` to search for the Application Layer Gateway service to be started or stopped |

## System commands

With PowerToys v0.31 and on, there are system level actions you can now execute.

  | **Action key**   |   **Action** |
  | ------------------ | ---------------------------------------------------------------------------------|
  | `Shutdown` | Shuts down the computer |
  | `Restart` | Restarts the computer |
  | `Sign Out` | Signs current user out |
  | `Lock` | Locks the computer |
  | `Sleep` | Sleeps the computer |
  | `Hibernate` | Hibernates the computer |
  | `Empty Recycle Bin` | Empties the recycle bin |

## Indexer settings

If indexer settings are not set to cover all drives, you will receive the following warning:

![PowerToys Run Indexer Warning](../images/pt-run-warning.png)

You can turn off the warning in the PowerToys settings or select the warning to expand which drives are being indexed. After selecting the warning, the Windows 10 settings "Searching Windows" options will open.

![Indexing Settings](../images/pt-run-indexing.png)

In this "Searching Windows" menu, you can:

- Select "Enhanced" mode to enable indexing across all of the drives on your Windows 10 machine.
- Specify folder paths to exclude.
- Select the "Advanced Search Indexer Settings" (near the bottom of the menu options) to set advanced index settings, add or remove search locations, index encrypted files, etc.

![Advanced Indexing Settings](../images/pt-run-indexing-advanced.png)

## Known issues

For a list of all known issues and suggestions, see the [PowerToys product repo issues on GitHub](https://github.com/microsoft/PowerToys/issues?q=is%3Aopen+is%3Aissue+label%3AProduct-Launcher).

## Attribution

- [Wox](https://github.com/Wox-launcher/Wox/)

- [Beta Tadele's Window Walker](https://github.com/betsegaw/windowwalker)
