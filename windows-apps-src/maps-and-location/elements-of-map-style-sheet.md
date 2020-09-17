---
description: Learn about using map style sheets to define the appearance of maps, such as those displayed in a Windows Store application's MapControl.
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Map style sheet reference
ms.date: 03/19/2017
ms.topic: article
keywords: windows 10, uwp, maps, map style sheet
ms.localizationpriority: medium
---
# Map style sheet reference

Microsoft mapping technologies use [map style sheets](/BingMaps/styling/map-style-sheets) to define the appearance of maps, including those displayed in a Windows Store application's [MapControl](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol).  A map style sheet is defined using JavaScript Object Notation (JSON) through the [MapStyleSheet.ParseFromJson](/uwp/api/windows.ui.xaml.controls.maps.mapstylesheet.parsefromjson#Windows_UI_Xaml_Controls_Maps_MapStyleSheet_ParseFromJson_System_String_) method.

A style sheet consists of [entries](/BingMaps/styling/map-style-sheet-entries) whose properties are overridden to change the final appearance of the map.

For example, the following JSON can be used to make water areas appear in red, water labels appear in green, and land areas appear in blue:

```json
    {"version":"1.*",
        "settings":{"landColor":"#0000FF"},
        "elements":{"water":{"fillColor":"#FF0000","labelColor":"#00FF00"}}
    }
```

Style sheets can be created interactively using the [Map Style Sheet Editor](https://www.microsoft.com/p/map-style-sheet-editor/9nbhtcjt72ft) application.