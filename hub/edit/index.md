---
title: Edit command line text editor
description: Learn how to use Edit, a command-line text editor, in Windows.
ms.topic: article
ms.date: 06/02/2026
---

# Edit command line tool

**Edit** is a lightweight, open-source command-line text editor for Windows 11. It pays homage to the classic [MS-DOS Editor](https://en.wikipedia.org/wiki/MS-DOS_Editor), but with a modern interface and input controls similar to VS Code.

## Availability

Edit is included in Windows 11 as part of the September 2025 optional update (or newer) and in the Windows 11 2025 Update (version 25H2). To check whether Edit is already available on your system, open Command Prompt, PowerShell, or Windows Terminal and enter: `edit`.

If Edit is not yet available on your version of Windows, you can install it using [winget](../package-manager/winget/index.md):

```powershell
winget install Microsoft.Edit
```

You can also [download the latest release from GitHub](https://github.com/microsoft/edit/releases/latest).

Edit is also available on **Linux** and **macOS**. See the [Edit GitHub repository](https://github.com/microsoft/edit) for installation instructions on those platforms.

## How to use Edit

To open Edit, enter `edit` in the command line, or run `edit <your-file-name>` to open a specific file. You can edit files directly in the terminal without context switching.

![Screenshot of the Edit text editor](../images/edit.png)

## Features

Edit has several features out of the box.

- **Modeless editing**: Edit uses a modeless interface with a Text User Interface (TUI), so there are no modes to learn. All menu options also have preconfigured keybindings.

- **Multiple files**: Open and switch between multiple files with <kbd>Ctrl+P</kbd>.

- **Find & Replace**: Find and replace text with <kbd>Ctrl+F</kbd> or select Edit > Find in the TUI menu. Match Case, Whole Word, and Regular Expression options are supported.

- **Word Wrap**: Toggle word wrap with <kbd>Alt+Z</kbd> or select View > Word Wrap in the TUI menu.

- **Mouse support**: Use the mouse to navigate menus, select text, and scroll.

## FAQ

### Why build another command-line text editor?

64-bit versions of Windows did not include a default CLI text editor after the classic 16-bit MS-DOS Editor was retired. Edit fills that gap with a modeless editor that provides a low barrier of entry for new users.

Because many existing modeless editors have no first-party support for Windows or are too large to bundle with the OS, Edit was built from scratch in Rust.

## Edit open source repository

Edit is open source under the MIT license and welcomes your contributions and feedback. You can find the source code on [GitHub](https://github.com/microsoft/edit).
