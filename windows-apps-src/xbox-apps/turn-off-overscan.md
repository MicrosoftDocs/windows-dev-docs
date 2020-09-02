---
title: How to draw UI to the edge of the screen
description: Learn how to turn off the default borders placed at the edges of the viewport and draw your UI to the edges of the screen.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 1adb221f-6f70-4255-9329-2046a486ca45
ms.localizationpriority: medium
---
# How to draw UI to the edge of the screen   
By default, applications will have borders placed at the edges of the viewport to account for the TV-safe area (for more information, see [Designing for Xbox and TV](../design/devices/designing-for-tv.md#tv-safe-area)). 

We recommend turning this off and drawing to the edge of the screen. You can draw to the edge of the screen by adding the following code when your application starts:
   
```
Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode(Windows.UI.ViewManagement.ApplicationViewBoundsMode.UseCoreWindow);
```
   
> [!NOTE]
> C++/DirectX applications do not have to worry about this. The system will always render your application to the edge of the screen.

## See also
- [Best practices for Xbox](tailoring-for-xbox.md)
- [UWP on Xbox One](index.md)
