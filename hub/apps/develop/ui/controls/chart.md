---
title: Chart control for WinUI apps
description: Learn how to set up the experimental Chart control in the Windows App SDK to present Cartesian data as line, area, and bar series.
author: GrantMeStrength
ms.author: jken
ms.date: 11/16/2026
ms.topic: overview
ms.localizationpriority: medium
---

# Chart control for WinUI apps

> [!IMPORTANT]
> The `Chart` control is available starting in [Windows App SDK 2.5.4-experimental](../../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=experimental#version-25-experimental-254-experimental). The APIs are marked experimental and might change or be removed in a future release. Don't use them in production apps.

Use the experimental [`Chart`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.chart?view=windows-app-sdk-2.0-experimental&preserve-view=true) control to present Cartesian data as line, area, and bar series. Chart supports configurable axes, legends, data labels, and data markers.

To try the control, install the Windows App SDK 2.6.6-experimental package and matching runtime. For download links and setup information, see [Latest Windows App SDK downloads](../../../windows-app-sdk/downloads.md#experimental-release).

> [!div class="checklist"]
>
> - Important APIs: [Chart class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.chart?view=windows-app-sdk-2.0-experimental&preserve-view=true), [LineSeries class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.lineseries?view=windows-app-sdk-2.0-experimental&preserve-view=true), [AreaSeries class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.areaseries?view=windows-app-sdk-2.0-experimental&preserve-view=true), [BarSeries class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.barseries?view=windows-app-sdk-2.0-experimental&preserve-view=true), [XamlChartsResources class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.xamlchartsresources?view=windows-app-sdk-2.0-experimental&preserve-view=true)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the Chart in action](winui3gallery://item/Chart)

[!INCLUDE [winui-3-gallery](../../../../includes/winui-3-gallery.md)]

The WinUI 3 Gallery includes an experimental Chart sample. You can also view the [Chart sample source code](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/Samples/Chart) on GitHub.

For the complete set of classes and enums in the namespace, see the [Microsoft.UI.Xaml.Controls.Charts namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts?view=windows-app-sdk-2.0-experimental&preserve-view=true) reference.

## Add Chart to your app

Chart requires theme resources that aren't included in `XamlControlsResources`. In *App.xaml*, merge [`XamlChartsResources`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.charts.xamlchartsresources?view=windows-app-sdk-2.0-experimental&preserve-view=true) into your app resources after `XamlControlsResources`:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            <XamlChartsResources xmlns="using:Microsoft.UI.Xaml.Controls.Charts" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

If you don't merge these resources, your app throws a XAML parse exception when it first displays a Chart. The exception message includes `Cannot find a Resource with the Name/Key ChartsControlBackgroundBrush`.

> [!TIP]
> Starting in Windows App SDK 2.6.6-experimental, if `XamlChartsResources` isn't merged when a Chart is created, Chart writes a message that starts with `Chart setup:` to the Visual Studio **Output** window.
> The message shows the entry to add to *App.xaml*.
> To see it, debug with native or mixed-mode debugging.
> In a C# project, select **Enable native code debugging** in the project's debug launch profile.
