---
title: ICommandProvider Interface
description: The ICommandProvider interface is used to provide commands for the Command Palette to use.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

This is the interface that an extension must implement to provide commands to the Command Palette. The Command Palette will call this interface to get the commands that it should display.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DisplayName | **String** | The display name of the command provider. This is used to identify the provider in the Command Palette. |
| Frozen | **Boolean** | Indicates whether the command provider is frozen. A frozen command provider will not be updated or refreshed. |
| Icon | [IIconInfo](iiconinfo.md) | The icon associated with the command provider. This is used to display an icon in the Command Palette. |
| Id | **String** | The unique identifier of the command provider. This is used to identify the provider in the Command Palette. |
| Settings | [ICommandSettings](icommandsettings.md) | The settings associated with the command provider. This is used to provide additional information or resources related to the item being displayed in the Command Palette. |

## Methods

| Method | Description |
| :--- | :--- |
| [FallbackCommands()](icommandprovider_fallbackcommands.md) | Returns the fallback commands for the command provider. |
| [GetCommand(String)](icommandprovider_getcommand.md) | Returns the command associated with the specified ID. |
| [InitializeWithHost(IExtensionHost)](icommandprovider_initializewithhost.md) | Initializes the command provider with the specified host. This is called when the command provider is first created. |
| [TopLevelCommands()](icommandprovider_toplevelcommands.md) | Returns the top-level commands for the command provider. |
