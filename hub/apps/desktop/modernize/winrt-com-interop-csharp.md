---
title: Call WinRT COM interop interfaces from a .NET 5+ app
description: Enhance your desktop application for your users by calling WinRT COM interop interfaces projected into .NET 5+.
ms.date: 01/13/2022
ms.topic: article
ms.localizationpriority: medium
---

# Call WinRT COM interop interfaces from a .NET 5+ app

As a C# desktop application developer, in .NET 5 and later you can make use of C# interop classes representing several Windows Runtime (WinRT) COM interoperability interfaces. These include [IWindowNative](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) and [IInitializeWithWindow](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow).

This topic lists the available C# interop classes, and shows how to use them. The [Background](#background) section at the end of the topic describes how interop interfaces were used in previous versions of .NET, and why the change was made.

## Configure a .NET 5+ desktop project to use the C# interop classes

The C# interop classes listed in the next section ([Available C# interop classes](#available-c-interop-classes)) are available in .NET 5 and later by using a [Target Framework Moniker](desktop-to-uwp-enhance.md#net-5-and-later-use-the-target-framework-moniker-option) that targets Windows 10, version 1809 or later.

### A WinUI 3 C# desktop project

When you create a new project from a WinUI 3 project template in Visual Studio (see [Create a WinUI 3 app](/windows/apps/winui/winui3/create-your-first-winui3-app)), your `.csproj` file is already configured, and you can start using the C# interop classes right away.

### Other C# desktop project types

For other .NET 5+ desktop project types, to access the C# interop classes, follow these steps:

1. Open the project file for your C# .NET 5+ desktop project.

2. In the `.csproj` file, modify the **TargetFramework** element to target a specific .NET and Windows SDK version. For example, the following element is appropriate for a .NET 6 project that targets Windows 10, version 2004.

    ```xml
    <PropertyGroup>
      <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
    </PropertyGroup>
    ```

