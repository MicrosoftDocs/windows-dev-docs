---
author: mithom
title: Input for games
description: This section demonstrates how to work with gamepads and other input devices for Universal Windows Platform (UWP) games.
ms.assetid: 2DD0B384-8776-4599-9E52-4FC0AA682735
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, input
---

# Input for games

This section describes the different kinds of input devices that can be used in Universal Windows Platform (UWP) games on Windows 10 and Xbox One, demonstrates their basic usage, and recommends patterns and techniques for effective input programming in games.

> **Note**    Other kinds of input devices exist and are available to be used in UWP games such as custom input devices that might be genre-specific or game-specific. Such devices and their programming are not discussed in this section. For information on the interfaces used to facilitate custom input devices, see the [Windows.Gaming.Input.Custom][] namespace.

## Gaming input devices

Game input devices are supported in UWP games and apps for Windows 10 and Xbox One by the [Windows.Gaming.Input][] namespace.

### Gamepads

Gamepads are the standard input device on Xbox One and a common choice for Windows gamers when they don't favor a keyboard and mouse. They provide a variety of digital and analog controls making them suitable for almost any kind of game and also provide tactile feedback through embedded vibration motors.

For information on how to use gamepads in your UWP game, see [Gamepad and vibration](gamepad-and-vibration.md).

### Arcade sticks

Arcade sticks are all-digital input devices valued for reproducing the feel of stand-up arcade machines and are the perfect input device for head-to-head-fighting or other arcade-style games.

For information on how to use arcade sticks in your UWP game, see [Arcade stick](arcade-stick.md).

### Racing wheels

Racing wheels are input devices that resemble the feel of a real racecar cockpit and are the perfect input device for any racing game that features cars or trucks. Many racing wheels are equipped with true force feedback--that is, they can apply actual forces on an axis of control such as the steering wheel--not just simple vibration.

For information on how to use racing wheels in your UWP game, see [Racing Wheel and force feedback](racing-wheel-and-force-feedback.md).

### UI navigation controller

UI Navigation controllers are a logical input device that exists to provide a common vocabulary for UI navigation commands that promotes a consistent user experience across different games and physical input devices. A game's user interface should use the UINavigationController interfaces instead of device-specific interfaces.

For information on how to use UI navigation controllers in your UWP game, see [UI navigation controller](ui-navigation-controller.md).

### Headsets

Headsets are audio capture and playback devices that are associated with a specific user when connected through their input device. They're commonly used by online games for voice chat but can also be used to enhance immersion or provide gameplay features in both online and offline games.

For information on how to use headsets in your UWP game, see [Headset](headset.md)

### Users

Each input device and its connected headset can be associated with a specific user to link their identity to their gameplay. The user identity is also the means by which input from a physical input device is correlated to input from its logical UI navigation controller.

For information on how to manage users and their input devices, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices).

## See Also
[Windows.Gaming.Input.Custom][]


[Windows.Gaming.Input]: https://msdn.microsoft.com/library/windows/apps/windows.gaming.input.aspx
[Windows.Gaming.Input.Custom]: https://msdn.microsoft.com/en-us/library/windows/apps/windows.gaming.input.custom.aspx
