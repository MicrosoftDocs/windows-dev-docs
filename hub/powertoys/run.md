---
title: PowerToys Run utility for Windows
description: A quick launcher for power users that contains some additional features without sacrificing performance.
ms.date: 05/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer, PowerToys Run, WindowWalker]
---

# PowerToys Run utility

PowerToys Run is a quick launcher for power users that contains some additional features without sacrificing performance. It is open source and modular for additional plugins.

To use PowerToys Run, select <kbd>Alt</kbd>+<kbd>Space</kbd> and start typing! _(note that this shortcut can be changed in the settings window)_
![PowerToys Run demo opening apps](../images/pt-powerrun-demo.gif)

## Requirements

- Windows 10 version 1903 or higher
- After installing, PowerToys must be enabled and running in the background for this utility to work

## Features

PowerToys Run features include:

- Search for applications, folders, or files
- Search for running processes (previously known as [WindowWalker](https://github.com/betsegaw/windowwalker/))
- Clickable buttons with keyboard shortcuts (such as _Open as administrator_ or _Open containing folder_)
- Invoke Shell Plugin using `>` (for example, `> Shell:startup` will open the Windows startup folder)
- Do a simple calculation using calculator
- Executing system commands
- Getting time and date informations or coverting units
- Opening web pages or starting a web search

## Settings

The following general options are available for PowerToys Run in the PowerToys settings menu.

![PowerToys Run settings](../images/pt-run-settings.png)

| Settings | Description |
| :--- | :--- |
| Activation shortcut | Define the keyboard shortcut to open/hide PowerToys Run |
| Use centralized keyboard hook | You can try this setting if there are issues with the shortcut 
| Ignore shortcuts in Fullscreen mode | When in full-screen (F11), PowerToys Run won't be engaged with the shortcut |
| Maximum number of results | Maximum number of results shown without scrolling |
| Clear the previous query on launch | When launched, previous searches will not be highlighted |
| Preferred monitor position | If multiple monitors are in use, PowerToys Run can be launched on the desired monitor:<br />- Primary monitor<br />- Monitor with mouse cursor<br />- Monitor with focused window |
| App theme | Change the color theme used by PowerToys Run|


## Usage

### Keyboard shortcuts

| Shortcuts | Action |
| :--- | :--- |
|<kbd>Alt</kbd>+<kbd>Space</kbd> (default) | Open or hide PowerToys Run |
|<kbd>Esc</kbd> | Hide PowerToys Run |
|<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Enter</kbd> | Open the selected application as administrator (only applicable to applications) |
|<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>E</kbd> | Open containing folder in File Explorer (only applicable to applications and files) |
|<kbd>Ctrl</kbd>+<kbd>C</kbd> | Copy path location (only applicable to folders and files) |
|<kbd>Tab</kbd> | Navigate through the search result and context menu buttons |


### Direct activation commands

These default direct activation commands will force PowerToys Run into only targeted plugins.

| Direct activation command | Action | Example |
| :--- | :--- | :--- |
| `=` | Calculator only | `=2+2` |
| `?` | File searching only | `?road` to find `roadmap.txt` |
| `??` | Web search only | `??What is the answer to life` to search with your default browser's search engine. |
| `.` | Installed programs only | `.code` to get Visual Studio Code. See [Program parameters](#program-parameters) for options on adding parameters to a program's startup |
| `//` | URIs only | `//` to launch your default browser, or `//docs.microsoft.com` to have your default browser go to https://docs.microsoft.com. Also supports `mailto:` and `ms-settings:` |
| `<` | Running processes only | `<outlook` to find all processes that contain outlook |
| `>` | Shell command only | `>ping localhost` to do a ping query |
| `:` | Registry keys only | `:hkcu` to search for the HKEY_CURRENT_USER registry key |
| `!` | Windows services only | `!alg` to search for the Application Layer Gateway service to be started or stopped |
| `{` | Visual Studio Code previously opened workspaces, remote machines (SSH or Codespaces) and containers. This plugin is off by default. | `{powertoys` to search for workspaces that contain 'powertoys' in their paths |
| `%%` | Unit converter only | `%% 10 ft in m` to calculate the number of meters in 10 feet |
| `$` | Windows settings only | `$ Add/Remove Programs` to launch the Windows settings menu for managing installed programs. To list all settings of an area category, type `:` after the category name. `$ Device:` to view all available Device settings |
| '(' | Show only time and date results | `(time and date` show the current time and date in different formats. For example, to get the calendar week for a specific date you can type `(calendar week::04/01/2022`.


## Plugins

PowerToys Run uses a plugin system to provide different types of results.

###  Plugin manager

The PowerToys Run settings menu includes a plugin manager that allows you to enable/disable the various available plugins. By selecting and expanding the sections, you can customize the direct activation commands used by each plugin. In addition, you can select whether a plugin appears in global results, as well as set additional plugin options where available.

![PowerToys Run plugin manager](../images/pt-run-plugin-manager.png)


### System commands

PowerToys Run enables a set of system level actions that can be executed.

| Action command / Search result | Action | Note |
| :--- | :--- | :--- |
| `Shutdown` | Shuts down the computer | |
| `Restart` | Restarts the computer | |
| `Sign Out` | Signs current user out | |
| `Lock` | Locks the computer | |
| `Sleep` | Sleeps the computer | |
| `Hibernate` | Hibernates the computer | |
| `Empty Recycle Bin` | Empties the recycle bin | |
| `UEFI Firmware Settings` | Reboot computer into UEFI Firmware Settings | Only available on systems with UEFI firmware.<br />(Requires administrative permissions.) |
| `IP address` | Shows the ip addresses from the network connections of your computer. | The search query has to start with `IP` or `address`. |
| `MAC address` | Shows the mac addresses from the network adapters in your computer. | The search query has to start with `MAC` or `address`. |


### Program parameters

The PowerToys Run program plugin allows for program arguments to be added when launching an application. The program arguments must follow the expected format as defined by the program's command line interface.

> [!NOTE]
> To input valid search queries, the first element after the program name has to be one of the following phrases:
> - The symbol sequence `--`.
> - A parameter that starts with `-`.
> - A parameter that starts with `--`.
> - A parameter that starts with `/`.


For example, when launching Visual Studio Code, you can specify the folder to be opened with:

`Visual Studio Code -- C:\myFolder`

Visual Studio Code also supports a set of [command line parameters](https://code.visualstudio.com/docs/editor/command-line), which can be utilized with their corresponding arguments in PowerToys Run to, for instance, view the difference between files:

`Visual Studio Code -d C:\foo.txt C:\bar.txt` 

If the program plugin's option "Include in global result" is not selected, be sure to include the activation phrase, `.` by default, to invoke the plugin's behavior:

`.Visual Studio Code -- C:\myFolder`

### Calculator Plugin

> [!NOTE]
> The calculator plugin respects your computers number format setting. Please be aware of the different decimal delimiters in different languages when searching.

The PowerToys Run calculator plugin supports the following operations:

| Operation |  Operator Syntax |
| - | - |
| Addition |  a + b |
| Subtraction | a - b |
| Multiplication | a * b |
| Division |  a / b |
| Modulo/Remainder | a % b |
| Exponentiation | a ^ b |
| Factorial | x ! |
| Sine | sin( x ) |
| Cosine | cos( x ) |
| Tangent | tan( x ) |
| Arc Tangent | arctan( x ) |


### Time and Date values
The time and date plugin provides the current time and date or a custom one in different formats. You can enter the format or a custom time/date or both when searching.

> [!NOTE]
> The time and date plugin respects your computers date and time format settings. Please be aware of the different notations in different locals.

Examples:
- To show the time use `time` or `(time`.
- To show all available formats for a date value use `(3/27/2022`.
- To show the calendar week for a date value use `(calendar week::3/27/2022`.


### Folder search filters

In the folder plugin you can filter the results by using some special characters.

Character sequence | Result | Example
| - | - |
| `>` | Search inside the folder. | `C:\Users\tom\Documents\>`
| `*` | Search files by mask. | `C:\Users\tom\Documents\*.doc`
| `>*` | Search files inside the folder by mask.  | `C:\Users\tom\Documents\>*.doc`

### Settings plugin

The settings plugin allows you to search for windows settings. You can search the settings by their name or by their location.

To search by location you can use the following syntax:
- `$device:` to list all settings with `device` in the area name.
- `$app>area>area` to navigate through the settings like in the settings app.<br />For example, the query `control>system>admin` shows all settings under the path "System and Security > Administrative Tools" in the "Control Panel" application. 

### Kill a window process

With the Window Walker plugin you can kill the process of a window if it hangs.

> [!Note]
> There are some limitations for the "kill process" feature:
> - Killing the Explorer process is only allowed, if each folder window is running in its own process.
> - You can only kill elevated processes, if you have admin permissions (UAC).
> - Windows of UWP apps don't know their process, until they are searched in non-minimized state.

> [!WARNING]
> If you kill the process of an UWP app window, you kill all instances of the app. All windows are assigned to the same process.

#### File Explorer setting

If the File Explorer settings in Windows are not set to open each window in a seperate process, you will receive the following information when searching for open Explorer windows:

![PowerToys Run: Explorer Process Info](../images/pt-run-explorer-info.png)

You can turn off the information in the PowerToys Run plugin manager options for Window Walker, or select the information to change the File Explorer settings. After selecting the information, the "Folder options" window will open.

On the "Folder options" window, you can enabled the setting "Launch folder windows in a separate process".

![Folder Options Window](../images/pt-run-folder-options.png)

### Windows Search settings

If the search settings in Windows are not set to cover all drives, you will receive the following warning when using the Windows Search plugin:

![PowerToys Run Indexer Warning](../images/pt-run-indexer-warning.png)

You can turn off the warning in the PowerToys Run plugin manager options for Windows Search, or select the warning to expand which drives are being indexed. After selecting the warning, the Windows settings page with the "Searching Windows" options will open.

![Indexing Settings](../images/pt-run-indexing.png)

On the "Searching Windows" page, you can:

- Select "Enhanced" mode to enable indexing across all of the drives on your Windows machine.
- Specify folder paths to exclude.
- Select the "Advanced Search Indexer Settings" (near the bottom of the menu options) to set advanced index settings, add or remove search locations, index encrypted files, etc.

![Advanced Indexing Settings](../images/pt-run-indexing-advanced.png)

## Known issues

For a list of all known issues and suggestions, see the [PowerToys product repo issues on GitHub](https://github.com/microsoft/PowerToys/issues?q=is%3Aopen+is%3Aissue+label%3A%22Product-PowerToys+Run%22).

## Attribution

- [Wox](https://github.com/Wox-launcher/Wox/)
- [Beta Tadele's Window Walker](https://github.com/betsegaw/windowwalker)
