---
description: Features available to Windows developers when selecting an app development framework.
title: Windows app development - options and features
ms.topic: article
ms.date: 09/20/2023
keywords: windows, win32, desktop development, app development
ms.localizationpriority: medium
---

# Windows app development: options and features

## App development frameworks

The following table lists the different app development frameworks available on Windows and the features supported by each framework.

For more information about Windows app development options, see [Writing apps for Windows](index.md).

| Feature | WPF | WinUI 3 | Windows Forms | Win32 | UWP | React Native (RNW) | .NET MAUI |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **Language** | C#, Visual Basic | C#, C++ | C#, Visual Basic | C++, Rust | C#, C++, Visual Basic | JavaScript, TypeScript | C# |
| **Fluent Design** | ❌ | ✅ | ❌ | ❌ | ❌ | ✅ | ✅ |
| **.NET Runtime** | .NET & .NET Framework | .NET | .NET & .NET Framework | N/A | .NET | N/A | .NET |
| **Windows App SDK** | ✅ (limited) | ✅ (full) | ✅ (limited) | ✅ (full) | ❌ | ❌ | ❌ |
| **Receiving updates** | ✅ | ✅ | ✅ | ✅ | ✅ (security/bugfix only) | ✅ | ✅ |
| **Currently supported** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Enterprise Apps** | ✅ | ❌ | ✅ | ✅ | ❌ | ✅ | ❌ |
| **Great for touch** | ❌ | ✅ | ❌ | ❌ | ✅ | ✅ | ✅ |
| **UI type** | XAML | XAML | Code | Code | XAML | JSX | XAML/Code |
| **UI designer** | ✅ | ❌ (use [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload)) | ✅ | ❌ | ✅ | ❌ (use [Fast Refresh](https://reactnative.dev/docs/fast-refresh)) | ❌ (use [Hot Reload](/dotnet/maui/xaml/hot-reload)) |
| **Cross-platform** | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ | ✅ |
| **Xbox/HoloLens support** | ❌ | ❌ | ❌ | ✅ | ✅ | ✅ | ❌ |

Learn more about each of these options:

- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [Windows UI Library in the Windows App SDK (WinUI 3)](/windows/apps/winui/winui3/)
- [Windows Forms](/dotnet/desktop/winforms/)
- [C++ and Win32](/windows/win32/)
- [Universal Windows Platform (UWP)](/windows/uwp/)
- [React Native for Windows (RNW)](/windows/dev-environment/javascript/react-native-for-windows)
- [.NET Multi-platform App UI (.NET MAUI)](/dotnet/maui/)

## Other Windows development options

There are even more options for developing on Windows:

- [Python on Windows](/windows/python/)
- [Progressive Web Apps (PWAs)](/microsoft-edge/progressive-web-apps-chromium/)
- [Microsoft for Java developers](/java/)
- [Windows Subsystem for Linux (WSL)](/windows/wsl/)
- [PowerShell](/powershell/)
- [Android development on Windows](/windows/android/overview)

## See also

- [Writing apps for Windows](index.md)
- [Sample applications for Windows development](samples.md)
