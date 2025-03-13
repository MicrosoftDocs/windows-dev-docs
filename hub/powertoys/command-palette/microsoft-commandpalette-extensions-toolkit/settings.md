---
title: Settings Class
description: 
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Settings Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [ICommandSettings](../microsoft-commandpalette-extensions/icommandsettings.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| SettingsPage | [IContentPage](../microsoft-commandpalette-extensions/icontentpage.md) | |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, Settings> SettingsChanged | |

## Methods

| Method | Description |
| :--- | :--- |
| [Add<T>(Setting<T>)](settings_add.md) | |
| [GetSetting<T>(String)](settings_getsetting.md) | |
| [ToContent()](settings_tocontent.md) | |
| [ToJson()](settings_tojson.md) | |
| [TryGetSetting<T>(String, T)](settings_trygetsetting.md) | |
| [Update(String)](settings_update.md) | |
