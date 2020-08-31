---
title: What's new for UWP on Xbox One
description: See new features, updates to existing features, and bug fixes for developers in the latest update of UWP on Xbox One.
ms.date: 03/29/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: fe63c527-8f06-43a5-868f-de909f5664b3
ms.localizationpriority: medium
---
# What's new for developers in the latest update of UWP on Xbox One

The latest update of Universal Windows Platform (UWP) on Xbox One contains the following new features, 
updates to existing features, and bug fixes.

## x86 apps and games are no longer supported on Xbox  
Xbox no longer supports x86 app development or x86 app submissions to the store.

## Apps can now support navigating back to the previous app 
UWP on Xbox One apps can now support navigating back to the previous app. To do this, subscribe to the 
[**Windows.UI.Core.SystemNavigationManager.BackRequested**](/uwp/api/Windows.UI.Core.SystemNavigationManager)
event and set the **Handled** property to **false** in your event handler.

> [!NOTE]
> For compatibility reasons, this functionality is available only to apps that are built with the most recent release of UWP on Xbox One. 

## Dev Home is now the default home experience on development consoles
Development consoles now launch Dev Home as the default home experience. This lets you get right to work without the need to click through 
from the retail Home screen. Dev Home now includes a quick action to launch the retail Home screen. Also, a new setting allows you to make 
retail Home the default experience. 

## New Dev Home user interface
The Dev Home user interface now includes the following productivity enhancements:
 - Important data like IP address and recovery version is now displayed at the top of the screen for visibility. 
 - Dev Home now has a tabbed UI that groups tools into logical sets, allowing quick navigation.
 - Quick-action buttons on the first tab of Dev Home allow fast access to the most commonly used actions. 

## WDP for Xbox enhancements
The Windows Device Portal (WDP) now includes additional support for console settings. 

## You can now switch the type of your UWP title between "App" and "Game"
Switching the type of your UWP title between "App" and "Game" allows you to test game scenarios without publishing to the store. 
In Dev Home, select the app in the **Games & Apps** pane, press the View button on the controller, select **App details** and then change the 
type to "App" or "Game".

## See also
- [Known issues](known-issues.md)
- [UWP on Xbox One](index.md)
 - You can now capture a screenshot of the console. For more information about taking a screenshot, see the [/ext/screenshot](wdp-media-capture-api.md) reference topic.
 - The tool can deploy a loose file build of your app. For more information about loose file builds, see the [/api/app/packagemanager/register](wdp-loose-folder-register-api.md) reference topic.
 - Developer files on your console can be accessed from File Explorer on your development PC. For more information about accessing files through File Explorer, see the [/ext/smb/developerfolder](wdp-smb-api.md) reference topic.

## See also
- [Known issues](known-issues.md)
- [UWP on Xbox One](index.md)