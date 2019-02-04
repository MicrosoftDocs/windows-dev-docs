---
author: Karl-Bridge-Microsoft
Description: Optimize your app for input from Xbox gamepad and remote control.
title: Gamepad and remote control interactions
ms.assetid: 784a08dc-2736-4bd3-bea0-08da16b1bd47
label: Gamepad and remote interactions
template: detail.hbs
isNew: true
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Gamepad and remote control interactions

![keyboard and gamepad image](images/keyboard/keyboard-gamepad.jpg)

***Common interaction patterns are shared between gamepad, remote control, and keyboard***

Making sure that your app works well with gamepad and remote is the most important step in optimizing for 10-foot experiences. There are several gamepad and remote-specific improvements that you can make to optimize the user interaction experience on a device where their actions are somewhat limited.

Universal Windows Platform (UWP) apps now support gamepad and remote control input. 

Gamepads and remote controls are the primary input devices for Xbox and TV experiences. 

UWP apps should be optimized for these input device types, just like they are for keyboard and mouse input on a PC, and touch input on a phone or tablet. 

Making sure that your app works well with these input devices is the most important step when optimizing for Xbox and the TV.

You can now plug in and use the gamepad with UWP apps on PC which makes validating the work easy.

To ensure a successful and enjoyable user experience for your UWP app when using a gamepad or remote control, you should consider the following:

* [Hardware buttons](../devices/designing-for-tv.md#hardware-buttons) - The gamepad and remote provide very different buttons and configurations.

* [XY focus navigation and interaction](../devices/designing-for-tv.md#xy-focus-navigation-and-interaction) - XY focus navigation enables the user to navigate around your app's UI.

* [Mouse mode](../devices/designing-for-tv.md#mouse-mode) - Mouse mode lets your app emulate a mouse experience when XY focus navigation isn't sufficient.
