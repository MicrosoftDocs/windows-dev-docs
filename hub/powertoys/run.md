---
title: PowerToys Run utility for Windows
description: A quick launcher for power users that contains some additional features without sacrificing performance.
ms.date: 05/29/2022
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, File Explorer, PowerToys Run, Window Walker]
---

# PowerToys Run utility

PowerToys Run is a quick launcher for power users that contains some additional features without sacrificing performance. It is open source and modular for additional plugins.

To use PowerToys Run, select <kbd>Alt</kbd>+<kbd>Space</kbd> and start typing! _(Note that this shortcut can be changed in the settings window.)_

> [!IMPORTANT]
> PowerToys must be running in the background and Run must be enabled for this utility to work.

![PowerToys Run demo opening apps.](../images/pt-powerrun-demo.gif)


## Features

PowerToys Run features include:

- Search for applications, folders or files
- Search for running processes (previously known as [Window Walker](https://github.com/betsegaw/windowwalker/))
- Clickable buttons with keyboard shortcuts (such as _Open as administrator_ or _Open containing folder_)
- Invoke Shell Plugin using `>` (for example, `> Shell:startup` will open the Windows startup folder)
- Do a simple calculation using calculator
- Execute system commands
- Get time and date information
- Convert units
- Open web pages or start a web search


## Settings

The following general options are available on the PowerToys Run settings page.

| Setting | Description |
| :--- | :--- |
| Activation shortcut | Define the keyboard shortcut to show/hide PowerToys Run |
| Use centralized keyboard hook | Try this setting if there are issues with the keyboard shortcut |
| Ignore shortcuts in full-screen mode | When in full-screen (F11), PowerToys Run won't be engaged with the shortcut |
| Maximum number of results | Maximum number of results shown without scrolling |
| Clear the previous query on launch | When launched, previous searches will not be highlighted |
| Preferred display position | If multiple displays are in use, PowerToys Run can be launched on:<br />- Primary display<br />- Display with mouse cursor<br />- Display with focused window |
| App theme | Change the color theme used by PowerToys Run |


### Plugin manager

PowerToys Run uses a plugin system to provide different types of results. The settings page includes a plugin manager that allows you to enable/disable the various available plugins. By selecting and expanding the sections, you can customize the direct activation commands used by each plugin. In addition, you can select whether a plugin appears in global results and set additional plugin options where available.

![PowerToys Run Plugin Manager.](../images/pt-run-plugin-manager.png)

#### Direct activation commands

The plugins can be activated with a direct activation command so that PowerToys Run will only use the targeted plugin. The following table shows the direct activation commands assigned by default.

> [!TIP]
> You can change them to fit your personal needs in the [plugin manager](#plugin-manager).

| Plug-in                   | Direct activation command | Example                                                                                                                                                                                                                                                                                              |
|:--------------------------|:--------------------------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Calculator                | `=`                       | `= 2+2`                                                                                                                                                                                                                                                                                              |
| File searching            | `?`                       | `? road` to find 'roadmap.txt'                                                                                                                                                                                                                                                                       |
| Installed programs        | `.`                       | `. code` to get Visual Studio Code. (See [Program parameters](#program-parameters) for options on adding parameters to a program's startup.)                                                                                                                                                         |
| Registry keys             | `:`                       | `: hkcu` to search for the 'HKEY_CURRENT_USER' registry key.                                                                                                                                                                                                                                         |
| Windows services          | `!`                       | `! alg` to search for the 'Application Layer Gateway' service to be started or stopped.                                                                                                                                                                                                              |
| Shell command             | `>`                       | `> ping localhost` to do a ping query.                                                                                                                                                                                                                                                               |
| Time and date             | `(`                       | `( time and date` shows the current time and date in different formats.<br />`( calendar week::04/01/2022` shows the calendar week for the date '04/01/2022'.                                                                                                                                        |
| Time zones                | `&`                       | `& Newfoundland` shows the current time in the time zone of Newfoundland.                                                                                                                                                                                                                            |
| Unit converter            | `%%`                      | `%% 10 ft in m` to calculate the number of meters in 10 feet.                                                                                                                                                                                                                                        |
| URI-handler               | `//`                      | `//` to launch your default browser.<br />`// docs.microsoft.com` to have your default browser go to https://docs.microsoft.com.<br />`mailto:` and `ms-settings:` links are supported.                                                                                                              |
| Visual Studio Code        | `{`                       | `{ powertoys` to search for previously opened workspaces, remote machines and containers that contain 'powertoys' in their paths.                                                                                                                                                                    |
| Web search                | `??`                      | `??` to launch your default browser's search page.<br />`?? What is the answer to life` to search with your default browser's search engine.                                                                                                                                                         |
| Windows settings          | `$`                       | `$ Add/Remove Programs` to launch the Windows settings page for managing installed programs.<br />`$ Device:` to list all settings with 'device' in their area/category name.<br />`$ control>system>admin` shows all settings of the path 'Control Panel > System and Security > Administrative Tools'. |
| Windows Terminal profiles | `_`                       | `_ powershell` to list all profiles that contains 'powershell' in their name.                                                                                                                                                                                                                        |
| Window Walker             | `<`                       | `< outlook` to find all open windows that contain 'outlook' in their name or the name of their process.                                                                                                                                                                                              |


## Using PowerToys Run

### General keyboard shortcuts

| Shortcut | Action |
| :--- | :--- |
| <kbd>Alt</kbd>+<kbd>Space</kbd> (default) | Show or hide PowerToys Run |
| <kbd>Esc</kbd> | Hide PowerToys Run |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Enter</kbd> | Open the selected application as administrator (only applicable to applications) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>U</kbd> | Open the selected application as different user (only applicable to applications) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>E</kbd> | Open containing folder in File Explorer (only applicable to applications and files) |
| <kbd>Ctrl</kbd>+<kbd>C</kbd> | Copy path location (only applicable to folders and files) |
| <kbd>Tab</kbd> | Navigate through the search results and context menu buttons |

### System commands

The Windows System Commands plugin provides a set of system level actions that can be executed.

> [!TIP]
> If your system language is supported by PowerToys, the system commands will be localized. If you prefer English commands, you can disable the setting **Use localized system commands instead of English ones** in the [plugin manager](#plugin-manager).

| Command | Action | Note |
| :--- | :--- | :--- |
| `Shutdown` | Shuts down the computer | |
| `Restart` | Restarts the computer | |
| `Sign Out` | Signs current user out | |
| `Lock` | Locks the computer | |
| `Sleep` | Puts the computer to sleep | |
| `Hibernate` | Hibernates the computer | |
| `Empty Recycle Bin` | Empties the recycle bin | |
| `UEFI Firmware Settings` | Reboots the computer into UEFI Firmware Settings | Only available on systems with UEFI firmware. Requires administrative permissions. |
| `IP address` * | Shows the ip addresses from the network connections of your computer. | The search query has to start with the word `IP` or the word `address`. |
| `MAC address` * | Shows the mac addresses from the network adapters in your computer. | The search query has to start with the word `MAC` or the word `address`. |

_*) This command may take some time to provide the results._

### Program parameters

The Program plugin allows for program arguments to be added when launching an application. The program arguments must follow the expected format as defined by the program's command line interface.

> [!NOTE]
> To input valid search queries, the first element after the program name has to be one of the following possibilities:
> - The characters sequence `--`.
> - A parameter that starts with `-`.
> - A parameter that starts with `--`.
> - A parameter that starts with `/`.

For example, when launching Visual Studio Code, you can specify the folder to be opened with:

`Visual Studio Code -- C:\myFolder`

Visual Studio Code also supports a set of [command line parameters](https://code.visualstudio.com/docs/editor/command-line), which can be utilized with their corresponding arguments in PowerToys Run to, for instance, view the difference between files:

`Visual Studio Code -d C:\foo.txt C:\bar.txt`

If the program plugin's option "Include in global result" is not selected, include the activation phrase, `.` by default, to invoke the plugin's behavior:

`.Visual Studio Code -- C:\myFolder`

### Calculator plugin

> [!TIP]
> The Calculator plugin respects the number format settings of your system. If you prefer the English (United States) number format, you can change the behavior for the query input and the result output in the [plugin manager](#plugin-manager).

> [!IMPORTANT]
> Please be aware of the different decimal and thousand delimiters in different locals.
> If your system's number format uses the comma (`,`) as the decimal delimiter, you have to write a space between the number(s) and comma(s) on operations with multiple parameters. The input has to look like this: `min( 1,2 , 3 , 5,7)` or `min( 1.2 , 3 , 5.7)`.

The Calculator plugin supports the following operations:

| Operation | Operator Syntax | Description |
| :--- | :--- | :--- |
| Addition | a + b | |
| Subtraction | a - b | |
| Multiplication | a * b | |
| Division | a / b | |
| Modulo/Remainder | a % b | |
| Exponentiation | a ^ b | |
| Ceiling function | ceil( x.y ) | Rounds a number up to the next larger integer. |
| Floor function | floor( x.y ) | Rounds a number down to the next smaller integer. |
| Rounding | round( x.abcd ) | Rounds to the nearest integer. |
| Exponential function | exp( x ) | Returns e raised to the specified power. |
| Maximum | max( x, y, z ) | |
| Minimum | min( x, y, z ) | |
| Absolute | abs( -x ) | Absolute value of a number. |
| Logarithm base 10 | log( x ) | |
| Logarithm base e | ln( x ) | |
| Square root | sqrt( x ) | |
| Power of x | pow( x, y ) | Calculate a number (x) raised to the power of some other number (y). |
| Factorial | x! | |
| Sign | sign( -x ) | A number that indicates the sign of value:<br />- `-1` if number is less than zero.<br />- `0` if number is zero.<br />- `1` if number is greater than zero. |
| Random number | rand() | Returns a fractional number between 0 and 1. |
| Pi | pi | Returns the number of pi. |
| Sine | sin( x ) | |
| Cosine | cos( x ) | |
| Tangent | tan( x ) | |
| Arc Sine | arcsin( x ) | |
| Arc Cosine | arccos( x ) | |
| Arc Tangent | arctan( x ) | |
| Hyperbolic Sine | sinh( x ) | |
| Hyperbolic Cosine | cosh( x ) | |
| Hyperbolic Tangent | tanh( x ) | |
| Hyperbolic Arc Sine | arsinh( x ) | |
| Hyperbolic Arc Cosine | arcosh( x ) | |
| Hyperbolic Arc Tangent | artanh( x ) | |

### Time and date plugin

The Time and date plugin provides the current time and date or a custom one in different formats. You can enter the format or a custom time/date or both when searching.

> [!NOTE]
> The Time and Date plugin respects the date and time format settings of your system. Please be aware of the different notations in different locals.

> [!IMPORTANT]
> For global queries the first word of the query has to be a complete match.

Examples:

- `time` or `( time` to show the time.
- `( 3/27/2022` to show all available formats for a date value.
- `( calendar week::3/27/2022` to show the calendar week for a date value.
- `( unix epoch::3/27/2022 10:30:45 AM` to convert the given time and date value into a Unix epoch timestamp.

### Unit converter plugin

> [!NOTE]
> The Unit Converter plugin respects the number format settings of your system. Please be aware of the different decimal and thousand delimiters in different locals. The names and abbreviations of the units aren't localized yet.

The Unit Converter plugin supports the following unit types:

- Acceleration
- Angle
- Area
- Duration
- Energy
- Information technology
- Length
- Mass
- Power
- Pressure
- Speed
- Temperature
- Volume

### Folder search filters

In the Folder plugin you can filter the results by using some special characters.

| Character sequence | Result | Example |
| :--- | :--- | :--- |
| `>` | Search inside the folder. | `C:\Users\tom\Documents\>` |
| `*` | Search files by mask. | `C:\Users\tom\Documents\*.doc` |
| `>*` | Search files inside the folder by mask. | `C:\Users\tom\Documents\>*.doc` |

### Windows Settings plugin

The Windows Settings plugin allows you to search for Windows settings. You can search the settings by their name or by their location.

To search by location you can use the following syntax:

- `$ device:` to list all settings with 'device' in the area name.
- `$ control>system>admin` shows all settings of the path 'Control Panel > System and Security > Administrative Tools'.

### Kill a window process

With the Window Walker plugin you can kill the process of a window if it hangs.

> [!NOTE]
> There are some limitations for the "kill process" feature:
>
> - Killing the Explorer process is only allowed if each folder window is running in its own process.
> - You can only kill elevated processes if you have admin permissions (UAC).
> - Windows of UWP apps don't know their process until they are searched in non-minimized state.

> [!WARNING]
> If you kill the process of an UWP app window, you kill all instances of the app. All windows are assigned to the same process.

#### File Explorer setting

If the File Explorer settings in Windows are not set to open each window in a separate process, you will receive the following message when searching for open Explorer windows:

![Explorer Process Info in PowerToys Run.](../images/pt-run-explorer-info.png)

You can turn off the message in the PowerToys Run plugin manager options for Window Walker, or select the message to change the File Explorer settings. After selecting the message, the "Folder options" window will open.

On the "Folder options" window, you can enable the setting "Launch folder windows in a separate process".

![Folder Options Window.](../images/pt-run-folder-options.png)

### Windows Search settings

If the indexing settings for Windows Search are not set to cover all drives, you will receive the following warning when using the Windows Search plugin:

![PowerToys Run Indexer Warning.](../images/pt-run-indexer-warning.png)

You can turn off the warning in the PowerToys Run plugin manager options for Windows Search, or select the warning to expand which drives are being indexed. After selecting the warning, the Windows settings page with the "Searching Windows" options will open.

![Indexing Settings.](../images/pt-run-indexing.png)

On the "Searching Windows" page, you can:

- Select "Enhanced" mode to enable indexing across all of the drives on your Windows machine.
- Specify folder paths to exclude.
- Select the "Advanced Search Indexer Settings" (near the bottom of the menu options) to set advanced index settings, add or remove search locations, index encrypted files, etc.

![Advanced Indexing Settings.](../images/pt-run-indexing-advanced.png)


## Known issues

For a list of all known issues and suggestions, see the [PowerToys product repository issues on GitHub](https://github.com/microsoft/PowerToys/issues?q=is%3Aopen+is%3Aissue+label%3A%22Product-PowerToys+Run%22).


## Attribution

- [Wox](https://github.com/Wox-launcher/Wox/)
- [Beta Tadele's Window Walker](https://github.com/betsegaw/windowwalker)
