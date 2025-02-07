---
title: ICommandProvider Interface
description: 
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DisplayName | String | |
| Frozen | Boolean | |
| Icon | [IIconInfo](iiconinfo.md) | |
| Id | String | |
| Settings | [ICommandSettings](icommandsettings.md) | |

## Methods

| Method | Description |
| :--- | :--- |
| [FallbackCommands()](icommandprovider_fallbackcommands.md) | |
| [GetCommand(String)](icommandprovider_getcommand.md) | |
| [InitializeWithHost(IExtensionHost)](icommandprovider_initializewithhost.md) | |
| [TopLevelCommands()](icommandprovider_toplevelcommands.md) | |
