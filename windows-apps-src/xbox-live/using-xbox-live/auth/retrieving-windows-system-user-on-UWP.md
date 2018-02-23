---
title: Retrieving Windows System User on UWP
author: KevinAsgari
description: Learn how to retrieve the Windows System User in a Universal Windows Platform (UWP) game.
ms.author: kevinasg
ms.date: 06/07/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, system user
ms.localizationpriority: low
---

# Retrieving the Windows System User in a Universal Windows Platform (UWP) title

## Windows.System.User

A [Windows.System.User](https://docs.microsoft.com/en-us/uwp/api/windows.system.user) object represents a local Windows user. On Xbox console, it allows multiple windows user to be logged in at the same time in a single interactive session, if your app is a multi-user application, then a Windows.System.User object would be required to construct a XboxLiveUser in order to access live services. On other windows platforms like PC or phone, it's either only allow one windows user on one device or one interactive session, passing in a Windows.System.User to construct an XboxLiveUser is not required.

## Ways to retrieve Windows System User

* **Using static methods from [Windows.System.User](https://docs.microsoft.com/en-us/uwp/api/windows.system.user) class.**

  [Windows.System.User](https://docs.microsoft.com/en-us/uwp/api/windows.system.user) class provides a set of static method to help retrieving Windows.System.User objects. For instance you may call FindAllAsync to get all active windows users.

* **[UserPicker](https://docs.microsoft.com/en-us/uwp/api/windows.system.userpicker)**

  [Windows.System.UserPicker](https://docs.microsoft.com/en-us/uwp/api/windows.system.userpicker) class provides methods to launch user picker UI and select a windows system user in multi-user scenarios.

* **[IGameController](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.igamecontroller)**

  [Windows.Gaming.Input.IGameController](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.igamecontroller) is the core interface for all controller devices(gamepad, racing wheel, flight stick etc.). You may get the Windows.System.User object associated with the game controller by calling its User property.  

  Here are couple game controller implemented by windows [ArcadeStick](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.arcadestick), [FlightStick](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.flightstick), [Gamepad](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.gamepad), [RacingWheel](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.input.racingwheel).

* **[UserDeviceAssociation](https://docs.microsoft.com/en-us/uwp/api/windows.system.userdeviceassociation)**

  You may call static method FindUserFromDeviceId to find the associated user with the device id. You can usually get the device id from windows input events, like [Windows.​UI.​Xaml.​Input.KeyRoutedEventArgs](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Input.KeyRoutedEventArgs), [Windows.​UI.​Core.KeyEventArgs](https://docs.microsoft.com/en-us/uwp/api/windows.ui.core.keyeventargs)
