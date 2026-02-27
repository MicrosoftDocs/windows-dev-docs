---
title: Data Binding with WinUI and MVVM Toolkit - Step 1 - Create a class library project
description: Create a separate class library project to hold ViewModels and services for improved testability in your WinUI app.
ms.date: 10/29/2025
ms.topic: tutorial
keywords: windows 11, windows app sdk, winui, windows ui, mvvm, mvvm toolkit, dotnet
ms.localizationpriority: medium
---

# Create a class library project

To enable unit testing of your ViewModels and services, create a separate class library project. You need this project because WinUI 3 unit test projects can't directly reference WinUI app projects.

## Understanding the WinUI Class Library template

The **WinUI Class Library** project template creates a .NET managed class library (DLL) specifically designed for use with WinUI desktop applications. This template is part of the Windows App SDK and provides important capabilities that a standard .NET class library doesn't include.

### Key differences from a .NET Class Library

The WinUI Class Library template differs from a standard .NET Class Library in several important ways:

- **Windows-specific targeting**: It targets a Windows-specific framework (like `net8.0-windows10.0.19041.0`) rather than the cross-platform .NET framework, giving access to Windows APIs.
- **Windows App SDK integration**: It includes references to the `Microsoft.WindowsAppSDK` and `Microsoft.Windows.SDK.BuildTools` NuGet packages, providing access to WinUI and Windows App SDK APIs.
- **WinUI support enabled**: The project includes `<UseWinUI>true</UseWinUI>` in its configuration, enabling WinUI-specific build tasks and XAML compilation.
- **Windows runtime identifiers**: It's configured for Windows-specific runtime identifiers (win-x86, win-x64, win-arm64).

### Why use a WinUI Class Library?

Use the **WinUI Class Library** template instead of a regular **.NET Class Library** when your library needs to:

- **Reference WinUI types and controls**: The WinUI Class Library template allows you to use types from the `Microsoft.UI.Xaml` namespace and other Windows App SDK APIs in your library code.
- **Include XAML resources**: If your library contains UserControls, custom controls, or other XAML resources, you need the WinUI Class Library template to properly compile and package these resources.
- **Integrate with WinUI apps**: The template is configured to work seamlessly with WinUI desktop apps, ensuring compatibility with the Windows App SDK runtime and deployment model.
- **Support XAML markup compilation**: The template includes the necessary build tasks to compile XAML files into the library.

### When to use a regular .NET Class Library

Use a standard **.NET Class Library** project when your library:

- Contains only pure .NET code (ViewModels, models, services, utilities)
- Doesn't reference any WinUI or Windows App SDK types
- Doesn't include any XAML files or UI-related code
- Needs to be shared across different application types (not just WinUI apps)
- Targets multiple platforms (for example, .NET MAUI or ASP.NET Core) or operating systems (for example, Linux or macOS)

For a tutorial on adding a .NET Class Library to your solution, see [Extend C# console app and debug in Visual Studio](/visualstudio/get-started/csharp/tutorial-console-part-2).

For this tutorial, use the **WinUI Class Library** template because it allows you to reference WinUI types if needed in the future, and it's specifically designed to integrate with WinUI applications. While our ViewModels and services don't currently require WinUI types, using this template provides flexibility and ensures proper integration with the Windows App SDK environment.

## Create the WinUINotes.Bus project

Create a new WinUI Class Library project named `WinUINotes.Bus` to hold your ViewModels, models, and services in the same solution as your WinUI app project.

1. In Visual Studio, right-click the solution in **Solution Explorer**.
1. Select **Add** > **New Project...**.
1. Choose the **WinUI Class Library** template and select **Next**.
   
   > [!NOTE]
   > Make sure you select **WinUI Class Library**, not just **Class Library**. The WinUI Class Library template includes references to the Windows App SDK and WinUI framework.

1. Name the project `WinUINotes.Bus` and select **Create**.
1. Delete the default `Class1.cs` file.

## Add project references

Project references enable your WinUI app project to use the ViewModels and services defined in the class library project:

1. Right-click the **WinUINotes** project and select **Add** > **Project Reference...**.
1. Check the **WinUINotes.Bus** project and select **OK**.

The Bus project contains your ViewModels, models, and services, so you can test them independently of the UI layer.

> [!NOTE]
> The term "Bus" indicates a project that acts as a communication layer or intermediary. It contains the presentation logic (ViewModels), business logic (models), and services that you can share and test independently of the UI.

> [!div class="nextstepaction"]
> [Continue to step 2 - Implement MVVM with the MVVM Toolkit](mvvm-implementation.md)
