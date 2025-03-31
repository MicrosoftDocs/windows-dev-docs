---
title: ShellRunAsType Enum
description: The ShellRunAsType enumeration defines the different types of user contexts in which a command can be executed in the Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ShellRunAsType Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ShellRunAsType** enumeration defines the different types of user contexts in which a command can be executed in the Command Palette. This enumeration is used to specify how a command should be run.

## Fields

| Name | Description |
| :--- | :--- |
| Administrator | Indicates that the command should be run with elevated privileges (as an administrator). |
| None | Indicates that the command should be run with the current user's privileges (no elevation). |
| OtherUser | Indicates that the command should be run as a different user. This requires additional user input to specify the user context. |
