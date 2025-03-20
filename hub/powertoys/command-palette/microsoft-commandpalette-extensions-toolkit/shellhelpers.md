---
title: ShellHelpers Class definition
description: The ShellHelpers class provides methods for opening commands in a shell environment with different user contexts.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ShellHelpers Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ShellHelpers** class provides methods for opening commands in a shell environment. It allows you to execute commands with different user contexts, such as running as an administrator or as a different user.

## Methods

| Method | Description |
| :--- | :--- |
| [OpenCommandInShell(String, String, String, String, ShellRunAsType, Boolean)](shellhelpers_opencommandinshell.md) | Opens a command in a shell environment with the specified parameters. |
| [OpenInShell(String, String, String, ShellRunAsType, Boolean)](shellhelpers_openinshell.md) | Opens a shell with the specified command and parameters. |

## Enums

| Enum | Description |
| :--- | :--- |
| [ShellRunAsType](shellrunastype.md) | The **ShellRunAsType** enumeration defines the different types of user contexts in which a command can be executed in the Command Palette. This enumeration is used to specify how a command should be run. |
