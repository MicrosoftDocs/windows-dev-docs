---
title: Connected apps and devices (Project Rome)
description: This section describes how to use the Remote Systems platform to discover remote devices, launch an app on a remote device, and communicate with an app service on a remote device.
ms.date: 06/08/2018
ms.topic: article
keywords: windows 10, uwp, connected devices, remote systems, rome, project rome
ms.assetid: 7f39d080-1fff-478c-8c51-526472c1326a
ms.localizationpriority: medium
---
# Connected apps and devices (Project Rome)

This section explains how to connect apps across devices and platforms using [Project Rome](https://developer.microsoft.com/windows/project-rome). To learn how to implement Project Rome in a cross-platform scenario, visit the [main docs page for Project Rome](/windows/project-rome/).

Most users have multiple devices and often begin an activity on one device and finish it on another. To accommodate this, apps need to span devices and platforms. Project Rome allows you to discover remote devices, launch an app on a remote device, and communicate with an app service on a remote device.

The [Remote Systems APIs](/uwp/api/Windows.System.RemoteSystems)
introduced in Windows 10, version 1607 enable you to write apps that allow users to start a task on one device and finish it on another. The task remains the central focus, and users can do their work on the device that is most convenient. For example, a user might be listening to the radio on their phone in the car, but when they get home they may want to transfer playback to their Xbox One which is hooked up to the home stereo system.

You can also use Project Rome for companion devices or remote control scenarios. Use the app service messaging APIs to create an app channel between two devices to send and receive custom messages. For example, you can write an app for your phone that controls playback on your TV, or a companion app that provides information about the characters on a TV show you are watching through another app.  

Devices can be connected proximally through Bluetooth and wireless, or remotely through the cloud; they are linked by the Microsoft account (MSA) of the person using them.

See the [Remote Systems UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/RemoteSystems ) for examples of how to discover remote system, launch an app on a remote system, and use app services to send messages between apps running on two systems.

For more information on Project Rome in general, including resources for cross-platform integration, go to [aka.ms/project-rome](https://developer.microsoft.com/windows/project-rome).

| Topic | Description |
|-------|-------------|
| [Launch an app on a remote device](launch-a-remote-app.md) | Learn how to launch an app on a remote device. This topic covers the simplest use case and preliminary setup.  |
| [Discover remote devices](discover-remote-devices.md)  | Learn how to discover devices that you can connect to. |
| [Communicate with a remote app service](communicate-with-a-remote-app-service.md) | Learn how to interact with an app on a remote device. |
| [Connect devices through remote sessions](remote-sessions.md) | Create shared experiences across multiple devices by joining them in a remote session. |
| [Continue user activity, even across devices](useractivities.md)| Help users resume what they were doing in your app, even across multiple devices.|
| [User Activities best practices](useractivities-best-practices.md)| Learn the recommended practices for creating and updating User Activities.|