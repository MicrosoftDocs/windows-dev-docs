---
author: Mtoepke
title: System resources for UWP apps and games on Xbox One
description: UWP on Xbox system resources
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 12e87019-4315-424e-b73c-426d565deef9
---

# System resources for UWP apps and games on Xbox One

UWP apps and games running on Xbox One share resources with the system and other apps. 
Therefore, UWP apps and games will have access to the following resources:

* The maximum memory available to an app running in the foreground is 1 GB.
    * The maximum memory available to an app running in the background is 128 MB.
    * Apps that exceed these memory requirements will encounter memory allocation failures. For more information about monitoring memory use, see the [MemoryManager class](https://msdn.microsoft.com/library/windows/apps/windows.system.memorymanager.aspx) reference.
    
    > [!NOTE]
    > When running your application or game from the Visual Studio debugger, these memory constraints do not apply. This limit is only applicable when not running in debugging mode.

* Share of 2-4 CPU cores depending on the number of apps and games running on the system.

* Share of 45% of the GPU depending on the number of apps and games running on the system.

* UWP on Xbox One supports DirectX 11 Feature Level 10. DirectX 12 is not supported at this time.

* All apps must target the x64 architecture in order to be developed or submitted to the store for Xbox.  

For **application development**, it's important to keep in mind that the resources available may be limited in comparison to a standard PC.

For **games development**, it’s important to keep in mind that Xbox One, like other games consoles, 
is a specialized piece of hardware that requires a specific hardware-based development kit to access its full potential. 
If you are working on a game that requires access to the maximum potential of the Xbox One hardware, 
you can register with the [ID@Xbox](http://www.xbox.com/Developers/id) program to get access to Xbox One development kits, which include DirectX 12 support.

## See also
- [UWP on Xbox One](index.md)
