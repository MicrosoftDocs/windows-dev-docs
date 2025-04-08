---
description: Overview of MRT Core components, and how they work to load application resources (Windows App SDK)
title: Manage resources MRT Core (Windows App SDK)
ms.topic: article
ms.date: 06/21/2021
keywords: MRT, MRTCore, pri, makepri, resources, resource loading, windows 10, windows 11, windows app sdk
ms.localizationpriority: medium
---

# Manage resources with MRT Core

MRT Core is a streamlined version of the modern Windows [Resource Management System](/windows/uwp/app-resources/resource-management-system) that is distributed as part of [the Windows App SDK](../index.md).

MRT Core has both build-time and run-time features. At build time, the system creates an index of all the different variants of the resources that are packaged up with your app. This index is the Package Resource Index, or PRI, and it's also included in your app's package.

## Prerequisites

To use MRT Core APIs in the Windows App SDK:

1. Download and install the latest release of the Windows App SDK. For more information, see [Get started with WinUI](../../get-started/start-here.md).
2. Follow the instructions to [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md) or to [use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md).

To learn more about the availability of MRT Core in the Windows App SDK, see [release channels](../release-channels.md).

## Package Resource Index (PRI) file

Every app package should contain a binary index of the resources in the app. This index is created at build time and it is contained in one or more PRI files. Each PRI file contains a named collection of resources, referred to as a resource map.

A PRI file contains actual string resources. Embedded binary and file path resources are indexed directly from the project files. A package typically contains a single PRI file per language, named **resources.pri**. The **resources.pri** file at the root of each package is automatically loaded when the [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) object is instantiated.

PRI files contain only data, so they don't use the portable executable (PE) format. They are specifically designed to be data-only.

> [!NOTE]
> For .NET apps, in Windows App SDK version 0.8 and onward, the **Build Action** file property for resource files in Visual Studio is automatically set, reducing the need for manual project configuration. Version 1.0 introduced [issue 1674](https://github.com/microsoft/WindowsAppSDK/issues/1674). This is fixed in 1.1 (from the stable channel), but the fix requires .NET SDK 6.0.300 or later. If you're using a lower version of the .NET SDK, please continue to use the workaround in the 1.0 release notes.

## Access app resources with MRT Core

MRT Core provides several different ways to access your app resources.

> [!NOTE]
> In Windows App SDK 1.0 Preview 1 and later releases, MRT Core APIs are in the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace. In earlier releases, they are in the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace.

### Basic functionality with ResourceLoader

The simplest way to access your app resources programmatically is by using the [ResourceLoader](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourceloader) class. **ResourceLoader** provides you basic access to string resources from the set of resource files, referenced libraries, or other packages.

### Advanced functionality with ResourceManager

The [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) class provides additional info about resources, such as enumeration and inspection. This goes beyond what the **ResourceLoader** class provides.

A [ResourceCandidate](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecandidate) object represents a single concrete resource value and its qualifiers, such as the string "Hello World" for English, or "logo.scale-100.jpg" as a qualified image resource that's specific to the scale-100 resolution.

Resources available to an app are stored in hierarchical collections, which you can access with a [ResourceMap](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemap) object. The **ResourceManager** class provides access to the various top-level **ResourceMap** instances used by the app, which correspond to the various packages for the app. The [ResourceManager.MainResourceMap](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager.mainresourcemap) value corresponds to the resource map for the current app package, and it excludes any referenced framework packages. Each **ResourceMap** is named for the package name that is specified in the package's manifest. Within a **ResourceMap** are subtrees (see [ResourceMap.GetSubtree](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemap.getsubtree)). The subtrees typically correspond to the resource files that contains the resource.

The **ResourceManager** not only supports access to an app's string resources, it also maintains the ability to enumerate and inspect the various file resources as well. In order to avoid collisions between files and other resources that originate from within a file, indexed file paths all reside within a reserved "Files" **ResourceMap** subtree. For example, the file '\Images\logo.png' corresponds to the resource name 'Files/images/logo.png'.

### Qualify resource selection with ResourceContext

Resource candidates are chosen based on a particular [ResourceContext](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcecontext), which is a collection of resource qualifier values (language, scale, contrast, and so on). A default context uses the app's current configuration for each qualifier value, unless overridden. For example, resources such as images can be qualified for scale, which varies from one monitor to another and hence from one application view to another. For this reason, each application view has a distinct default context. Whenever you retrieve a resource candidate, you should pass in a **ResourceContext** instance to obtain the most appropriate value for a given view.

## Sample

For a sample that demonstrates how to use the MRT Core API, see the [MRT Core sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/ResourceManagement).

## See also

- [UWP Resource Management System](/windows/uwp/app-resources/resource-management-system)
- [Localize strings in your UI and app package manifest](localize-strings.md)
- [Load images and assets tailored for scale, theme, high contrast, and others](images-tailored-for-scale-theme-contrast.md)
- [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)
