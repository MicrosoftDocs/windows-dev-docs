---
author: mcleanbyron
ms.assetid: 2fba38c4-11be-4058-bfa3-5f979390791c
description: Learn how to handle the events of the AdControl class.
title: AdControl events in C#
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdControl, events
---

# AdControl events in C\# #  


The following examples demonstrate basic event handlers for the following [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) events: [ErrorOccurred](https://msdn.microsoft.com/library/windows/apps/xaml/microsoft.advertising.winrt.ui.adcontrol.erroroccurred.aspx), [AdRefreshed](https://msdn.microsoft.com/library/windows/apps/xaml/microsoft.advertising.winrt.ui.adcontrol.adrefreshed.aspx), and [IsEngagedChanged](https://msdn.microsoft.com/library/windows/apps/xaml/microsoft.advertising.winrt.ui.adcontrol.isengagedchanged.aspx). These examples assume that you have already assigned the event handlers to the events in your XAML code. For more information about how to do this, see [XAML properties example](xaml-properties-example.md).

For more information about handling events in C#, see [Events and routed events overview (Universal Windows apps using C#/VB/C++ and XAML)](http://msdn.microsoft.com/library/windows/apps/hh758286).

## Examples

[!code-cs[AdControl](./code/AdvertisingSamples/AdControlSamples/cs/MainPage.xaml.cs#EventHandlers)]

## Related topics

* [Advertising samples on GitHub](http://aka.ms/githubads)
* [AdControl error handling](adcontrol-error-handling.md)
* [RoutedEventArgs Class](http://msdn.microsoft.com/library/system.windows.routedeventargs.aspx)

 

 
