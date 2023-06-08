---
title: Uno Setup Guide
description: Learn how to reach users on other platforms like Web, iOS, Android, and Linux with minimal changes to the C#/WinUI 3 simple photo viewer built in the previous tutorial. We'll use Uno Platform to create a new multi-platform app, which we can move code from the existing desktop project to.
ms.topic: article
ms.date: 05/21/2023
keywords: Windows, App, SDK, WinUI 3, WinUI, photo, viewer, Windows 11, Windows 10, XAML, C#, uno platform, uno
ms.author: aashcraft
author: alvinashcraft
ms.localizationpriority: medium
---

## Finalize your environment

1. Open a command-line prompt, Windows Terminal if you have it installed, or else Command Prompt or Windows Powershell from the Start Menu.

2. Install the `uno-check` tool:
    - Use the following command:

        `dotnet tool install -g uno.check`

    - To update the tool, if you already have an existing one:

        `dotnet tool update -g uno.check`

3. Run the tool with the following command:

    `uno-check`

4. Follow the instructions indicated by the tool. Because it needs to modify your system, you may be prompted for elevated permissions.
