---
title: Preview release channel for the Windows App SDK 
description: Provides info about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 08/07/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Latest preview channel release notes for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

**Preview channel release note archive:**

- [Preview channel release notes for the Windows App SDK 1.6](release-notes-archive/preview-channel-1.6.md)
- [Preview channel release notes for the Windows App SDK 1.5](release-notes-archive/preview-channel-1.5.md)
- [Preview channel release notes for the Windows App SDK 1.4](release-notes-archive/preview-channel-1.4.md)
- [Preview channel release notes for the Windows App SDK 1.3](release-notes-archive/preview-channel-1.3.md)
- [Preview channel release notes for the Windows App SDK 1.2](release-notes-archive/preview-channel-1.2.md)
- [Preview channel release notes for the Windows App SDK 1.1](release-notes-archive/preview-channel-1.1.md)
- [Preview channel release notes for the Windows App SDK 1.0](release-notes-archive/preview-channel-1.0.md)




## Version 1.7 Preview 1 (1.7-preview1)

This is the latest release of the preview channel for version 1.7.

In an existing Windows App SDK 1.6 (from the stable channel) app, you can update your Nuget package to 1.7.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### New features
 
#### New Badge Notifications Feature

New badge notification support allows showing a number or glyph badge on your app in the taskbar. For more info, see GitHub [#4926](https://github.com/microsoft/WindowsAppSDK/issues/4926).

#### New CameraCaptureUI API

A new CameraCaptureUI API makes it easier to capture photos and videos in your Windows App SDK app. For more info, see GitHub issue [#4721](https://github.com/microsoft/WindowsAppSDK/issues/4721).

#### New Authentication API

A new `OAuth2Manager` API provides a streamlined solution for web authentication, offering OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. For more info, see GitHub issue [#4772](https://github.com/microsoft/WindowsAppSDK/issues/4772).

#### New Background Task support

A new `BackgroundTaskBuilder` API enables registering background tasks for Windows App SDK apps. For more info, see GitHub [#4831](https://github.com/microsoft/WindowsAppSDK/issues/4831).

#### New TitleBar control

A new `TitleBar` control makes it much easier to create a great, customizable titlebar for your app. Configure properties such as the titlebar icon, Title, and Subtitle, include an integrated back button, or even add a custom control like a search box! The control includes robust titlebar capabilities like empty-space draggable regions, theme responsiveness, caption buttons, and built-in accessibility support so you can focus on your personalized design and still get the same reliable titlebar as the default experience. For more info, see GitHub [#10056](https://github.com/microsoft/microsoft-ui-xaml/issues/10056).

#### Support for MathML

`RichEditBox` now supports MathML, via `RichEditTextDocument.SetMathMode` and `RichEditTextDocument.SetMathML`. For more info, see GitHub [#4196](https://github.com/microsoft/microsoft-ui-xaml/issues/4196).

#### Other notable changes
 
* New `RuntimeCompatibilityOptions` support will allow more control over how servicing changes affect apps. For more info, see GitHub [#4966](https://github.com/microsoft/WindowsAppSDK/issues/4966).
* A new `ReleaseInfo` API provides easy access to the version of the Windows App SDK Runtime in use. For more info, see GitHub [#2893](https://github.com/microsoft/WindowsAppSDK/issues/2893).
* Note: Windows Copilot Runtime APIs are not included this release. To experiment with these APIs, please continue to use the 1.7-experimental3 release and share your feedback!
* Note: New APIs for windowing on `AppWindow` are not included in this release. To experiment with these APIs, please continue to use the 1.7-experimental3 release and share your feedback!
 
### New APIs
 
This release includes the following new APIs compared to the stable 1.6 release:
 
```
Microsoft.Security.Authentication.OAuth
 
    AuthFailure
    AuthRequestParams
    AuthRequestResult
    AuthResponse
    ClientAuthentication
    CodeChallengeMethodKind
    OAuth2Manager
    OAuthContract
    TokenFailure
    TokenFailureKind
    TokenRequestParams
    TokenRequestResult
    TokenResponse
```
```
Microsoft.UI.Text
 
    RichEditTextDocument
        GetMathML
        GetMathMode
        SetMathML
        SetMathMode
 
    TextApiContract
```
```
Microsoft.UI.Xaml
 
    XamlIsland
    XamlRoot
        ContentIsland
```
```
Microsoft.UI.Xaml.Controls
 
    TitleBar
    TitleBarAutomationPeer
    TitleBarTemplateSettings
```
```
Microsoft.Windows.ApplicationModel.Background
 
    BackgroundTaskBuilder
    BackgroundTaskContract
```
```
Microsoft.Windows.ApplicationModel.Background.UniversalBGTask
 
    Task
    UniversalBackgroundTaskContract
```
```
Microsoft.Windows.ApplicationModel.WindowsAppRuntime
 
    ReleaseInfo
    RuntimeCompatibilityChange
    RuntimeCompatibilityContract
    RuntimeCompatibilityOptions
    RuntimeInfo
    VersionInfoContract
    WindowsAppRuntimeVersion
```
```
Microsoft.Windows.BadgeNotifications
 
    BadgeNotificationGlyph
    BadgeNotificationManager
    BadgeNotificationsContract
```
```
Microsoft.Windows.Media.Capture
 
    CameraCaptureUI
    CameraCaptureUIContract
    CameraCaptureUIMaxPhotoResolution
    CameraCaptureUIMaxVideoResolution
    CameraCaptureUIMode
    CameraCaptureUIPhotoCaptureSettings
    CameraCaptureUIPhotoFormat
    CameraCaptureUIVideoCaptureSettings
    CameraCaptureUIVideoFormat
```

### Bug fixes

This release includes the following bug fixes:

- Changed `SplitButton` so touch input now matches the behavior of mouse input. For more info, see GitHub issue [#178](https://github.com/microsoft/microsoft-ui-xaml/issues/178).
- Changed cascading menus so sub menus now open immediately if clicked. For more info, see GitHub issue [#939](https://github.com/microsoft/microsoft-ui-xaml/issues/939).
- Fixed an issue where opening a `ComboBox` which is in a flyout closes all flyouts. For more info, see GitHub issue [#1467](https://github.com/microsoft/microsoft-ui-xaml/issues/1467).
- Fixed an issue where `SwipeControl` would randomly crash in a `ListView`. For more info, see GitHub issue [#2527](https://github.com/microsoft/microsoft-ui-xaml/issues/2527).
- Fixed an issue where drag-and-drop only a `ListViewItem` would leave it in the wrong visual state. For more info, see GitHub issue [#3458](https://github.com/microsoft/microsoft-ui-xaml/issues/3458).
- Fixed an issue in `StackLayout` so that it respects the ItemsRepeater.HorizontalAlignment and ItemsRepeater.VerticalAlignment properties (when StackLayout.Orientation is Vertical and Horizontal respectively). The old layout behaved as if the ItemsRepeater alignment was Stretch. With the fix, the layout results in items aligned to the right when the Right alignment is used, for example. For more info, see GitHub issue [#3842](https://github.com/microsoft/microsoft-ui-xaml/issues/3842).
- Fixed an issue where deleting items in the `ItemsRepeater`'s source would not generate items which moved up into view. For more info, see GitHub issue [#6661](https://github.com/microsoft/microsoft-ui-xaml/issues/6661).
- Fixed an issue where the right Alt key would not show keytips for Access Keys. For more info, see GitHub issue [#8447](https://github.com/microsoft/microsoft-ui-xaml/issues/8447). **Note:** This may result in key events for the right Alt key no longer being delivered to handles in the app or controls. 
- Fixed a crash where `UniformGridLayout` would sometimes pick a wrong layout anchor and cause infinite layout passes when scrolling backwards. For more info, see GitHub issue [#9199](https://github.com/microsoft/microsoft-ui-xaml/issues/9199).
- Fixed an issue where setting `NavigationFailedEventArgs.Handled` to True would still throw an exception. For more info, see GitHub issue [#9632](https://github.com/microsoft/microsoft-ui-xaml/issues/9632).
- Fixed an issue where `TabView` would not apply any specified `CornerRadius`. For more info, see GitHub issue [#9846](https://github.com/microsoft/microsoft-ui-xaml/issues/9846).
- Fixed a potential layout cycle crash in `StackLayout`. For more info, see GitHub issue [#9852](https://github.com/microsoft/microsoft-ui-xaml/issues/9852).
- Fixed a potential crash in `ItemsView` when removing items. For more info, see GitHub issue [#9868](https://github.com/microsoft/microsoft-ui-xaml/issues/9868).

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)