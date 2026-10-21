---
title: PowerToys Peek Utility - Preview Files Without Opening Apps
description: Preview files instantly with PowerToys Peek utility for Windows. View images, documents, videos, and code without opening multiple apps. Download PowerToys today.
ms.date: 10/14/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Peek, Win]
# customer intent: As a Windows power user, I want to learn how to configure and use the Peek utility in PowerToys.
---

# Peek utility

PowerToys Peek utility is a system-wide tool for Windows that lets you preview file content without opening multiple applications or interrupting your workflow. This utility offers a seamless and quick file preview experience for various file types, including images, Office documents, videos, web pages, Markdown files, text files, SQLite databases, and developer files. For developer workflows, Peek's source code preview includes project and plugin manifest files such as Unreal Engine `.uproject` and `.uplugin` files. Peek also displays summary information about folders, helping you work more efficiently.

:::image type="content" source="images/peek/peek.png" alt-text="Screenshot of PowerToys Peek utility displaying a file preview window with navigation options and file information overlay.":::

## Preview a file

Select a file in File Explorer and open the Peek preview using the activation / deactivation shortcut (default: <kbd>Space</kbd>).

Using <kbd>Left</kbd> and <kbd>Right</kbd> or <kbd>Up</kbd> and <kbd>Down</kbd>, you can scroll between all files in the current folder. Select multiple files in File Explorer for previewing to scroll only between selected ones.

### Interact with previews by using the keyboard

In previews that you can interact with, such as PDF, Markdown, text, HTML, and archive previews, the arrow keys scroll the content and move through text instead of switching files. To go to the previous or next file from anywhere in the Peek window, including inside these previews, press <kbd>Alt</kbd>+<kbd>Left</kbd> or <kbd>Alt</kbd>+<kbd>Right</kbd>.

When Peek opens, none of the title bar buttons have keyboard focus, so pressing <kbd>Space</kbd> again doesn't accidentally pin the window or open the file. Press <kbd>Tab</kbd> to move to the title bar buttons. To close Peek, press <kbd>Esc</kbd> or <kbd>Ctrl</kbd>+<kbd>W</kbd>.

## Preview SQLite databases

Peek shows a read-only preview of SQLite database files with the `.db`, `.sqlite`, and `.sqlite3` extensions. The preview lists the database tables on the left. Select a table to see its columns and rows. Peek shows up to 200 rows per table, and the record count shows how many of the table's rows are displayed. Binary (BLOB) values appear as their size in bytes.

If a `.db` file isn't a valid SQLite database, Peek shows the file's details instead.

## Preview folders

When you preview a folder, Peek shows its type, size, and date modified, along with how many files and subfolders it contains. Peek counts the folder contents in the background and updates the size and counts as it scans. A progress indicator appears while the scan is in progress.

Peek skips subfolders that it can't access. If Peek can't read part of the folder, it shows a warning icon and marks the counts as *incomplete* instead of showing an error.

## Preview other text files

If a file has no extension, or an extension that Peek doesn't recognize, Peek checks whether the file contains text. If it does, Peek shows the file as plain text in the source code preview. Otherwise, Peek shows the file's details. Files larger than the **Maximum file size to preview** setting show the file's details instead of the content.

## Pin preview window position and size

The Peek window adjusts its size based on the dimensions of the images being previewed. However, if you prefer to keep the window's size and position, you can use the pinning feature.

By selecting the pinning button, the window will preserve the current size and position. Selecting the pinning button again will unpin the window. When unpinned, the Peek window will return to the default position and size when previewing the next file.

## Open file with the default program

Select **Open with** or <kbd>Enter</kbd> to open the current file with the default program.

## See extra information about the current file

Hover over the preview to see extra information about the file, including its size, type, and when it was last modified. You can hide this tooltip from Peek's **Behavior** settings if you'd rather not see metadata on hover.

## Delete files

Press the <kbd>Delete</kbd> key to move the current file to the Recycle Bin.

By default, a confirmation dialog will appear before deletions. To skip future confirmations, either:

- Check the "Don't show this warning again" checkbox in the dialog.
- Uncheck the "Ask for confirmation before deleting files" option in Peek's settings page.

:::image type="content" source="images/peek/delete-confirmation.png" alt-text="Screenshot of PowerToys Peek file deletion confirmation dialog with checkbox option to skip future warnings.":::

After deleting the file, Peek will automatically preview the next file. If there are no more files to preview, a message will be displayed.

> [!TIP]
> Only files may be deleted. Folders may not be deleted, even if they are empty.

## Use Peek from the command line

Peek can also be launched from the command line to preview files and folders. Run `PowerToys.Peek.UI.exe` followed by one or more file or folder paths, separated by spaces. You can use full or relative paths. For example, `PowerToys.Peek.UI.exe .` previews the current folder.

```powershell
PowerToys.Peek.UI.exe "C:\Users\Docs\report.pdf"
PowerToys.Peek.UI.exe .\photo.png .\notes.txt .\Screenshots
```

When you pass more than one path, use <kbd>Left</kbd> and <kbd>Right</kbd> (or <kbd>Alt</kbd>+<kbd>Left</kbd> and <kbd>Alt</kbd>+<kbd>Right</kbd>) to move between them. The title bar shows the position of the current item and the total number of items. Peek skips paths that don't exist. If none of the paths exist, Peek doesn't open and returns exit code `2`.

> [!TIP]
> The per-user executable is located in `%LOCALAPPDATA%\PowerToys\WinUI3Apps` by default, while the machine-wide executable is located in `C:\Program Files\PowerToys\WinUI3Apps`.

## Settings

From the settings page, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to open Peek for the selected file(s). New starting in PowerToys 0.95 - <kbd>Space</kbd> is now the default keyboard command. |
| Always run without elevation, even when PowerToys is elevated | Tries to run Peek without elevated permissions, to fix access to network shares. |
| Peek window is always on top | Keeps the Peek window above other windows while it is open. Default: Off. |
| Show Peek icon on the taskbar | Shows the Peek window on the taskbar and in window switchers while it is open. Default: On. |
| Automatically close the Peek window after it loses focus |  |
| Show file preview tooltip | When enabled, hovering the preview shows a tooltip with filename, type, date modified, and size. Turn this off to disable the on-hover tooltip. |
| Confirm before deleting files | When enabled, Peek shows a confirmation dialog before deleting files. |

The following settings are available for source code files but are still in preview:

| Setting | Description |
| :--- | :--- |
| **Wrap text** | When enabled, this option wraps long lines of code in the preview window. |
| **Try to format the source for preview** | When enabled, this option attempts to format the source code for better readability in the preview window. |
| **Font size** | Adjust the font size used in the preview window. |
| **Enable sticky scroll** | When enabled, this option keeps the code preview in sync with the editor's scroll position. |
| **Show minimap** | When enabled, this option displays a minimap of the code in the preview window. |
| **Maximum file size to preview** | The maximum size, in kilobytes, of files that Peek displays in the source code preview, from 1 to 102,400. Larger files show the file details instead. This limit prevents Peek from loading very large files into memory. Default: 10,240. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
