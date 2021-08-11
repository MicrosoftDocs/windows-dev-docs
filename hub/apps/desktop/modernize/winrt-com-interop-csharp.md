---
description: Enhance your desktop application for Windows 10 users by using projected COM interop interfaces in .NET 5+.
title: Call WinRT COM interop interfaces from .NET 5+ apps
ms.date: 07/07/2021
ms.topic: article
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Call WinRT COM interop interfaces from .NET 5+ apps

In .NET 5 and later, C# desktop application developers can more easily access select WinRT COM interop interfaces. These APIs are available in .NET 5 and later by using a [Target Framework Moniker](desktop-to-uwp-enhance.md#net-5-use-the-target-framework-moniker-option) that targets Windows 10, version 1809 or later.

Before .NET 5, C# developers could define an interop interface directly in C# with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) attribute and directly cast a projected class to that interop interface. Starting with .NET 5, built-in WinRT projection support has been removed from the C# compiler and .NET runtime. While the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) technique still works for **IUnknown**-based interop interfaces, it no longer works for [IInspectable](/windows/win32/api/inspectable/nn-inspectable-iinspectable)-based interfaces which are used for interoperating with WinRT interfaces (see [ComInterfaceType](/dotnet/api/system.runtime.interopservices.cominterfacetype)).

As a replacement, starting with the .NET 5.0.205 SDK and .NET 5.0.302 SDK releases, apps can make use of COM interop class implementations for several WinRT COM interop interfaces as well as [IWindowNative](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) and [IInitializeWithWindow](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow). This article provides a list of the available class implementations and instructions for using them.

## Available interop classes

The following is a list of available C# interop classes and the mappings to the underlying COM interfaces. All classes listed below implement the methods of the associated COM interface and provide type-safe wrappers for parameters and return values. For example, **DragDropManagerInterop.GetForWindow** requires an **IntPtr** window handle parameter and returns a [CoreDragDropManager](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragdropmanager) object. All of the WinRT COM interop classes below and associated methods are static.

| Class | COM interface |
| -------------------------|-------|
| WinRT.Interop.WindowNative | [IWindowNative](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) |
| WinRT.Interop.InitializeWithWindow | [IInitializeWithWindow](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) |
| Windows.ApplicationModel.DataTransfer.DragDrop.Core.DragDropManagerInterop | [IDragDropManagerInterop](/windows/win32/api/dragdropinterop/nn-dragdropinterop-idragdropmanagerinterop) |
| Windows.Graphics.Printing.PrintManagerInterop | [IPrintManagerInterop](/windows/win32/api/printmanagerinterop/nn-printmanagerinterop-iprintmanagerinterop) |
| Windows.Media.SystemMediaTransportControlsInterop | [ISystemMediaTransportControlsInterop](/windows/win32/api/systemmediatransportcontrolsinterop/nn-systemmediatransportcontrolsinterop-isystemmediatransportcontrolsinterop) |
| Windows.Media.PlayTo.PlayToManagerInterop | [IPlayToManagerInterop](/windows/win32/api/playtomanagerinterop/nn-playtomanagerinterop-iplaytomanagerinterop) |
| Windows.Security.Credentials.UI.UserConsentVerifierInterop | [IUserConsentVerifierInterop](/windows/win32/api/userconsentverifierinterop/nn-userconsentverifierinterop-iuserconsentverifierinterop) |
| Windows.Security.Authentication.Web.Core.WebAuthenticationCoreManagerInterop | [IWebAuthenticationCoreManagerInterop](/windows/win32/api/webauthenticationcoremanagerinterop/nn-webauthenticationcoremanagerinterop-iwebauthenticationcoremanagerinterop) |
| Windows.UI.ApplicationSettings.AccountsSettingsPaneInterop | [IAccountsSettingsPaneInterop](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop) |
| Windows.UI.Input.RadialControllerConfigurationInterop | [IRadialControllerConfigurationInterop](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerconfigurationinterop) |
| Windows.UI.Input.RadialControllerInterop | [IRadialControllerInterop](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerinterop) |
| Windows.UI.Input.Core.RadialControllerIndependentInputSourceInterop | **IRadialControllerIndependentInputSourceInterop** |
| Windows.UI.Input.Spatial.SpatialInteractionManagerInterop | [ISpatialInteractionManagerInterop](/windows/win32/api/spatialinteractionmanagerinterop/nn-spatialinteractionmanagerinterop-ispatialinteractionmanagerinterop) |
| Windows.UI.ViewManagement.InputPaneInterop | [IInputPaneInterop](/windows/win32/api/inputpaneinterop/nn-inputpaneinterop-iinputpaneinterop) |
| Windows.UI.ViewManagement.UIViewSettingsInterop | [IUIViewSettingsInterop](/windows/win32/api/uiviewsettingsinterop/nn-uiviewsettingsinterop-iuiviewsettingsinterop) |

## Configure a .NET 5+ desktop project to use the C# interop classes

To configure your desktop project to access the C# interop classes, follow these steps:

1. Open the project file for your C# project. In the .csproj file, modify the `TargetFramework` element to target a specific Windows SDK version. For example, the following element is for a project that targets Windows 10, version 2004.

    ```xml
    <PropertyGroup>
        <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
    </PropertyGroup>
    ```

    For more information including a list of other supported values, see [Use the Target Framework Moniker option](desktop-to-uwp-enhance.md#net-5-use-the-target-framework-moniker-option).

2. The following example demonstrates how to use the **WinRT.Interop.WindowNative** interop class in a [WinUI 3-based application](../../winui/winui3/create-your-first-winui3-app.md) to obtain a window handle object and then open a folder picker dialog using the window handle. In this example, the `this` object is an instance of the [Microsoft.UI.Xaml.Window](/windows/winui/api/microsoft.ui.xaml.window) class provided by WinUI 3. Note that the **WinRT.Interop.WindowNative** class implements the [IWindowNative](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) interface provided by WinUI 3, and this interface is implemented by the [Microsoft.UI.Xaml.Window](/windows/winui/api/microsoft.ui.xaml.window) class.

    ```csharp
    private async void myButton_Click(object sender, RoutedEventArgs e)
    {
        // Pass in the current WinUI window and get its handle
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var folderPicker = new Windows.Storage.Pickers.FolderPicker();
        folderPicker.FileTypeFilter.Add("*");
        WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

        // Now you can call methods on folderPicker
        var folder = await folderPicker.PickSingleFolderAsync();
        // ...
    }
    ```

## Troubleshooting and known issues

This section lists known issues and solutions for using the WinRT COM interop APIs. To provide feedback or to report other issues, add your feedback to an existing issue or file a new issue on the [C#/WinRT GitHub repo](https://github.com/microsoft/CsWinRT).

- **WinRT.Interop.WindowNative.GetWindowHandle does not marshal window handles (HWNDs) correctly when running on x86 platforms.** To resolve this issue, update your .NET 5 SDK version to one of the following versions (or later): .NET SDK 5.0.206, 5.0.303, or 5.0.400.


