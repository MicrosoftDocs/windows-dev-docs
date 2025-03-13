---
title: CommandProvider Class
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandProvider Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ICommandProvider](../microsoft-commandpalette-extensions/icommandprovider.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DisplayName | String | |
| Frozen | Boolean | |
| [ICommandProvider.Icon](../microsoft-commandpalette-extensions/icommandprovider.md#properties) | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | |
| Icon | [IconInfo](iconinfo.md) | |
| Id | String | |
| Settings | [ICommandSettings](../microsoft-commandpalette-extensions/icommandsettings.md) | |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)> ItemsChanged | |

## Methods

| Method | Description |
| :--- | :--- |
| [Dispose()](commandprovider_dispose.md) | |
| [FallbackCommands()](commandprovider_fallbackcommands.md) | |
| [GetCommand(String)](commandprovider_getcommand.md) | |
| [InitializeWithHost(IExtensionHost)](commandprovider_initializewithhost.md) | |
| [RaiseItemsChanged(Int)](commandprovider_raiseitemschanged.md) | |
| [TopLevelCommands()](commandprovider_toplevelcommands.md) | |
