---
title: Use the Xbox Live NuGet package with the XDK
author: KevinAsgari
description: Learn how to use the Xbox Live API NuGet package to develop XDK titles.
ms.assetid: 2c5ae514-393d-48bb-afd8-a897d35f7938
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, NuGet
ms.localizationpriority: low
---

# Use the Xbox Live API NuGet package to develop XDK titles

### 1.	Ensure you have the latest NuGet Package Manager installed
1.	Check your current version:
	- On the menu bar, select Tools-> Extensions and Updates.
	- Under the Installed tab,  look for `NuGet Package Manager`
![](../images/nuget/nuget_uwp_install_1.png)
2.	To update your current version:
	- On the menu bar, select Tools-> Extensions and Updates.
	- Under the Updates->Visual Studio Gallery tab, select `Update`
![](../images/nuget/nuget_uwp_install_2.png)

### 2.	Add reference to the project
1.	Add reference to the project
	1.	Right click on your project solution and select "Manage NuGet Packages"
<br/>
![](../images/nuget/nuget_xbox_install_4.png)
1.	Search for `Xbox Live` and select the appropriate package and click `Install`.
  - The Xbox Services API comes in flavors for both UWP and XDK, and for C++ and WinRT.  
  - Choose between `Microsoft.Xbox.Live.SDK.*.UWP` and `Microsoft.Xbox.Live.SDK.*.XboxOneXDK`.  `XboxOneXDK` is for ID@Xbox and Managed developers who are using the Xbox One XDK.  `UWP` is for UWP games which can run on either PC, the Xbox One, or Windows Phone.  You can read more about running UWP on Xbox One at [https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/getting-started](https://docs.microsoft.com/en-us/windows/uwp/xbox-apps/getting-started)
  - Choose between `Microsoft.Xbox.Live.SDK.Cpp.*` and `Microsoft.Xbox.Live.SDK.WinRT.*`. `Cpp` is for C++ game engines using the Xbox Live APIs.  `WinRT` is for game engines written with C++, C#, or Javascript using the Xbox Live APIs.  When using WinRT with a C++ engine, you would use C++/CX which uses hats (^).  `Cpp` is the recommended API to use for C++ game engines.    
![](../images/nuget/nuget_xbox_install_5.png)
![](../images/nuget/nuget_uwp_install_7.png)
1. After accepting the License TOS, wait until the package has been successfully added.  You should see this log in the Package Manger output window:

```
========== Finished ==========
```

### 3.	Optionally include header
* For `Microsoft.Xbox.Live.SDK.Cpp.*` based projects `#include <xsapi\services.h>` in your project's source.
* For `Microsoft.Xbox.Live.SDK.WinRT.*` based projects, no need to include any headers.   