For more information&mdash;including a list of other supported values&mdash;see [Use the Target Framework Moniker option](desktop-to-uwp-enhance.md#net-5-and-later-use-the-target-framework-moniker-option).

## Available C# interop classes

> [!NOTE]
> The classes below require the .NET 5.0.205 SDK or later.

Here are the available C# interop classes, mapped from their underlying WinRT COM interop interfaces. Each class listed implements the methods of its underlying WinRT COM interop interface, and provides type-safe wrappers for parameters and return values. For example, **Windows.ApplicationModel.DataTransfer.DragDrop.Core.DragDropManagerInterop.GetForWindow** requires an **IntPtr** window handle (HWND) parameter, and returns a [**CoreDragDropManager**](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragdropmanager) object. All of the C# interop classes below and associated methods are static.

|WinRT COM interop interface|C# interop class|
|-|-|
|[**IAccountsSettingsPaneInterop**](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop)|(**Windows.UI.ApplicationSettings**) **AccountsSettingsPaneInterop**|
|[**IDragDropManagerInterop**](/windows/win32/api/dragdropinterop/nn-dragdropinterop-idragdropmanagerinterop)|(**Windows.ApplicationModel.DataTransfer.DragDrop.Core**) **DragDropManagerInterop**|
|[**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow)|(**WinRT.Interop**) **InitializeWithWindow**|
|[**IInputPaneInterop**](/windows/win32/api/inputpaneinterop/nn-inputpaneinterop-iinputpaneinterop)|(**Windows.UI.ViewManagement**) **InputPaneInterop**|
|[**IPlayToManagerInterop**](/windows/win32/api/playtomanagerinterop/nn-playtomanagerinterop-iplaytomanagerinterop)|(**Windows.Media.PlayTo**) **PlayToManagerInterop**|
|[**IPrintManagerInterop**](/windows/win32/api/printmanagerinterop/nn-printmanagerinterop-iprintmanagerinterop)|(**Windows.Graphics.Printing**) **PrintManagerInterop**|
|[**IRadialControllerConfigurationInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerconfigurationinterop)|(**Windows.UI.Input**) **RadialControllerConfigurationInterop**|
|**IRadialControllerIndependentInputSourceInterop**|(**Windows.UI.Input.Core**) **RadialControllerIndependentInputSourceInterop**|
|[**IRadialControllerInterop**](/windows/win32/api/radialcontrollerinterop/nn-radialcontrollerinterop-iradialcontrollerinterop)|(**Windows.UI.Input**) **RadialControllerInterop**|
|[**ISpatialInteractionManagerInterop**](/windows/win32/api/spatialinteractionmanagerinterop/nn-spatialinteractionmanagerinterop-ispatialinteractionmanagerinterop)|(**Windows.UI.Input.Spatial**) **SpatialInteractionManagerInterop**|
|[**ISystemMediaTransportControlsInterop**](/windows/win32/api/systemmediatransportcontrolsinterop/nn-systemmediatransportcontrolsinterop-isystemmediatransportcontrolsinterop)|(**Windows.Media**) **SystemMediaTransportControlsInterop**|
|[**IUIViewSettingsInterop**](/windows/win32/api/uiviewsettingsinterop/nn-uiviewsettingsinterop-iuiviewsettingsinterop)|(**Windows.UI.ViewManagement**) **UIViewSettingsInterop**|
|[**IUserConsentVerifierInterop**](/windows/win32/api/userconsentverifierinterop/nn-userconsentverifierinterop-iuserconsentverifierinterop)|(**Windows.Security.Credentials.UI**) **UserConsentVerifierInterop**|
|[**IWebAuthenticationCoreManagerInterop**](/windows/win32/api/webauthenticationcoremanagerinterop/nn-webauthenticationcoremanagerinterop-iwebauthenticationcoremanagerinterop)|(**Windows.Security.Authentication.Web.Core**) **WebAuthenticationCoreManagerInterop**|
|[**IWindowNative**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative)|(**WinRT.Interop**) **WindowNative**|

## Code example

This code example demonstrates how to use C# interop classes in a WinUI 3 application (see [Create a WinUI 3 app](/windows/apps/winui/winui3/create-your-first-winui3-app)). The example scenario is to display a [**Windows.Storage.Pickers.FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker). But before displaying the picker, it's necessary to initialize it with the handle (HWND) of the owner window.

1. You can obtain a window handle (HWND) by using the [**IWindowNative**](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nn-microsoft-ui-xaml-window-iwindownative) WinRT COM interop interface. And (looking in the table in the previous section) that interface is represented by the **WinRT.Interop.WindowNative** C# interop class. Here, the `this` object is a reference to a [**Microsoft.UI.Xaml.Window**](/windows/winui/api/microsoft.ui.xaml.window) object from the main window code-behind file.
2. To initialize a piece of UI with an owner window, you use the [**IInitializeWithWindow**](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) WinRT COM interop interface. And that interface is represented by the **WinRT.Interop.InitializeWithWindow** C# interop class.

```csharp
private async void myButton_Click(object sender, RoutedEventArgs e)
{
    // Create a folder picker.
    var folderPicker = new Windows.Storage.Pickers.FolderPicker();

    // 1. Retrieve the window handle (HWND) of the current WinUI 3 window.
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

    // 2. Initialize the folder picker with the window handle (HWND).
    WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter.Add("*");
    var folder = await folderPicker.PickSingleFolderAsync();
}
```

## Background

Previous versions of the .NET Framework and .NET Core had built-in knowledge of WinRT. With those previous versions, you could define an interop interface directly in C# with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) attribute, and then directly cast a projected class to that interop interface.

Since WinRT is a Windows-specific technology, to support the portability and efficiency goals of .NET 5, we lifted the WinRT projection support out of the C# compiler and .NET runtime, and moved it into the [C#/WinRT](/windows/uwp/csharp-winrt/) toolkit (see [Built-in support for WinRT is removed from .NET](/dotnet/core/compatibility/interop/5.0/built-in-support-for-winrt-removed)).

While the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) technique still works for **IUnknown**-based interop interfaces, it no longer works for the [IInspectable](/windows/win32/api/inspectable/nn-inspectable-iinspectable)-based interfaces that are used for interoperating with WinRT.

So as a replacement, in .NET 5 and later, you can make use of the C# interop classes described in this topic.

## Troubleshooting and known issues

This section lists known issues and solutions for using the C# interop classes. To provide feedback, or to report other issues, add your feedback to an existing issue, or file a new issue on the [C#/WinRT GitHub repo](https://github.com/microsoft/CsWinRT).

* **WinRT.Interop.WindowNative.GetWindowHandle doesn't marshal window handles (HWNDs) correctly when running on x86 platforms.** To work around this issue, update your .NET 5 SDK version to one of the following versions (or later): .NET SDK 5.0.206, 5.0.400, or 6.0.100.
