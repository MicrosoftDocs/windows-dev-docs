---
title: IAccessKeyManagerStaticsDisplayMode.EnterDisplayMode method
description: Specifies that keytips for currently valid access keys should be displayed and the access keys enabled.
ms.topic: article
ms.date: 07/28/2023
ms.localizationpriority: low
---

# IAccessKeyManagerStaticsDisplayMode.EnterDisplayMode(XamlRoot) method

## Definition

Namespace: Windows.UI.Xaml.Input

Specifies that keytips for currently valid access keys should be displayed and the access keys enabled.

```csharp
[uuid(3e602318-59f6-5f2c-9752-bcbb9c907d45)]
interface IAccessKeyManagerStaticsDisplayMode 
{
    void EnterDisplayMode(Windows.UI.Xaml.XamlRoot xamlRoot);
};
```

### Parameters

**`xamlRoot`** [XamlRoot](/uwp/api/windows.ui.xaml.xamlroot)

The XamlRoot for the currently focused element. Cannot be `null`.

## Windows requirements

<table><tr><td>Device family</td><td>Windows 11, version 22H2 (introduced in 10.0.22621.0)</td></tr></table>

## Remarks

Calling this method has no effect if the scope is already in display mode. If another scope is in display mode, it will be exited.

After calling this method, the [IsDisplayModeEnabled](/uwp/api/windows.ui.xaml.input.accesskeymanager.isdisplaymodeenabled) property will be `true`.

Call [ExitDisplayMode](/uwp/api/windows.ui.xaml.input.accesskeymanager.exitdisplaymode) to disable display mode.
