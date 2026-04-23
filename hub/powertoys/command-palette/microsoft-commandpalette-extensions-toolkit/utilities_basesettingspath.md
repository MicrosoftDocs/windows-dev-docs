---
title: Utilities.BaseSettingsPath(String) Method
description: The BaseSettingsPath utility method is used to produce a path to a settings folder which your app can use.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Utilities.BaseSettingsPath(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Used to produce a path to a settings folder which your app can use. This is useful for storing settings or other data that your app needs to persist between runs. The path returned will be different depending on whether your app is running packaged or not.

## Parameters

*settingsFolderName* **String**

A **String** representing the name of the settings folder. This is the name of the folder that will be created in the settings path. The folder will be created if it does not already exist.

## Returns

A **String** representing the path to the settings folder. If your app is running packaged, this will return the redirected local app data path (`Packages/{your_pfn}/LocalState`). If not, it'll return `%LOCALAPPDATA%\{settingsFolderName}`.
