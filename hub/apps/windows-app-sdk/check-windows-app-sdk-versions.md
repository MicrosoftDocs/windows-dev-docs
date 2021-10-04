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
get-appxpackage *reunion*
```

You should see output similar to the following, which includes the `x64` and `x86` architecture of the [Framework package](deployment-architecture.md#framework-packages-for-packaged-and-unpackaged-apps) and a single [Main package](deployment-architecture.md#main-package) and [Dynamic Dependency Lifetime Manager (DDLM) package](deployment-architecture.md#dynamic-dependency-lifetime-manager-ddlm), depending on your computer.  

```console
Name              : Microsoft.ProjectReunion.0.8-preview
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X86
ResourceId        :
Version           : 8000.144.525.0
PackageFullName   : Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x86__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x86__8wekyb3d8bbwe
IsFramework       : True
PackageFamilyName : Microsoft.ProjectReunion.0.8-preview_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : Microsoft.ProjectReunion.0.8-preview
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 8000.144.525.0
PackageFullName   : Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe
IsFramework       : True
PackageFamilyName : Microsoft.ProjectReunion.0.8-preview_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
IsPartiallyStaged : False
SignatureKind     : Store
Status            : Ok

Name              : Microsoft.ProjectReunion.Main.0.8-preview
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 8000.144.525.0
PackageFullName   : Microsoft.ProjectReunion.Main.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program
                    Files\WindowsApps\Microsoft.ProjectReunion.Main.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.ProjectReunion.Main.0.8-preview_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Developer
Status            : Ok

Name              : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x8-p
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X86
ResourceId        :
Version           : 8000.144.525.0
PackageFullName   : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x8-p_8000.144.525.0_x86__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.ProjectReunion.DDLM.8000.144.525.0-x8-p_8000.144.525.0_x86__
                    8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x8-p_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x86__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Developer
Status            : Ok

Name              : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x6-p
Publisher         : CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture      : X64
ResourceId        :
Version           : 8000.144.525.0
PackageFullName   : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x6-p_8000.144.525.0_x64__8wekyb3d8bbwe
InstallLocation   : C:\Program Files\WindowsApps\Microsoft.ProjectReunion.DDLM.8000.144.525.0-x6-p_8000.144.525.0_x64__
                    8wekyb3d8bbwe
IsFramework       : False
PackageFamilyName : Microsoft.ProjectReunion.DDLM.8000.144.525.0-x6-p_8wekyb3d8bbwe
PublisherId       : 8wekyb3d8bbwe
IsResourcePackage : False
IsBundle          : False
IsDevelopmentMode : False
NonRemovable      : False
Dependencies      : {Microsoft.ProjectReunion.0.8-preview_8000.144.525.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged : False
SignatureKind     : Developer
Status            : Ok
```

## Related topics

- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)
- [Runtime architecture and deployment scenarios](deployment-architecture.md)
- [Remove outdated Windows App SDK runtime versions from your development computer](remove-windows-app-sdk-versions.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with the Windows App SDK](get-started.md)
