---
author: mijacobs
Description: Notifications Visualizer is a new Universal Windows Platform (UWP) app in the Store that helps developers design adaptive live tiles for Windows 10.
title: Notifications Visualizer
ms.assetid: FCBB7BB1-2C79-484B-8FFC-26FE1934EC1C
template: detail.hbs
ms.author: mijacobs
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Notifications Visualizer

 


Notifications Visualizer is a new Universal Windows Platform (UWP) app in [the Store](https://www.microsoft.com/store/apps/notifications-visualizer/9nblggh5xsl1) that helps developers design adaptive live tiles for Windows 10.

## Overview


The Notifications Visualizer app provides instant visual previews of your tile as you edit, similar to Visual Studio's XAML editor/design view. The app also checks for errors, which ensures that you create a valid tile payload.

This screenshot from the app shows the XML payload and how tile sizes appear on a selected device:

![screenshot of notifications visualizer app editor with code and tiles](images/notif-visualizer-001.png)

 

With Notifications Visualizer, you can create and test adaptive tile payloads without having to edit and deploy the app itself. Once you've created a payload with ideal visual results you can integrate that into your app. See [Send a local tile notification](sending-a-local-tile-notification.md) to learn more.

**Note**   Notifications Visualizer's simulation of the Windows Start menu isn't always completely accurate, and it doesn't support some payload properties like [baseUri](https://msdn.microsoft.com/library/windows/apps/br208712). When you have the tile design you want, test it by pinning the tile to the actual Start menu to verify that it appears as you intend.

 

## Features


Notifications Visualizer comes with a number of sample payloads to showcase what's possible with adaptive live tiles and to help you get started. You can experiment with all the different text options, groups/subgroups, background images, and you can see how the tile adapts to different devices and screens. Once you've made changes, you can save your updated payload to a file for future use.

The editor provides real-time errors and warnings. For example, if your app payload is limited to less than 5 KB (a platform limitation), Notifications Visualizer warns you if your payload exceeds that limit. It gives you warnings for incorrect attribute names or values, which helps you debug visual issues.

You can control tile properties like display name, color, logos, ShowName, badge value. These options help you instantly understand how your tile properties and tile notification payloads interact, and the results they produce.

This screenshot from the app shows the tile editor:

![screenshot of notifications visualizer editor with tiles](images/notif-visualizer-004.png)

 

## Related topics


* [Get Notifications Visualizer in the Store](https://www.microsoft.com/store/apps/notifications-visualizer/9nblggh5xsl1)
* [Create adaptive tiles](create-adaptive-tiles.md)
* [Tiles and toasts (MSDN blog)](http://blogs.msdn.com/b/tiles_and_toasts/)