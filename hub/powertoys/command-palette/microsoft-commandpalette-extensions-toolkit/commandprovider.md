---
title: CommandProvider Class
description: The CommandProvider class is a base class for creating command providers in the Command Palette.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandProvider Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ICommandProvider](../microsoft-commandpalette-extensions/icommandprovider.md)

The **CommandProvider** class is a base class for creating command providers in the Command Palette. It provides a set of properties and methods to manage commands, including top-level commands and fallback commands. The class also supports event handling for changes in command items.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| DisplayName | **String** | The display name of the command provider. |
| Frozen | **Boolean** | Indicates whether the command provider is frozen. A frozen provider cannot be modified. |
| [ICommandProvider.Icon](../microsoft-commandpalette-extensions/icommandprovider.md#properties) | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | The icon associated with the command provider. |
| Icon | [IconInfo](iconinfo.md) | The icon associated with the command provider. |
| Id | **String** | The unique identifier of the command provider. |
| Settings | [ICommandSettings](../microsoft-commandpalette-extensions/icommandsettings.md) | The settings associated with the command provider. |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)> ItemsChanged | Invoked when the command provider's items change. |

## Methods

| Method | Description |
| :--- | :--- |
| [Dispose()](commandprovider_dispose.md) | Releases the resources used by the command provider. |
| [FallbackCommands()](commandprovider_fallbackcommands.md) | Returns the fallback commands for the command provider. |
| [GetCommand(String)](commandprovider_getcommand.md) | Retrieves a command by its ID. |
| [InitializeWithHost(IExtensionHost)](commandprovider_initializewithhost.md) | Initializes the command provider with the specified host. |
| [RaiseItemsChanged(Int)](commandprovider_raiseitemschanged.md) | Raises the **ItemsChanged** event with the specified change type. |
| [TopLevelCommands()](commandprovider_toplevelcommands.md) | Returns the top-level commands for the command provider. |
