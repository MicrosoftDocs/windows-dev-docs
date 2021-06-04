---
title: Project Reunion release channels and release notes
description: Learn about Project Reunion release channels and what's in the latest releases.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, project reunion 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Project Reunion release channels and release notes

Project Reunion currently provides two release channels: **stable** and **preview**. For more details about the features and latest release notes for each channel, see these articles:

- [Stable channel](stable-channel.md): This channel is supported for use by apps in production environments.
- [Preview channel](preview-channel.md): This channel includes experimental features, and is not supported for use in production environments.

## Known issues and limitations for all release channels

The following known issues and limitations apply to both stable channel and preview channel releases of Project Reunion.

#### .NET SDK references

In order to receive all of the fixes from the latest stable or preview release of Project Reunion in C# .NET 5 projects, you'll need to explicitly set your .NET SDK to the correct version.

To determine the correct version for your app, locate the `<TargetFramework>` tag in your project file. Using the Windows SDK build number that your app is targeting in the `<TargetFramework>` tag (such as 18362 or 19041), add the following item to your project file, then save your project: 

```xml
<ItemGroup>            
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.{Target Windows SDK Build Number}.16" />
    <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.{Target Windows SDK Build Number}.16" />
</ItemGroup>
```

Note this workaround is required for .NET SDK 5.0.203 and earlier, but will not be required for .NET SDK 5.0.204 or 5.0.300

#### Using the Project Reunion NuGet package in existing projects

If you want to [use the Project Reunion NuGet package in existing projects](get-started-with-project-reunion.md#use-project-reunion-in-an-existing-project), be aware of the following limitations:

- The Project Reunion NuGet package is supported for use with desktop (C# .NET 5 and C++ desktop) projects in production environments. It is available as a developer preview for UWP projects, and is not supported for use with UWP projects in production environments.
- The Project Reunion NuGet package (named **Microsoft.ProjectReunion**) contains other sub-packages (including **Microsoft.ProjectReunion.Foundation** and **Microsoft.ProjectReunion.WinUI**) that contain the implementations for components including WinUI, MRT Core, and DWriteCore. You cannot install these sub-packages individually to reference only certain components in your project. You must install the **Microsoft.ProjectReunion** package, which includes all of the components.  

#### ASTA to STA threading model

If you're migrating code from an existing UWP app to a new C# .NET 5 or C++ desktop WinUI 3 project that uses Project Reunion, be aware that the new project uses the [single-threaded apartment (STA)](/windows/win32/com/single-threaded-apartments) threading model instead of the [Application STA (ASTA)](https://devblogs.microsoft.com/oldnewthing/20210224-00/?p=104901) threading model used by UWP apps. If your code assumes the non re-entrant behavior of the ASTA threading model, your code may not behave as expected.

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Get started developing apps with Project Reunion](get-started-with-project-reunion.md)
- [Deploy apps that use Project Reunion](deploy-apps-that-use-project-reunion.md)