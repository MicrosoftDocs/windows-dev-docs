---
description: Features available to Windows developers when selecting an app development framework.
title: Windows app development - options and features
ms.topic: article
ms.date: 09/20/2023
ms.author: aashcraft
author: alvinashcraft
keywords: windows, win32, desktop development, app development
ms.localizationpriority: medium
---

# Windows app development: options and features

## App development frameworks

There is a wide range of options for developing applications for Windows. The best option for you depends on your app requirements, your existing code, and your familiarity with the technology. The following table lists the most popular app development frameworks available on Windows and the features supported by each framework.

To read more about each of these Windows app development options, see [Writing apps for Windows](index.md).

| Feature | .NET MAUI | React Native (RNW) | UWP XAML (Windows.UI.Xaml) | Win32 (MFC) | Windows Forms | WinUI 3 | WPF |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **Language** | C# | JavaScript, TypeScript | C#, C++, Visual Basic | C++, Rust | C#, Visual Basic | C#, C++ | C#, Visual Basic |
| **UI language** | XAML/Code | JSX | XAML | Code | Code | XAML | XAML |
| **UI designer**<br/>(drag & drop) | ❌ | ❌ | ✅ | ❌ | ✅ | ❌ | ✅ |
| **UI debugging** | [Hot Reload](/dotnet/maui/xaml/hot-reload) | [Fast Refresh](https://reactnative.dev/docs/fast-refresh) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) | - | [Hot Reload](/visualstudio/debugger/hot-reload) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) | [Hot Reload](/visualstudio/xaml-tools/xaml-hot-reload) |
| **Fluent Design** | ✅ | ✅ | ✅ (via [WinUI 2](/windows/apps/winui/winui2)) | ❌ | ❌ | ✅ | ❌ |
| **.NET** | .NET | N/A | .NET Core & .NET Native | N/A | .NET & .NET Framework | .NET | .NET & .NET Framework |
| **Windows App SDK** | ✅ ([more info](/dotnet/maui/platform-integration/invoke-platform-code)) | ✅ ([more info](https://techcommunity.microsoft.com/t5/modern-work-app-consult-blog/getting-started-with-react-native-for-windows/ba-p/912093)) | ❌ | ✅ | ✅ ([more info](../windows-app-sdk/winforms-plus-winappsdk.md)) | ✅ | ✅ ([more info](../windows-app-sdk/wpf-plus-winappsdk.md)) |
| **Great for touch** | ✅ | ✅ | ✅ | ❌ | ❌ | ✅ | ❌ |
| **Cross-platform** | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Xbox/HoloLens apps** | ❌ | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| **Sandboxing (AppContainer)** | ❌ | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| **Currently supported** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Receiving updates** | ✅ | ✅ | ✅ (security & bugfix) | ✅ | ✅ | ✅ | ✅ |
| **Roadmap** | [GitHub](https://github.com/dotnet/maui/wiki/Roadmap) | [GitHub](https://aka.ms/rnw-roadmap) | n/a | n/a | [GitHub](https://github.com/dotnet/winforms/blob/main/docs/roadmap.md) | [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/docs/roadmap.md) | [GitHub](https://github.com/dotnet/wpf/blob/main/roadmap.md) |

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
