---
title: Sample applications for Windows development
description: Use these Github repositories to learn about Windows development. Sample apps demonstrate Windows features, API usage patterns, and end-to-end scenarios.
ms.topic: how-to
ms.date: 09/06/2022
keywords: windows, win32, desktop development
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.collection: windows11
ms.custom: kr2b-contr-experiment
---

# Sample applications for Windows development

This page is targeted at users who are looking for Windows development samples that demonstrate specific tasks, features, and API usage patterns. 

The samples in this document demonstrate features from Windows App SDK / WinUI 3, UWP / WinUI 2, .NET MAUI, and more. See our [Samples Browser](/samples/browse/) for a more extensive catalog of samples. 

Most of the samples identified in this document contain Solution (`.sln`) files that can be opened in Visual Studio. Refer to each resource's `Readme` for additional instructions.


## Windows App SDK / WinUI 3 samples

#### Samples repositories

| Samples repository                                                                    | Description                                                                                                                                                                   |
|---------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [microsoft/WindowsAppSDK-Samples](https://github.com/microsoft/WindowsAppSDK-Samples) | This is the main Windows App SDK samples repository.                                                                                                                          |
| [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery/tree/main)               | Showcases various WinUI 3 controls and how to effectively use them. See [WinUI 3 Gallery in the Store](https://apps.microsoft.com/store/detail/winui-3-gallery/9P3JFPWWDZRC). |
| [Input & Composition Gallery](https://github.com/microsoft/WindowsCompositionSamples) | Showcases a variety of `Microsoft.UI.Composition` and `Microsoft.UI.Input` API calls.                                                                                         |


#### Samples that demonstrate specific tasks

| Sample                                                                                                                             | Task                                                                                                                                                                                                                                                                                        |
|------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Activation](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/AppLifecycle/Activation)                         | **Handle app activation kinds**. [App activiation APIs](../windows-app-sdk/applifecycle/applifecycle-rich-activation.md) control the way that your app handles activation kinds like `Launch`, `File`, and `Protocol`.                                                              |
| [App Instancing](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/Instancing)                     | **Determine application instancing behavior**. [App instancing APIs](../windows-app-sdk/applifecycle/applifecycle-instancing.md) control whether or not users can run multiple instances of your application at the same time.                                                      |
| [Power Notifications](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/StateNotifications)        | **Use state notifications**. [State notifications](../windows-app-sdk/applifecycle/applifecycle-power.md) allow you to detect when the user's device enters specific states, such as low power mode.                                                                                |
| [Restart](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/AppLifecycle/Restart)                               | **Programatically restart your app**. This sample makes use of the Windows App SDK [Restart APIs](../windows-app-sdk/applifecycle/applifecycle-restart.md).                                                                                                                         |
| [Resource Management](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/ResourceManagement)                     | **Tailor your app to the user and device settings**. [MRT Core resource management APIs](../windows-app-sdk/mrtcore/mrtcore-overview.md) allow you to adapt resource utilization to specific situations.                                                                            |
| [Deployment Manager](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/DeploymentManager)                       | **[Call the Deployment API](../windows-app-sdk/deploy-packaged-apps.md#call-the-deployment-api)** to ensure that Windows application framework components are up to date.                                                                                                            |
| [Installer](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Installer)                                        | **Launch the Windows App SDK installer** without using a console window.                                                                                                                                                                                                                    |
| [TextRendering with DWriteCore](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/TextRendering)                | **Render text** using DWriteCore APIs.                                                                                                                                                                                                                                                      |
| [Unpackaged](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Unpackaged)                                      | **[Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../windows-app-sdk/deploy-unpackaged-apps.md)**, an alternative to packaged app deployment architecture.                                                                                                                                         |
| [Dynamic Dependencies](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/DynamicDependenciesSample/DynamicDependencies) | Demonstrates the techniques shown in [Use the dynamic dependency API to reference MSIX packages at run time](../desktop/modernize/framework-packages/use-the-dynamic-dependency-api.md). |
| [Push Notifications](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Notifications/Push)                      | **Add push notifications to your app**. [Push Notifications](../windows-app-sdk/notifications/push-notifications/push-quickstart.md) can be used to send device-native app notifications to users.                                                                                  |
| [App Notifications](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/App)                        | **Add toast notifications to your app**. See [App Notifications](../windows-app-sdk/notifications/app-notifications/app-notifications-quickstart.md?tabs=cs) to learn more.                                                                                                         |
| [Custom Controls](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/CustomControls)                             | **Add custom C#/WinRT controls to your app** using the patterns demonstrated in [Author Windows Runtime components with C#/WinRT](../develop/platform/csharp-winrt/authoring.md).                                                                                                   |
| [Windowing](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Windowing)                                        | **Add windowing support to your app** to create/hide new windows, customize titlebars, and more. See [Manage App Windows](../windows-app-sdk/windowing/windowing-overview.md) to learn more.                                                                                        |





## UWP / WinUI 2 samples

#### Samples repositories

| Samples repository                                                                                     | Description                                                                                                                                                                   |
|--------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [microsoft/Windows-universal-samples](https://github.com/microsoft/Windows-universal-samples)          | This is the main UWP samples repository.                                                                                                                                      |
| [microsoft/DesktopBridgeToUWP-Samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)        | Samples for framework conversion (e.g. Win32 -> UWP, etc).                                                                                                                    |
| [WinUI 2 Gallery](https://github.com/microsoft/WinUI-Gallery/tree/winui2)                              | Showcases various WinUI 2 controls and how to effectively use them. See [WinUI 2 Gallery in the Store](https://apps.microsoft.com/store/detail/winui-2-gallery/9MSVH128X2ZT). |
| [Universal Windows Platform (UWP) app samples](https://github.com/microsoft/Windows-universal-samples) | Demonstrates WinRT API usage patterns for UWP.                                                                                                                                |


#### Samples that demonstrate specific tasks

| Sample                                                                                                                      | Task                                                                                                                                                      |
|-----------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| [360 Degree Video Playback](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/360VideoPlayback)      | **Play a 360-degree video** to give an immersive/explorative experience to your users.                                                                    |
| [Advanced Casting](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AdvancedCasting)                | **Use advanced casting** to allow your users to share their content on other devices like TVs.                                                            |
| [Animation Metrics](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AnimationMetrics)              | **Create Windows-styled designs** via the `AnimationMetrics` API to keep your app consistent with Windows.                                                |
| [App Window](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AppWindow)                            | **Create new windows** and control their positioning, size, or Picture-in-Picture mode.                                                                   |
| [Application Data](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/ApplicationData)                | **Store user-unique data** such as session states, preferences and other settings easily allowing it to be backed up to the cloud inside Windows.         |
| [Application Resources](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/ApplicationResources)      | **Store resources** such as images, and strings, away from your app code allowing you to easily edit them at any time.                                    |
| [Appointments](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Appointments)                       | **Create/manage appointments** with Calendar app integration.                                                                                             |
| [Association Launching](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AssociationLaunching)      | **Use association launching** to associate your app with specific file types and protocols.                                                               |
| [Audio Creation](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/AudioCreation)                    | **Use the audio APIs** to load audio files, play audio, capture audio from other apps, apply effects to audio, and create custom effects.                 |
| [Background Tasks](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BackgroundTask)                 | **Use Background Tasks** to run specific events (like updating your app) whenever your app isn't running.                                                 |
| [Basic Suspension](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BasicSuspension)                | **Use the Suspension Manager** to save/restore your app's state when the app is suspended or shut down.                                                   |
| [Camera Starter Kit](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/CameraStarterKit)             | **Use the MediaCapture APIs** to stop/start camera previews, take pictures/videos, handle rotation, and adjust elements based on the angle of the camera. |
| [Camera Frames](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/CameraFrames)                      | **Process individual camera frames** or monitor when new cameras are connected.                                                                           |
| [Disabling Screen Capture](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/DisablingScreenCapture) | **Disable and Enable screen capture** to protect sensitive in-app information.                                                                            |
| [File Access](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/FileAccess)                          | **Use the Storage API** to access files, read file properties, write data to files, and more.                                                             |
| [File Picker](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/FilePicker)                          | **Create file and directory pickers** so your app can prompt users to select files and directories.                                                       |
| [JSON](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/Json)                                       | **Serialize and deserialize JSON** objects returned from APIs.                                                                                            |
| [PDF Document](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/PdfDocument)                        | **Display and modify PDFs** via the `PDF` namespace.                                                                                                  |
| [Title Bar](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/TitleBar)                              | **Customize the title bar** properties, colors, style, and controls.                                                                                      |
| [WebSocket](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/WebSocket)                             | **Use WebSockets** to communicate with another app (or a browser page), avoiding the overhead of HTTP.                                                    |





## .NET MAUI samples

#### Samples repositories

| Samples repository                                            | Description                                                                                                                                                                                                                                                                                                                            |
|---------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [dotnet/maui-samples](https://github.com/dotnet/maui-samples) | .NET MAUI is a cross-platform framework for creating mobile and desktop apps with C# and XAML. Using .NET MAUI, you can develop apps that can run on Android, iOS, iPadOS, macOS, and Windows from a single shared codebase. These are also available in the [Samples browser](/samples/browse/?expanded=dotnet&products=dotnet-maui). |
| [.NET Podcast app](https://github.com/microsoft/dotnet-podcasts) | The .NET Podcast app is a sample application showcasing .NET 6, ASP.NET Core, Blazor, .NET MAUI, Azure Container Apps, Orleans, and more. |

#### Samples that demonstrate specific tasks

| Sample Name/Link                                                                                            | Task                                                                                                                                                                       |
|-------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Animations](https://github.com/dotnet/maui-samples/tree/main/6.0/Animations)                               | **Use animations** to bring your app to life.                                                                                                                              |
| [Behaviors](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/BehaviorsDemos)               | **Add functionality without subclassing** using behaviors, instead attaching behavior classes to your controls.                                                            |
| [Control Templates](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/ControlTemplateDemos) | **Define the visual structure** of `ContentView` derived custom controls, and `ContentPage` derived pages.                                                                 |
| [Data Binding](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/DataBindingDemos)          | **Bind objects** to the actual UI elements, responsively updating one another when one changes.                                                                            |
| [Shell App](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/Shell)                        | **Use a .NET Shell app** to reduce complexity, reuse code, and integrate existing Windows functionality (such as URI navigation and integrated search bars) into your app. |
| [Triggers](https://github.com/dotnet/maui-samples/tree/main/6.0/Fundamentals/TriggersDemos)                 | **Use triggers** to selectively update, hide, or display specific controls when an event or data changes.                                                                  |




## More Windows development samples repositories

| Samples repository                                                                       | Description                                                                                                                                                                                                                                            |
|------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Template Studio](https://github.com/microsoft/TemplateStudio#template-studio)           | Accelerate the creation of apps using a wizard-based UI.                                                                                                                                                                                               |
| [App Model Samples](https://github.com/Microsoft/AppModelSamples)                        | Contains sample apps that demonstrate the core application activation and lifecycle management infrastructure of various platforms such as the Universal Windows Platform (UWP), Windows Forms (WinForms), and console.                                |
| [Windows classic samples](https://github.com/microsoft/Windows-classic-samples)          | Demonstrates a wide range of desktop app scenarios, including Win32, Windows Runtime (WinRT), and .NET.                                                                                                                                                |
| [Desktop Bridge to UWP samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples) | Demonstrates the Desktop Conversion Extensions for converting desktop apps (such as Win32, Windows Presentation Foundation, and Windows Forms) and games to UWP apps and games.                                                                |
| [DirectX 12 graphics samples](https://github.com/Microsoft/DirectX-Graphics-Samples)     | Demonstrates how to build graphics-intensive apps on Windows using DirectX 12.                                                                                                                                                                         |
| [Windows Composition samples](https://github.com/microsoft/WindowsCompositionSamples)    | Demonstrates how to use types from the `Windows.UI.Xaml` and `Windows.UI.Composition` namespaces to make beautiful UWP apps.                                                                                                                           |
| [Windows samples for IoT](https://github.com/Microsoft/Windows-iotcore-samples)          | Sample apps to help you get started with developing for Windows on Devices.                                                                                                                                                                            |
| [Windows Community Toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit)  | A collection of helper functions, custom controls, and app services. It simplifies and demonstrates common developer tasks when building apps for Windows.                                                                                             |
| [Windows task snippets](https://github.com/Microsoft/Windows-task-snippets)              | Ready-to-use snippets of code that accomplish small but useful tasks of interest to UWP app developers. These snippets show simple solutions to common problems, and simple recipes to help you implement new app features.                            |
| [Win2D](https://github.com/Microsoft/win2d)                                              | Win2D is an easy-to-use Windows Runtime (WinRT) API for immediate-mode 2D graphics rendering with GPU acceleration. It's available to C# and C++ developers, and it utilizes the power of Direct2D, integrating seamlessly with XAML and `CoreWindow`. |



## Next steps

- [Windows Application Development - Best Practices](best-practices.md)
- [Windows Developer FAQ](windows-developer-faq.yml)