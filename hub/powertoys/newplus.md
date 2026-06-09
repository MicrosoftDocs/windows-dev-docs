---
title: PowerToys New+ - Create Custom File Templates in Windows
description: Create custom file and folder templates with PowerToys New+. Access personalized templates directly from File Explorer's context menu to boost productivity.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, New+, New, NewPlus, Win]
# Customer intent: As a Windows power user, I want to learn about the New+ feature in PowerToys so that I can create files and folders from a personalized set of templates directly from the File Explorer context menu.
---

# New+

**PowerToys New+** gives you the ability to create files and folders from a personalized set of templates, directly from the File Explorer context menu. This feature is designed to enhance your productivity by allowing you to quickly create new items without having to navigate through multiple steps or applications. It is a powerful tool for users who frequently create files and folders with similar structures or content.

## Getting started

This section provides an overview of the New+ feature, including how to enable it, how to create new items using templates, and how to customize your template collection.

### Enable New+ in PowerToys settings

To start using New+, enable New+ in the PowerToys settings.

### Create a new object using New+

To create a new item within a folder, right-click on the folder to bring up the context menu. From there, click on the "New+" option and then select the template you were looking for.

### Add or customize templates in New+

To create a new template, start by right-clicking on the folder. This will open a context menu where you can select the 'New+' option. From there, choose 'Open templates' to access the "Templates" folder. In this folder, you have the freedom to add, edit, or rename objects as per your needs. It’s important to note that the objects you add here will be displayed on the ‘New+’ menu in a sorted order, with folders always appearing first. This provides the ability to find and select your templates.

Template objects in the "Templates" folder can be files, folders, or shortcuts. The templates can be of any type, such as text files, Word documents or templates, Excel spreadsheets or templates, or even shortcuts to applications.

## Settings

### <a name="template_location"></a>Templates

#### Templates location

The default template location is in the local app data folder (`%localappdata%\Microsoft\PowerToys\NewPlus\Templates`) for your user account. However, these templates don't roam across devices with your account. If you want a common set of templates across devices, you can change the template location to a folder that's synced with a cloud file management service, such as OneDrive. This allows you to access your templates from any device.

### Display options

#### Hide template filename extensions

The option allows you to toggle the display of filename extensions. When this option is toggled off, a file named "filename.ext" will be displayed with its extension, appearing as "filename.ext". However, when this option is toggled on (the default), the template will be displayed without its extension, appearing simply as "filename".

#### Hide template filename starting digits, spaces and dots

The option gives you the ability to toggle the display of starting digits, spaces and dots. When this option is toggled off (the default), a file named "1. filename" will be displayed as is. However, when this option is toggled on, the template will be displayed as "filename". This is useful when using digits, spaces, and dots at the beginning of filenames to control the display order of templates.

#### Hide the built-in "New" context menu

When enabled, this option hides the built-in Windows "New" context menu and uses only New+ templates instead. This is useful if you want to rely exclusively on your New+ templates for creating new files and folders. Disabled by default.

### Behavior

#### Replace variables in template filename

This setting causes supported variables in filenames, including in files within subfolders, to be replaced when the template is copied. The default setting of this option is disabled.

**Note:** Any invalid filename characters are replaced with spaces.

##### Examples

| Example template filename | Result |
| :---             | :--- |
|`$YYYY-$MM-$DD, $hh $mm $ss  - $PARENT_FOLDER_NAME by %USERNAME%` | `2024-11-22, 12 08 54 - PowerShell project by cgaarden` |
|`File where variable value contains invalid characters %USERPROFILE%` | `File where variable value contains invalid characters C  Users cgaarden` |

##### Date and time related variables

These date and time related variable patterns are the same as those used by PowerRename and are case-sensitive.

| Variable | Explanation |
| :---             | :--- |
| `$YYYY`          | Year, represented by a full four or five digits, depending on the calendar used. |
| `$YY`            | Year, represented only by the last two digits. A leading zero is added for single-digit years. |
| `$Y`             | Year, represented only by the last digit. |
| `$MMMM`          | Name of the month. |
| `$MMM`           | Abbreviated name of the month. |
| `$MM`            | Month, as digits with leading zeros for single-digit months. |
| `$M`             | Month, as digits without leading zeros for single-digit months. |
| `$DDDD`          | Name of the day of the week. |
| `$DDD`           | Abbreviated name of the day of the week. |
| `$DD`            | Day of the month, as digits with leading zeros for single-digit days. |
| `$D`             | Day of the month, as digits without leading zeros for single-digit days. |
| `$hh`            | Hours, with leading zeros for single-digit hours. |
| `$h`             | Hours, without leading zeros for single-digit hours. |
| `$mm`            | Minutes, with leading zeros for single-digit minutes. |
| `$m`             | Minutes, without leading zeros for single-digit minutes. |
| `$ss`            | Seconds, with leading zeros for single-digit seconds. |
| `$s`             | Seconds, without leading zeros for single-digit seconds. |
| `$fff`           | Milliseconds, represented by full three digits. |
| `$ff`            | Milliseconds, represented only by the first two digits. |
| `$f`             | Milliseconds, represented only by the first digit. |

##### Special variables

These special variables are case-sensitive, so they will only work when used in the filename exactly as shown here.

| Variable | Explanation |
| :---             | :--- |
| `$PARENT_FOLDER_NAME`          | Expands to the name of the parent folder. This only works in template subfolders. |

##### Environment variables

These variables are case-insensitive, meaning you use them in the filename in a mix of uppercase or lowercase.

Each `%environment_variable%` in the file and folder names will be replaced with the value of the corresponding environment variable.

For instance, %USERNAME% will be replaced with the name of the current Windows user.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
