---
title: Author Windows Runtime components with C#/WinRT
description: Overview of authoring Windows Runtime Components with C#/WinRT 
ms.date: 03/15/2022
ms.topic: article
ms.localizationpriority: medium
---

# Author Windows Runtime components with C#/WinRT

> [!NOTE]
> Authoring Windows Runtime components with C#/WinRT is supported on .NET 6 and later.

The C#/WinRT NuGet package provides support for authoring your own Windows Runtime types and components in C#, and consuming them from any Windows Runtime-compatible language such as [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) or [Rust](/windows/dev-environment/rust/rust-for-windows). C#/WinRT authoring and hosting support requires .NET 6 and Visual Studio 2022, and it is intended to support desktop application scenarios including the [Windows App SDK](/windows/apps/windows-app-sdk/), and [WinUI3](/windows/apps/winui/winui3/).

For a walkthrough showing how to author a Windows Runtime component with .NET 6, and how to consume it from a C++/WinRT console application, see [Walkthrough: Create a C#/WinRT component and consume it from C++/WinRT](./create-windows-runtime-component-cswinrt.md).

For more details, and to search for or file any issues, refer to the [C#/WinRT Github repo](https://github.com/microsoft/CsWinRT) and [Authoring C#/WinRT Components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md).

## Declaring types in Windows Runtime components

While authoring your Windows Runtime component, follow the guidelines and type restrictions outlined in the existing UWP documentation about Windows Runtime components (see [Windows Runtime components with C# and Visual Basic](/windows/uwp/winrt-components/creating-windows-runtime-components-in-csharp-and-visual-basic)). The component can for the most part be implemented like any other C# library. However, there are restrictions on the public types in the component that will be exposed to the Windows Runtime and declared in the generated `.winmd` for others to consume. 

Externally, you can expose only Windows Runtime types for parameters and return values. You can use built-in C# types as part of the public surface of the component as long as there is a mapping from the .NET type to WinRT (see [.NET mappings of WinRT types in C#/WinRT](/windows/apps/develop/platform/csharp-winrt/net-mappings-of-winrt-types)), and they will appear to users of the component as the corresponding Windows Runtime types. Windows Runtime types from other Windows Runtime components and the Windows SDK can also be used as part of the public implementation of the component, such as `in` parameters, return types, and class inheritance.

> [!NOTE]
> There are some Windows Runtime types that are mapped to .NET types (see [.NET mappings of WinRT types in C#/WinRT](/windows/apps/develop/platform/csharp-winrt/net-mappings-of-winrt-types)). These .NET types can be used in the public interface of your Windows Runtime component, and they will appear to users of the component as the corresponding Windows Runtime types.

## Related topics

* [Authoring C#/WinRT Components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md)
* [Walkthrough: Create a C#/WinRT component and consume it from C++/WinRT](./create-windows-runtime-component-cswinrt.md)
* [Diagnose C#/WinRT component errors](./authoring-diagnostics.md)
* [.NET mappings of WinRT types in C#/WinRT](/windows/apps/develop/platform/csharp-winrt/net-mappings-of-winrt-types)
