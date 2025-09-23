---
title: Headset
description: Use the Windows.Gaming.Input headset APIs to detect headsets, capture player voice, and playback audio.
ms.assetid: 021CCA26-D339-4C8B-B084-0D499BD83ABE
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, headset
ms.localizationpriority: medium
---
# Headset

This page describes the basics of programming for headsets using [Windows.Gaming.Input.Headset][headset] and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:
* How to access a headset that's connected to an input or navigation device
* How to detect that a headset has been connected or disconnected


## Headset overview

Headsets are audio capture and playback devices most often used to communicate with other players in online games but can also be used in gameplay or for other creative uses. Headsets are supported in Windows 10 or Windows 11 and Xbox UWP apps through the [Windows.Gaming.Input][] namespace.


## Detect and track headsets

Headsets are managed by the system, therefore you don't have to create or initialize them. The system provides access to a headset through the input device its connected to and events to notify you when a headset is connected or disconnected.

### IGameController.Headset

All input devices in the [Windows.Gaming.Input][] namespace implement the [IGameController][] interface which defines the [Headset][igamecontroller.headset] property to be the headset currently connected to the device.

### Connecting and disconnecting headsets.

When a headset is connected or disconnected, the [HeadsetConnected][igamecontroller.headsetconnected] and [HeadsetDisconnected][igamecontroller.headsetdisconnected] events are raised. You can register handlers for these events to keep track of whether an input device currently has a headset connected to it.

The following example shows how to register a handler for the `HeadsetConnected` event.

```cpp
auto inputDevice = myGamepads[0]; // or arcade stick, racing wheel

inputDevice.HeadsetConnected += ref new TypedEventHandler<IGameController^, Headset^>(IGameController^ device, Headset^ headset)
{
    // enable headset capture and playback on this device
}
```

The following example shows how to register a handler for the `HeadsetDisconnected` event.

```cpp
auto inputDevice = myGamepads[0]; // or arcade stick, racing wheel

inputDevice.HeadsetDisconnected += ref new TypedEventHandler<IGameController^, Headset^>(IGameController^ device, Headset^ headset)
{
    // disable headset capture and playback on this device
}
```

## Using the headset

The [Headset][] class is made up of two strings that represent XAudio endpoint IDs--one for audio capture (recording from the headset microphone) and one for audio rendering (playback through the headset earpiece).

The details of working with XAudio are not discussed here, for more information see the [XAudio2 programming guide](/windows/desktop/xaudio2/programming-guide) and [XAudio2 API reference](/windows/desktop/xaudio2/programming-reference).


[Windows.Gaming.Input]: /uwp/api/Windows.Gaming.Input
[igamecontroller]: /uwp/api/Windows.Gaming.Input.IGameController
[igamecontroller.headset]: /uwp/api/Windows.Gaming.Input.IGameController
[igamecontroller.headsetconnected]: /uwp/api/Windows.Gaming.Input.IGameController
[igamecontroller.headsetdisconnected]: /uwp/api/Windows.Gaming.Input.IGameController
[headset]: /uwp/api/Windows.Gaming.Input.Headset