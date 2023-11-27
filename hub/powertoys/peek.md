---
title: PowerToys Peek utility for Windows
description: A system-wide utility for Windows that allows you to quickly preview file content.
ms.date: 08/03/2023
ms.topic: article
no-loc: [PowerToys, Windows, Peek, Win]
---

# Peek utility

A system-wide utility for Windows that allows you to preview file content without the need to open multiple applications or interrupt your workflow. It offers a seamless and quick file preview experience for various file types, including images, Office documents, web pages, Markdown files, text files, and developer files.

![Screenshot of PowerToys Peek utility.](../images/powertoys-peek.png)

## Preview a file

Select a file in the File Explorer and open the Peek preview using the activation / deactivation shortcut (default: <kbd>Ctrl</kbd>+<kbd>Space</kbd>).

Using <kbd>Left</kbd> and <kbd>Right</kbd> or <kbd>Up</kbd> and <kbd>Down</kbd>, you can scroll between all files in the current folder. Select multiple files in the File Explorer for previewing to scroll only between selected ones.

## Pin preview window position and size

The Peek window adjusts its size based on the dimensions of the images being previewed. However, if you prefer to keep the window's size and position, you can utilize the pinning feature.

By selecting the pinning button located in the top right corner of the Peek window, the window will preserve the current size and position.

Selecting the pinning button again will unpin the window. When unpinned, the Peek window will return to the default position and size when previewing the next file.

## Open file with the default program

Select **Open with** on the top or <kbd>Enter</kbd> to open the current file with the default program.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to open Peek for the selected file(s). |
| Always run not elevated, even when PowerToys is elevated | Tries to run Peek without elevated permissions, to fix access to network shares. |
| Automatically close the Peek window after it loses focus |  |
