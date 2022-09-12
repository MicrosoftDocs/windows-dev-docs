---
title: Sample applications for Windows development
description: Use these Github repositories to learn about Windows development. Sample apps demonstrate Windows features, API usage patterns, and end-to-end scenarios.
ms.topic: how-to
ms.date: 09/06/2022
keywords: windows, win32, desktop development
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
ms.collection: windows11
ms.custom: kr2b-contr-experiment
---

# Sample applications for Windows development

This page is targeted at users who are looking for Windows development samples that demonstrate specific tasks, features, and API usage patterns. See our [Samples Browser](/samples/browse/) for a more comprehensive overview of Windows development samples. Most of the samples identified in this document contain Solution (`.sln`) files that can be opened in Visual Studio. Refer to each resource's `Readme` for additional instructions.


## Windows App SDK / WinUI 3 samples

#### Samples by repository

|                                      Samples repository                                      |                                                                                  Description                                                                                  |
|:--------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| [Input & Composition Gallery sample](https://github.com/microsoft/WindowsCompositionSamples) |                                             Showcases a variety of `Microsoft.UI.Composition` and `Microsoft.UI.Input` API calls.                                             |
|           [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery/tree/main)            | Showcases various WinUI 3 controls and how to effectively use them. See [WinUI 3 Gallery in the Store](https://apps.microsoft.com/store/detail/winui-3-gallery/9P3JFPWWDZRC). |
|    [microsoft/WindowsAppSDK-Samples](https://github.com/microsoft/WindowsAppSDK-Samples)     |                                                                    Hosts samples for the Windows App SDK.                                                                     |


#### Samples by scenario

|                                                                              Scenario                                                                              |                                                                  Sample                                                                   |
|:------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------:|
|            **Handle app activation kinds**. App activation APIs determine how your app handles activation kinds like `Launch`, `File`, and `Protocol`.             |             [Activation sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/AppLifecycle/Activation)             |
| **Determine application instancing behavior**. App instancing APIs determine whether or not users can run multiple instances of your application at the same time. |           [App Instancing sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/Instancing)           |
|                 **Use state notifications**. These APIs allow you to detect when the user's device enters specific states, such as low power mode.                 |    [Power Notifications sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/StateNotifications)     |
|                                   **Programatically restart your app**. Restart APIs let you programmatically restart your app.                                    |                [Restart sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/Restart)                |
|           **Tailor your app to the user and device settings**. Resource management APIs allow you to adapt resource utilization to specific situations.            |           [Resource Management sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/ResourceManagement)           |
|                               **Use the DeploymentManager** to ensure that Windows application framework components are up to date.                                |            [Deployment Manager sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/DeploymentManager)            |
|                                              **Launch the Windows App SDK installer** without using a console window.                                              |                        [Installer](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Installer)                        |
|                                                               **Render text** using DWriteCore APIs.                                                               |            [TextRendering with DWriteCore](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/TextRendering)            |
|                                    **Deploy a non-MSIX packaged app**, an alternative to packaged app deployment architecture.                                     |                   [Unpackaged sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Unpackaged)                    |
|             **Dynamically load MSIX Framework packages** (like DirectX) instead of including static dependencies directly within your distributed app.             | [Dynamic Dependencies sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/DynamicDependenciesSample/DynamicDependencies) |
|                                                              **Add push notifications to your app**.                                                               |           [Push Notifications sample](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Notifications/Push)            |
|                                                              **Add toast notifications to your app**.                                                              |            [App Notifications sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/App)             |
|                                                           **Add custom C#/WinRT controls to your app**.                                                            |               [Custom Controls sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/CustomControls)               |
|                             **Add windowing support to your app** in order to create/hide new windows, customize titlebars, and more.                              |                    [Windowing sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing)                     |





## UWP / WinUI 2 samples

#### Samples by repository

|                                           Samples repository                                           |                                                                                  Description                                                                                  |
|:------------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|     [microsoft/Windows-universal-samples](https://github.com/microsoft/Windows-universal-samples)      |                                                                               Main UWP Samples                                                                                |
|    [microsoft/DesktopBridgeToUWP-Samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)     |                                                           Samples for framework conversion (e.g. Win32 -> UWP, etc)                                                           |
|               [WinUI 2 Gallery](https://github.com/microsoft/WinUI-Gallery/tree/winui2)                | Showcases various WinUI 2 controls and how to effectively use them. See [WinUI 2 Gallery in the Store](https://apps.microsoft.com/store/detail/winui-2-gallery/9MSVH128X2ZT). |
| [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples) |                                                                Demonstrates WinRT API usage patterns for UWP.                                                                 |


#### Samples by scenario

|                                                                         Scenario                                                                          |                                                           Sample                                                            |
|:---------------------------------------------------------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------------------:|
|                            **Playback a 360 degree video** in order to give an immersive/explorative experience to your users.                            |   [360 Degree Video Playback](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/360VideoPlayback)    |
|                              **Use advanced casting** to allow your users to share their content on other devices like TVs.                               |        [Advanced Casting](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AdvancedCasting)         |
|                        **Create Windows-styled designs** via the `AnimationMetrics` API to keep your app consistent with Windows.                         |       [Animation Metrics](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AnimationMetrics)        |
|                                  **Create new windows** and control their positioning, size, or Picture-in-Picture mode.                                  |              [App Window](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AppWindow)               |
|     **Store user-unique data** such as session states, preferences and other settings easily allowing it to be backed up to the cloud inside Windows.     |        [Application Data](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/ApplicationData)         |
|                  **Store resources** such as images, and strings, away from your app code allowing you to easily edit them at any time.                   |   [Application Resources](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/ApplicationResources)    |
|                                               **Create/manage appointments** with Calendar app integration.                                               |            [Appointments](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Appointments)            |
|                                **Use association launching** to associate your app with specific file types and protocols.                                |   [Association Launching](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AssociationLaunching)    |
|         **Use the audio APIs** to load audio files, play audio, capture audio from other apps, apply effects to audio, and create custom effects.         |          [Audio Creation](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AudioCreation)           |
|                         **Use Background Tasks** to run specific events (like updating your app) whenever your app isn't running.                         |         [Background Tasks](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BackgroundTask)         |
|                          **Use the Suspension Manager** to save/restore your app's state when the app is suspended or shut down.                          |        [Basic Suspension](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BasicSuspension)         |
| **Use the MediaCapture APIs** to stop/start camera previews, take pictures/videos, handle rotation, and adjust elements based on the angle of the camera. |      [Basic Camera Sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/CameraStarterKit)       |
|                                      **Process individual camera frames** or monitor when new cameras are connected.                                      |           [Camera Frames](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames)            |
|                                      **Disable and Enable screen capture** to protect sensitive in-app information.                                       | [Disabling Screen Capture](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/DisablingScreenCapture) |
|                               **Use the Storage API** to access files, read file properties, write data to files, and more.                               |             [File Access](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/FileAccess)              |
|                            **Create file and directory pickers** so your app can prompt users to select files and directories.                            |             [File Picker](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/FilePicker)              |
|                                              **Serialize and deserialize JSON** objects returned from APIs.                                               |                    [JSON](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Json)                    |
|                                                 **Display and modify PDFs** via the the `PDF` namespace.                                                  |            [PDF Document](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/PdfDocument)             |
|                                           **Customize the title bar** properties, colors, style, and controls.                                            |               [Title Bar](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/TitleBar)                |
|                          **Use WebSockets** to communicate with another app (or a browser page), avoiding the overhead of HTTP.                           |               [WebSocket](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/WebSocket)               |





## .NET MAUI samples

#### Samples by repository

|                      Samples repository                       |                                                                                                                                                              Description                                                                                                                                                               |
|:-------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| [dotnet/maui-samples](https://github.com/dotnet/maui-samples) | .NET MAUI is a cross-platform framework for creating mobile and desktop apps with C# and XAML. Using .NET MAUI, you can develop apps that can run on Android, iOS, iPadOS, macOS, and Windows from a single shared codebase. These are also available in the [Samples browser](/samples/browse/?expanded=dotnet&products=dotnet-maui). |


#### Samples by scenario

|                                                                                    Scenario                                                                                    |                                              Sample Name/Link                                               |
|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------:|
|                                                                 **Use animations** to bring your app to life.                                                                  |                [Animations](https://github.com/dotnet/maui-samples/tree/main/6.0/Animations)                |
|                                **Add functionality without subclassing** using behaviors, instead attaching behavior classes to your controls.                                 |        [Behaviors](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/BehaviorsDemos)        |
|                                   **Define the visual structure** of `ContentView` derived custom controls, and `ContentPage` derived pages.                                   | [Control Templates](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/ControlTemplateDemos) |
|                                        **Bind objects** to the actual UI elements, responsively updating one another when one changes.                                         |     [Data Binding](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/DataBindingDemos)      |
| **Use a .NET Shell app** to reduce complexity, duplicate code, and integrate existing Windows functionality (such as URI navigation and integrated search bars) into your app. |            [Shell App](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/Shell)             |
|                                   **Use triggers** to selectively update, hide, or display specific controls when an event or data changes.                                    |         [Triggers](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/TriggersDemos)         |




## More Windows development samples repositories

| Samples repository                                                                       | Description                                                                                                                                                                                                                                            |
|------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Template Studio](https://github.com/microsoft/TemplateStudio#template-studio)           | Accelerate the creation of apps using a wizard-based UI.                                                                                                                                                                                               |
| [App Model Samples](https://github.com/Microsoft/AppModelSamples)                        | Contains sample apps that demonstrate the core application activation and lifecycle management infrastructure of various platforms such as the Universal Windows Platform (UWP), Windows Forms (WinForms), and console.                                |
| [Windows classic samples](https://github.com/microsoft/Windows-classic-samples)          | Demonstrates a wide range of desktop app scenarios, including Win32, Windows Runtime (WinRT), and .NET.                                                                                                                                                |
| [Desktop Bridge to UWP samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples) | Demonstrates the Desktop Conversion Extensions for converting classic desktop apps (such as Win32, Windows Presentation Foundation, and Windows Forms) and games to UWP apps and games.                                                                |
| [DirectX 12 graphics samples](https://github.com/Microsoft/DirectX-Graphics-Samples)     | Demonstrates how to build graphics-intensive apps on Windows using DirectX 12.                                                                                                                                                                         |
| [Windows Composition samples](https://github.com/microsoft/WindowsCompositionSamples)    | Demonstrates how to use types from the `Windows.UI.Xaml` and `Windows.UI.Composition` namespaces to make beautiful UWP apps.                                                                                                                           |
| [Windows samples for IoT](https://github.com/Microsoft/Windows-iotcore-samples)          | Sample apps to help you get started with developing for Windows on Devices.                                                                                                                                                                            |
| [Windows Community Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit)  | A collection of helper functions, custom controls, and app services. It simplifies and demonstrates common developer tasks when building apps for Windows.                                                                                             |
| [Windows task snippets](https://github.com/Microsoft/Windows-task-snippets)              | Ready-to-use snippets of code that accomplish small but useful tasks of interest to UWP app developers. These snippets show simple solutions to common problems, and simple recipes to help you implement new app features.                            |
| [Win2D](https://github.com/Microsoft/win2d)                                              | Win2D is an easy-to-use Windows Runtime (WinRT) API for immediate-mode 2D graphics rendering with GPU acceleration. It's available to C# and C++ developers, and it utilizes the power of Direct2D, integrating seamlessly with XAML and `CoreWindow`. |



## Next steps

- [Best Practices](best-practices.md)
- [Windows Developer FAQ](windows-developer-faq.yml)
