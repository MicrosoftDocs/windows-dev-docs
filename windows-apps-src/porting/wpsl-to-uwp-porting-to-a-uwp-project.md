---
description: You begin the porting process by creating a new Windows 10 project in Visual Studio and copying your files into it.
title: Porting Windows Phone Silverlight projects to UWP projects
ms.assetid: d86c99c5-eb13-4e37-b000-6a657543d8f4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Porting Windows Phone Silverlight projects to UWP projects


The previous topic was [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md).

You begin the porting process by creating a new Windows 10 project in Visual Studio and copying your files into it.

## Create the project and copy files to it

1.  Launch Microsoft Visual Studio 2015 and create a new Blank Application (Windows Universal) project. For more info, see [Jumpstart your Windows Runtime 8.x app using templates (C#, C++, Visual Basic)](/previous-versions/windows/apps/hh768232(v=win.10)). Your new project builds an app package (an appx file) that will run on all device families.
2.  In your Windows Phone Silverlight app project, identify all the source code files and visual asset files that you want to reuse. Using File Explorer, copy data models, view models, visual assets, Resource Dictionaries, folder structure, and anything else that you wish to re-use, to your new project. Copy or create sub-folders on disk as necessary.
3.  Copy views (for example, MainPage.xaml and MainPage.xaml.cs) into the new project node, too. Again, create new sub-folders as necessary, and remove the existing views from the project. But, before you over-write or remove a view that Visual Studio generated, keep a copy because it may be useful to refer to it later. The first phase of porting a Windows Phone Silverlight app focuses on getting it to look good and work well on one device family. Later, you'll turn your attention to making sure the views adapt themselves well to all form factors, and optionally to adding any adaptive code to get the most from a particular device family.
4.  In **Solution Explorer**, make sure **Show All Files** is toggled on. Select the files that you copied, right-click them, and click **Include In Project**. This will automatically include their containing folders. You can then toggle **Show All Files** off if you like. An alternative workflow, if you prefer, is to use the **Add Existing Item** command, having created any necessary sub-folders in the Visual Studio **Solution Explorer**. Double-check that your visual assets have **Build Action** set to **Content** and **Copy to Output Directory** set to **Do not copy**.
5.  The differences in namespace and class names will generate lots of build errors at this stage. For example, if you open the views that Visual Studio generated, you'll see that they are of type [**Page**](/uwp/api/Windows.UI.Xaml.Controls.Page), and not **PhoneApplicationPage**. There are lots of XAML markup and imperative code differences that the following topics in this porting guide cover in detail. But, you'll make fast progress just following these general steps: change "clr-namespace" to "using" in your namespace prefix declarations in XAML markup; use the [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md) topic and Visual Studio's **Find and Replace** command to make bulk changes to your source code (for example, replace "System.Windows" with "Windows.UI.Xaml"); and in the imperative code editor in Visual Studio use the **Resolve** and **Organize Usings** commands on the context menu for more targeted changes.

## Extension SDKs

Most of the Universal Windows Platform (UWP) APIs your ported app will call are implemented in the set of APIs known as the universal device family. But, some are implemented in extension SDKs, and Visual Studio only recognizes APIs that are implemented by your app's target device family or by any extension SDKs that you have referenced.

If you get compile errors about namespaces or types or members that could not be found, then this is likely to be the cause. Open the API's topic in the API reference documentation and navigate to the Requirements section: that will tell you what the implementing device family is. If that's not your target device family, then to make the API available to your project, you will need a reference to the extension SDK for that device family.

Click **Project** &gt; **Add Reference** &gt; **Windows Universal** &gt; **Extensions** and select the appropriate extension SDK. For example, if the APIs you want to call are available only in the mobile device family, and they were introduced in version 10.0.x.y, then select **Windows Mobile Extensions for the UWP**.

That will add the following reference to your project file:

```XML
<ItemGroup>
    <SDKReference Include="WindowsMobile, Version=10.0.x.y">
        <Name>Windows Mobile Extensions for the UWP</Name>
    </SDKReference>
</ItemGroup>
```

The name and version number match the folders in the installed location of your SDK. For example, the above information matches this folder name:

`\Program Files (x86)\Windows Kits\10\Extension SDKs\WindowsMobile\10.0.x.y`

Unless your app targets the device family that implements the API, you'll need to use the [**ApiInformation**](/uwp/api/Windows.Foundation.Metadata.ApiInformation) class to test for the presence of the API before you call it (this is called adaptive code). This condition will then be evaluated wherever your app runs, but it will only evaluate to true on devices where the API is present and therefore available to call. Only use extension SDKs and adaptive code after first checking whether a universal API exists. Some examples are given in the section below.

Also, see [App package manifest](#the-app-package-manifest).

## Maximizing markup and code reuse

You will find that refactoring a little, and/or adding adaptive code (which is explained below), will allow you to maximize the markup and code that works across all device families. Here are more details.

-   Files that are common to all device families need no special consideration. Those files will be used by the app on all the device families that it runs on. This includes XAML markup files, imperative source code files, and asset files.
-   It is possible for your app to detect the device family that it is running on and navigate to a view that has been designed specifically for that device family. For more details, see [Detecting the platform your app is running on](wpsl-to-uwp-input-and-sensors.md).
-   A similar technique that you may find useful if there is no alternative is to give a markup file or **ResourceDictionary** file (or the folder that contains the file) a special name such that it is automatically loaded at runtime only when your app runs on a particular device family. This technique is illustrated in the [Bookstore1](wpsl-to-uwp-case-study-bookstore1.md) case study.
-   To use features that are not available on all device families (for example, printers, scanners, or the camera button) you can write adaptive code. See the third example in [Conditional compilation and adaptive code](#conditional-compilation-and-adaptive-code) in this topic.
-   If you want to support both Windows Phone Silverlight and Windows 10, then you may be able to share source code files between projects. Here's how: in Visual Studio, right-click the project in **Solution Explorer**, select **Add Existing Item**, select the files to share, and then click **Add As Link**. Store your source code files in a common folder on the file system where the projects that link to them can see them, and don't forget to add them to source control. If you can factor your imperative source code so that most, if not all, of a file will work on both platforms, then you don't need to have two copies of it. You can wrap any platform-specific logic in the file inside conditional compilation directives where possible, or run-time conditions where necessary. See the next section below, and [C# Preprocessor Directives](/dotnet/articles/csharp/language-reference/preprocessor-directives/index).
-   For reuse at the binary level, rather than the source code level, there are Portable Class Libraries, which support the subset of .NET APIs that are available in Windows Phone Silverlight as well as the subset for Windows 10 apps (.NET Core). Portable Class Library assemblies are binary compatible with these .NET platforms and more. Use Visual Studio to create a project that targets a Portable Class Library. See [Cross-Platform Development with the Portable Class Library](/dotnet/standard/cross-platform/cross-platform-development-with-the-portable-class-library).

## Conditional compilation and adaptive code

If you want to support both Windows Phone Silverlight and Windows 10 in a single code file then you can do that. If you look in your Windows 10 project at the project properties pages, you'll see that the project defines WINDOWS\_UAP as a conditional compilation symbol. In general, you can use the following logic to perform conditional compilation.

```csharp
#if WINDOWS_UAP
    // Code that you want to compile into the Windows 10 app.
#else
    // Code that you want to compile into the Windows Phone Silverlight app.
#endif // WINDOWS_UAP
```

If you have code that you've been sharing between a Windows Phone Silverlight app and a Windows Runtime 8.x app, then you may already have source code with logic like this:

```csharp
#if NETFX_CORE
    // Code that you want to compile into the Windows Runtime 8.x app.
#else
    // Code that you want to compile into the Windows Phone Silverlight app.
#endif // NETFX_CORE
```

If so, and if you now want to support Windows 10 in addition, then you can do that, too.

```csharp
#if WINDOWS_UAP
    // Code that you want to compile into the Windows 10 app.
#else
#if NETFX_CORE
    // Code that you want to compile into the Windows Runtime 8.x app.
#else
    // Code that you want to compile into the Windows Phone Silverlight app.
#endif // NETFX_CORE
#endif // WINDOWS_UAP
```

You may have used conditional compilation to limit handling of the hardware back button to Windows Phone. In Windows 10, the back button event is a universal concept. Back buttons implemented in hardware or in software will all raise the [**BackRequested**](/uwp/api/windows.ui.core.systemnavigationmanager.backrequested) event, so that's the one to handle.

```csharp
       Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
            this.ViewModelLocator_BackRequested;

...

    private void ViewModelLocator_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
    {
        // Handle the event.
    }

```

You may have used conditional compilation to limit handling of the hardware camera button to Windows Phone. In Windows 10, the hardware camera button is a concept particular to the mobile device family. Because one app package will be running on all devices, we change our compile-time condition into a run-time condition using what is known as adaptive code. To do that, we use the [**ApiInformation**](/uwp/api/Windows.Foundation.Metadata.ApiInformation) class to query at run-time for the presence of the [**HardwareButtons**](/uwp/api/Windows.Phone.UI.Input.HardwareButtons) class. **HardwareButtons** is defined in the mobile extension SDK, so we'll need to add a reference to that SDK to our project for this code to compile. Note, though, that the handler will only be executed on a device that implements the types defined in the mobile extension SDK, and that's the mobile device family. So, the following code is careful only to use features that are present, although it achieves it in a different way from conditional compilation.

```csharp
       // Note: Cache the value instead of querying it more than once.
        bool isHardwareButtonsAPIPresent = Windows.Foundation.Metadata.ApiInformation.IsTypePresent
            ("Windows.Phone.UI.Input.HardwareButtons");

        if (isHardwareButtonsAPIPresent)
        {
            Windows.Phone.UI.Input.HardwareButtons.CameraPressed +=
                this.HardwareButtons_CameraPressed;
        }

    ...

    private void HardwareButtons_CameraPressed(object sender, Windows.Phone.UI.Input.CameraEventArgs e)
    {
        // Handle the event.
    }
```

Also, see [Detecting the platform your app is running on](wpsl-to-uwp-input-and-sensors.md).

## The app package manifest

The settings in your project (including any extension SDKs references) determine the API surface area that your app can call. But, your app package manifest is what determines the actual set of devices that your customers can install your app onto from the Store. For more info, see Examples in [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily).

It's worth knowing how to edit the app package manifest, because the topics that follow talk about using it for various declarations, capabilities, and other settings that some features need. You can use the Visual Studio app package manifest editor to edit it. If the **Solution Explorer** is not shown, choose it from the **View** menu. Double-click **Package.appxmanifest**. This opens the manifest editor window. Select the appropriate tab to make changes and then save the changes. You may want to ensure that the **pm:PhoneIdentity** element in the ported app manifest matches what is in the app manifest of the app you're porting (for full details, see the [**pm:PhoneIdentity**](/uwp/schemas/appxpackage/uapmanifestschema/element-pm-phoneidentity) topic).

See [Package manifest schema reference for Windows 10](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).

The next topic is [Troubleshooting](wpsl-to-uwp-troubleshooting.md).