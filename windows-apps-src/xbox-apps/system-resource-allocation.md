---
author: Mtoepke
title: System resources for UWP apps and games on Xbox One
description: UWP on Xbox system resources
ms.author: mstahl
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 12e87019-4315-424e-b73c-426d565deef9
ms.localizationpriority: medium
---

# System resources for UWP apps and games on Xbox One

UWP apps running on Xbox One share resources with the system and other apps. 
The resources available to a UWP on Xbox One app depend on whether you submit as an app or as an Xbox Live Creators Program game.

* Maximum available memory while running in the foreground:
    * Apps: 1 GB
    * Games: 5 GB

The maximum memory available to an app running in the background is 128 MB. Background mode only applies to concurrent applications, like background music players.  Games will be suspended and terminated in the background.

Exceeding these limitations will cause memory allocation failures. For more information about monitoring memory use, see the [MemoryManager class](https://msdn.microsoft.com/library/windows/apps/windows.system.memorymanager.aspx) reference.
    
    > [!NOTE]
    > When running your app or game from the Visual Studio debugger, these memory constraints do not apply. This limit is only applicable when not running in debugging mode.

* CPU
    * Apps: share of 2-4 CPU cores depending on the number of apps and games running on the system.
    * Games: 4 exclusive and 2 shared CPU cores.

* GPU
    * Apps: share of 45% of the GPU depending on the number of apps and games running on the system.
    * Games: full access to available GPU cycles.

* DirectX support
    * Apps: DirectX 11 Feature Level 10.
    * Games: DirectX 12, and DirectX 11 Feature Level 10.

* All apps and games must target the x64 architecture in order to be developed or submitted to the store for Xbox.  

For **application development**, resources available may be limited in comparison to a standard PC and can vary based on the number of apps and games running on the system.

For **games development**, Xbox One, like other games consoles, 
is a specialized piece of hardware that requires a specific hardware-based development kit to access its full potential. 
If you are working on a game that requires access to the maximum potential of the Xbox One hardware, 
consider registering with the [ID@Xbox](http://www.xbox.com/Developers/id) program to get access to an Xbox One development kit.


For more information about system resources for UWP apps on Xbox One, see the first part of this video.
</br>
</br>
<iframe src="https://mva.microsoft.com/en-US/training-courses-embed/developing-xbox-one-applications-16860/Video-What-s-Unique--vk0fOPf9C_2006218965" width="636" height="480" allowFullScreen frameBorder="0"></iframe>

## See also
- [UWP on Xbox One](index.md)
- [Get started with the Xbox Live Creators Program](../xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md)
