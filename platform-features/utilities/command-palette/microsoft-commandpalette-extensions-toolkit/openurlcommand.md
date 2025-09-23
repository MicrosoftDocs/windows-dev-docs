---
title: OpenUrlCommand Class
description: The OpenUrlCommand class is used to represent a command that opens a URL in the default web browser.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenUrlCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **OpenUrlCommand** class is used to represent a command that opens a URL in the default web browser.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [OpenUrlCommand(String)](openurlcommand_constructor.md) | Initializes a new instance of the OpenUrlCommand class with the specified URL. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | Gets the result of the command. This property is used to indicate whether the command was successful or not. |

## Methods

| Method | Description |
| :--- | :--- |
| [Invoke()](openurlcommand_invoke.md) | Invokes the command. This method opens the specified URL in the default web browser. |
