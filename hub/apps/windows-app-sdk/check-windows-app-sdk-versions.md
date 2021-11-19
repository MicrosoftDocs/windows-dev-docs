---
title: Check for installed versions of the Windows App SDK runtime
description: This article provides instructions for verifying the version of the Windows App SDK runtime installed on your development computer. 
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Check for installed versions of the Windows App SDK runtime

To check which versions of the Windows App SDK runtime are installed on your development computer, open a **PowerShell** window and run this command.

```Powershell
# For 1.0 Stable and Preview releases 
get-appxpackage *appruntime*

# For 1.0 Experimental
get-appxpackage *WindowsAppSDK* 

# For version 0.8
get-appxpackage *reunion*

```

You should see output similar to the following, which may include the `x64` and `x86` architecture of the [Framework package](deployment-architecture.md#framework-package), [Dynamic Dependency Lifetime Manager (DDLM) package](deployment-architecture.md#dynamic-dependency-lifetime-manager-ddlm), [Main package](deployment-architecture.md#main-package), and [Singleton package](deployment-architecture.md#singleton-package), depending on your computer and the Windows App SDK version.  

```console
Name              : Microsoft.WindowsAppRuntime.1.0
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe
IsFramework       : True
PackageFamilyName : Microsoft.WindowsAppRuntime.1.0_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : Microsoft.WindowsAppRuntime.1.0
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X86
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x86__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x86__8wekyb3d8bbwe
IsFramework       : True
PackageFamilyName : Microsoft.WindowsAppRuntime.1.0_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : MicrosoftCorporationII.WindowsAppRuntime.Main.1.0
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : MicrosoftCorporationII.WindowsAppRuntime.Main.1.0_0.318.928.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program
                    Files\WindowsApps\MicrosoftCorporationII.WindowsAppRuntime.Main.1.0_0.318.928.0_x64__8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : MicrosoftCorporationII.WindowsAppRuntime.Main.1.0_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : Microsoft.WindowsAppRuntime.Singleton
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : Microsoft.WindowsAppRuntime.Singleton_0.318.928.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.WindowsAppRuntime.Singleton_0.318.928.0_x64__8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.WindowsAppRuntime.Singleton_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6_0.318.928.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program
                    Files\WindowsApps\Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6_0.318.928.0_x64__8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Developer
Status            : Ok

Name              : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X86
ResourceId        :
Version           : 0.318.928.0
PackageFullName   : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8_0.318.928.0_x86__8wekyb3d8bbwe
InstallLocation   : C:\Program
                    Files\WindowsApps\Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8_0.318.928.0_x86__8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x86__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Developer
Status            : Ok
```

## Related topics

- [Runtime architecture](deployment-architecture.md)
- [Windows App SDK deployment guide for unpackaged apps](deploy-unpackaged-apps.md)
- [Windows App SDK deployment guide for packaged apps](deploy-packaged-apps.md)
- [Remove outdated Windows App SDK runtime versions from your development computer](remove-windows-app-sdk-versions.md)
