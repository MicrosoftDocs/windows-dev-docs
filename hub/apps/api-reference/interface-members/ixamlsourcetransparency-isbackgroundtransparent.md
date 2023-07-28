---
title: IXamlSourceTransparency.IsBackgroundTransparent property
description: Gets or sets the background transparency of all DesktopWindowXamlSource objects on the current thread.
ms.topic: article
ms.date: 07/28/2023
ms.author: jimwalk
author: jwmsft
ms.localizationpriority: low
---

# IXamlSourceTransparency.IsBackgroundTransparent property

## Definition

Namespace: Windows.UI.Xaml

Gets or sets a value that specifies whether the background of all DesktopWindowXamlSource objects on the current thread is transparent.

```csharp
[uuid(06636c29-5a17-458d-8ea2-2422d997a922)]
interface IXamlSourceTransparency
{
    Boolean IsBackgroundTransparent;
};
```

### Property Value

[Boolean](/dotnet/api/system.boolean)

`true` if the window background is transparent; otherwise, `false`.

## Windows requirements

<table><tr><td>Device family</td><td>Windows 10 (introduced in 10.0.17763.0)</td></tr></table>

## Examples

This example shows the [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) objects being configured to have a transparent background.

```csharp
// Make all DesktopWindowXamlSource objects on this
// thread have a transparent background.
var xamlSourceTransparency = (IXamlSourceTransparency)Window.Current;
xamlSourceTransparency.IsBackgroundTransparent = true;
```

With `IsBackgroundTransparent` set to `true`, if the following is set as the content of an island, the first column will show the content of the host but the second column will be white.

```xaml
<Grid ColumnDefinitions="Auto,*">
    <TextBlock>Column 0 text</TextBlock>

    <Border Grid.Column="1" Background="White">
        <TextBlock>Column 1 text</TextBlock>
    </Border>
</Grid>
```

## Remarks

The [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) class is used to host Xaml in another app. For example, you can use this to host some Xaml content in a WPF or WinForms app. See [Host WinRT XAML controls in desktop apps (XAML Islands)](/windows/apps/desktop/modernize/xaml-islands) for more info.

By default, the XAML content has an opaque background, meaning that it's not possible to have any of the host content behind the XAML show through. (In WinUI3, this behavior is changed; the XAML always has a transparent background.)

Set this property to `true` to give all [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) objects on the current thread a transparent background.

This interface is retrieved from a XAML [Window](/uwp/api/windows.ui.xaml.window).

> [!NOTE]
> Setting this property to `true` in a XAML UWP app will cause a XAML [Window](/uwp/api/Windows.UI.Xaml.Window) to be transparent as well when it's in full screen mode (when you've called [ApplicationView.TryEnterFullScreenMode](/uwp/api/Windows.UI.ViewManagement.ApplicationView.TryEnterFullScreenMode)).
