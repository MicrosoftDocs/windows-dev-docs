---
title: ShellHelpers.OpenCommandInShell(String, String, String, String, ShellRunAsType, Boolean) Method
description: The ShellHelpers.OpenCommandInShell method opens a command in the shell with the specified parameters.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ShellHelpers.OpenCommandInShell(String, String, String, String, [ShellRunAsType](shellrunastype.md), Boolean) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **OpenCommandInShell** method opens a command in the shell with the specified parameters.

## Parameters

*path* **String**

The path to the shell executable. This can be a full path or a relative path.

*pattern* **String**

The pattern to match the command. This can be a wildcard pattern or a regular expression.

*arguments* **String**

The arguments to pass to the shell. This can include options, flags, and other parameters.

*workingDir* **String**

The working directory for the shell. This is the directory where the command will be executed.

*runAs* [ShellRunAsType](shellrunastype.md)

The user context in which to run the shell.

*runWithHiddenWindow* **Boolean**

The flag indicating whether to run the command in a hidden window.

## Returns

A **Boolean** value indicating whether the command was successfully opened in the shell.
