---
title: Utilities.BaseSettingsPath(String) Method
description: 
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Utilities.BaseSettingsPath(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Used to produce a path to a settings folder which your app can use. If your app is running packaged, this will return the redirected local app data path (Packages/{your_pfn}/LocalState). If not, it'll return %LOCALAPPDATA%\{`settingsFolderName`}.

## Parameters

**`settingsFolderName`** String

## Returns

String
