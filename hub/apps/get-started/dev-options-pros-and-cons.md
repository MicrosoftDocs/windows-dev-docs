---
description: Review the pros and cons of the different app development options available on Windows.
title: Windows app development options - pros and cons
ms.topic: article
ms.date: 09/20/2023
keywords: windows, win32, desktop development, app development
ms.localizationpriority: medium
---

# Windows app development options: pros and cons

## App development frameworks

The following table lists the different app development frameworks available on Windows and the features supported by each framework.

For more information about Windows app development options, see [Writing apps for Windows](index.md).

| Feature | WPF | WinUI 3 | Windows Forms | Win32 | UWP | React Native | .NET MAUI |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **Supported languages** | C#, Visual Basic | C#, C++ | C#, Visual Basic | C++, Rust | C#, C++, Visual Basic | JavaScript, TypeScript | C# |
| **Fluent Design System** | No | Yes | No | No | No | Yes | Yes |
| **.NET Runtime** | .NET & .NET Framework | .NET | .NET & .NET Framework | N/A | .NET | N/A | .NET |
| **Windows App SDK** | Yes (limited) | Yes (full) | Yes (limited) | Yes (full) | No | No | No |
| **Receiving updates** | Yes | Yes | Yes | Yes | Yes (security/bugfix only) | Yes | Yes |
| **Active development** | No | Yes | No | No | No | Yes | Yes |
| **Currently supported** | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| **Enterprise Apps** | Yes | No | Yes | Yes | No | Yes | No |
| **Recommended for touch** | No | Yes | No | No | Yes | Yes | Yes |
| **UI type** | XAML | XAML | Code | Code | XAML | HTML/CSS | XAML/Code |
| **UI designer** | Yes | No (use Hot Reload) | Yes | No | Yes | No | No (use Hot Reload) |
| **Cross-platform** | No | No | No | No | No | Yes | Yes |
| **Xbox/HoloLens support** | No | No | No | Yes | Yes | No | No |

Learn more about each of these options:

- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [Windows UI Library in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)
- [Windows Forms](/dotnet/desktop/winforms/)
- [C++ and Win32](/windows/win32/)
- [Universal Windows Platform (UWP)](/windows/uwp/)
- [React Native for Windows](/windows/dev-environment/javascript/react-native-for-windows)
- [.NET Multi-platform App UI (.NET MAUI)](/dotnet/maui/)

## Other Windows development options

There are even more options for developers who want to develop on Windows:

- [Python on Windows](/windows/python/)
- [Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/)
- [Microsoft for Java developers](/java/)
- [Windows Subsystem for Linux (WSL)](/windows/wsl/)
- [PowerShell](/powershell/)
- [Android development on Windows](/windows/android/overview)

## See also

- [Writing apps for Windows](index.md)
- [Sample applications for Windows development](samples.md)
