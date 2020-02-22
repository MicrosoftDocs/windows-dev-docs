---
title: Windows Terminal Command Line Arguments
description: Learn how to create command line arguments for Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Create Command Line Arguments for Windows Terminal

Instructions on how to create Command Line Arguments for Windows Terminal.

## What are command line arguments?

Command line arguments enable you to launch Windows Terminal with new tabs and panes split to your specification, using profiles that you setup and choose, and opening in the directories that you specify. 

To create a command line argument, you must use the `wt` execution alias.

Here are some examples:

`wt -d .`
Opens the Terminal with the default profile in the current working directory.

`wt -d . ; new-tab -d C:\ pwsh.exe`
Opens the Terminal with two tabs. The first is running the default profile starting in the current working directory. The second is using the default profile with pwsh.exe as the "commandline" (instead of the default profile’s "commandline") starting in the C:\ directory.

`wt -p "Windows PowerShell" -d . ; split-pane -V wsl.exe`
Opens the Terminal with two panes, split vertically. The top pane is running the profile with the name “Windows Terminal” and the bottom pane is running the default profile using wsl.exe as the "commandline" (instead of the default profile’s "commandline").

`wt -d C:\Users\cinnamon\GitHub\WindowsTerminal ; split-pane -p "Command Prompt" ; split-pane -p "Ubuntu" -d \\wsl$\Ubuntu\home\cinnak -H`

The image below illustrates this command line argument opening the terminal with multiple split panes.

![Windows Terminal Command Line Argument for Split Panes](./images/terminal-command-args.gif)