---
title: ShellHelpers.OpenInShell(String, String, String, ShellRunAsType, Boolean) Method
description: The OpenInShell method opens a shell with the specified command and parameters for the Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ShellHelpers.OpenInShell(String, String, String, [ShellRunAsType](shellrunastype.md), Boolean) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **OpenInShell** method opens a shell with the specified command and parameters. This method allows you to execute commands in a shell environment with different user contexts, such as running as an administrator or as a different user.

## Parameters

*path* **String**

The path to the shell executable. This can be a full path or a relative path.

*arguments* **String**

The arguments to pass to the shell executable. This can include command-line options and parameters.

*workingDir* **String**

The working directory for the shell. This is the directory in which the shell will start executing commands.

*runAs* [ShellRunAsType](shellrunastype.md)

The user context in which to run the shell.

*runWithHiddenWindow* **Boolean**

The flag indicating whether to run the shell with a hidden window. If set to `true`, the shell will not be visible to the user. This is useful for background tasks or when you don't want to interrupt the user's workflow.

## Returns

A **Boolean** value indicating whether the shell was opened successfully.
