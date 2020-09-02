---
description: Learn how to port a Windows Runtime 8.x project to a Universal Windows Platform (UWP) project on Windows 10.
title: Porting a Windows Runtime 8.x project to a UWP project'
ms.assetid: 2dee149f-d81e-45e0-99a4-209a178d415a
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Porting a Windows Runtime 8.x project to a UWP project



You have two options when you begin the porting process. One is to edit a copy of your existing project files, including the app package manifest (for that option, see the info about updating your project files in [Migrate apps to the Universal Windows Platform (UWP)](/visualstudio/misc/migrate-apps-to-the-universal-windows-platform-uwp?view=vs-2015)). The other option is to create a new Windows 10 project in Visual Studio and copy your files into it. The first section in this topic describes that second option, but the rest of the topic has additional info applicable to both options. You can also choose to keep your new Windows 10 project in the same solution as your existing projects and share source code files using a shared project. Or, you can keep the new project in a solution of its own and share source code files using the linked files feature in Visual Studio.

## Create the project and copy files to it

These steps focus on the option to create a new Windows 10 project in Visual Studio and copy your files into it. Some of the specifics around how many projects you create, and which files you copy over, will depend on the factors and decisions described in [If you have a Universal 8.1 app](w8x-to-uwp-root.md) and the sections that follow it. These steps assume the simplest case.

1.  Launch Microsoft Visual Studio 2015 and create a new Blank Application (Windows Universal) project. For more info, see [Jumpstart your Windows Runtime 8.x app using templates (C#, C++, Visual Basic)](/previous-versions/windows/apps/hh768232(v=win.10)). Your new project builds an app package (an appx file) that will run on all device families.
2.  In your Universal 8.1 app project, identify all the source code files and visual asset files that you want to reuse. Using File Explorer, copy data models, view models, visual assets, Resource Dictionaries, folder structure, and anything else that you wish to re-use, to your new project. Copy or create sub-folders on disk as necessary.
3.  Copy views (for example, MainPage.xaml and MainPage.xaml.cs) into the new project, too. Again, create new sub-folders as necessary, and remove the existing views from the project. But, before you over-write or remove a view that Visual Studio generated, keep a copy because it may be useful to refer to it later. The first phase of porting a Universal 8.1 app focuses on getting it to look good and work well on one device family. Later, you'll turn your attention to making sure the views adapt themselves well to all form factors, and optionally to adding any adaptive code to get the most from a particular device family.
4.  In **Solution Explorer**, make sure **Show All Files** is toggled on. Select the files that you copied, right-click them, and click **Include In Project**. This will automatically include their containing folders. You can then toggle **Show All Files** off if you like. An alternative workflow, if you prefer, is to use the **Add Existing Item** command, having created any necessary sub-folders in the Visual Studio **Solution Explorer**. Double-check that your visual assets have **Build Action** set to **Content** and **Copy to Output Directory** set to **Do not copy**.
5.  You are likely to see some build errors at this stage. But, if you know what you need to change, then you can use Visual Studio's **Find and Replace** command to make bulk changes to your source code; and in the imperative code editor in Visual Studio, use the **Resolve** and **Organize Usings** commands on the context menu for more targeted changes.

## Maximizing markup and code reuse

You will find that refactoring a little, and/or adding adaptive code (which is explained below), will allow you to maximize the markup and code that works across all device families. Here are more details.

-   Files that are common to all device families need no special consideration. Those files will be used by the app on all the device families that it runs on. This includes XAML markup files, imperative source code files, and asset files.
-   It is possible for your app to detect the device family that it is running on and navigate to a view that has been designed specifically for that device family. For more details, see [Detecting the platform your app is running on](w8x-to-uwp-input-and-sensors.md).
-   A similar technique that you may find useful if there is no alternative is give a markup file or **ResourceDictionary** file (or the folder that contains the file) a special name such that it is automatically loaded at runtime only when your app runs on a particular device family. This technique is illustrated in the [Bookstore1](w8x-to-uwp-case-study-bookstore1.md) case study.
-   You should be able to remove a lot of the conditional compilation directives in your Universal 8.1 app's source code if you only need to support Windows 10. See [Conditional compilation and adaptive code](#conditional-compilation-and-adaptive-code) in this topic.
-   To use features that are not available on all device families (for example, printers, scanners, or the camera button), you can write adaptive code. See the third example in [Conditional compilation and adaptive code](#conditional-compilation-and-adaptive-code) in this topic.
-   If you want to support Windows 8.1, Windows Phone 8.1, and Windows 10, then you can keep three projects in the same solution and share code with a Shared project. Alternatively, you can share source code files between projects. Here's how: in Visual Studio, right-click the project in **Solution Explorer**, select **Add Existing Item**, select the files to share, and then click **Add As Link**. Store your source code files in a common folder on the file system where the projects that link to them can see them. And don't forget to add them to source control.
-   For reuse at the binary level, rather than the source code level, see [Creating Windows Runtime components in C# and Visual Basic](/previous-versions/windows/apps/br230301(v=vs.140)). There are also Portable Class Libraries, which support the subset of .NET APIs that are available in the .NET Framework for Windows 8.1, Windows Phone 8.1, and Windows 10 apps (.NET Core), and the full .NET Framework. Portable Class Library assemblies are binary compatible with all these platforms. Use Visual Studio to create a project that targets a Portable Class Library. See [Cross-Platform Development with the Portable Class Library](/dotnet/standard/cross-platform/cross-platform-development-with-the-portable-class-library).

## Extension SDKs

Most of the Windows Runtime APIs your Universal 8.1 app already calls are implemented in the set of APIs known as the universal device family. But, some are implemented in extension SDKs, and Visual Studio only recognizes APIs that are implemented by your app's target device family or by any extension SDKs that you have referenced.

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

Also, see [App package manifest](#app-package-manifest).

## Conditional compilation and adaptive code

If you're using conditional compilation (with C# preprocessor directives) so that your code files work on both Windows 8.1 and Windows Phone 8.1, then you can now review that conditional compilation in light of the convergence work done in Windows 10. Convergence means that, in your Windows 10 app, some conditions can be removed altogether. Others change to run-time checks, as demonstrated in the examples below.

**Note**   If you want to support Windows 8.1, Windows Phone 8.1, and Windows 10 in a single code file, then you can do that too. If you look in your Windows 10 project at the project properties pages, you'll see that the project defines WINDOWS\_UAP as a conditional compilation symbol. So, you can use that in combination with WINDOWS\_APP and WINDOWS\_PHONE\_APP. These examples show the simpler case of removing the conditional compilation from a Universal 8.1 app and substituting the equivalent code for a Windows 10 app.

This first example shows the usage pattern for the **PickSingleFileAsync** API (which applies only to Windows 8.1) and the **PickSingleFileAndContinue** API (which applies only to Windows Phone 8.1).

```csharp
#if WINDOWS_APP
    // Use Windows.Storage.Pickers.FileOpenPicker.PickSingleFileAsync
#else
    // Use Windows.Storage.Pickers.FileOpenPicker.PickSingleFileAndContinue
#endif // WINDOWS_APP
```

Windows 10 converges on the [**PickSingleFileAsync**](/uwp/api/windows.storage.pickers.fileopenpicker.picksinglefileasync) API, so your code simplifies to this:

```csharp
    // Use Windows.Storage.Pickers.FileOpenPicker.PickSingleFileAsync
```

In this example, we handle the hardware back button—but only on Windows Phone.

```csharp
#if WINDOWS_PHONE_APP
        Windows.Phone.UI.Input.HardwareButtons.BackPressed += this.HardwareButtons_BackPressed;
#endif // WINDOWS_PHONE_APP

...

#if WINDOWS_PHONE_APP
    void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
    {
        // Handle the event.
    }
#endif // WINDOWS_PHONE_APP
```

In Windows 10, the back button event is a universal concept. Back buttons implemented in hardware or in software will all raise the [**BackRequested**](/uwp/api/windows.ui.core.systemnavigationmanager.backrequested) event, so that's the one to handle.

```csharp
    Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
        this.ViewModelLocator_BackRequested;

...

private void ViewModelLocator_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
{
    // Handle the event.
}
```

This final example is similar to the previous one. Here, we handle the hardware camera button—but again, only in the code compiled into the Windows Phone app package.

```csharp
#if WINDOWS_PHONE_APP
    Windows.Phone.UI.Input.HardwareButtons.CameraPressed += this.HardwareButtons_CameraPressed;
#endif // WINDOWS_PHONE_APP

...

#if WINDOWS_PHONE_APP
void HardwareButtons_CameraPressed(object sender, Windows.Phone.UI.Input.CameraEventArgs e)
{
    // Handle the event.
}
#endif // WINDOWS_PHONE_APP
```

In Windows 10, the hardware camera button is a concept particular to the mobile device family. Because one app package will be running on all devices, we change our compile-time condition into a run-time condition using what is known as adaptive code. To do that, we use the [**ApiInformation**](/uwp/api/Windows.Foundation.Metadata.ApiInformation) class to query at run-time for the presence of the [**HardwareButtons**](/uwp/api/Windows.Phone.UI.Input.HardwareButtons) class. **HardwareButtons** is defined in the mobile extension SDK, so we'll need to add a reference to that SDK to our project for this code to compile. Note, though, that the handler will only be executed on a device that implements the types defined in the mobile extension SDK, and that's the mobile device family. So, this code is morally equivalent to the Universal 8.1 code in that it is careful only to use features that are present, although it achieves that in a different way.

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

Also, see [Detecting the platform your app is running on](w8x-to-uwp-input-and-sensors.md).

## App package manifest

The [What's changed in Windows 10](/uwp/schemas/appxpackage/uapmanifestschema/what-s-changed-in-windows-10) topic lists changes to the package manifest schema reference for Windows 10, including elements that have been added, removed, and changed. For reference info on all elements, attributes, and types in the schema, see [Element Hierarchy](/uwp/schemas/appxpackage/uapmanifestschema/root-elements). If you're porting a Windows Phone Store app, or if your app is an update to an app from the Windows Phone Store, ensure that the **pm:PhoneIdentity** element matches what's in the app manifest of your previous app (use the same GUIDs that were assigned to the app by the Store). This will ensure that users of your app who are upgrading to Windows 10 will receive your new app as an update, not a duplicate. See the [**pm:PhoneIdentity**](/uwp/schemas/appxpackage/uapmanifestschema/element-pm-phoneidentity) reference topic for more details.

The settings in your project (including any extension SDKs references) determine the API surface area that your app can call. But, your app package manifest is what determines the actual set of devices that your customers can install your app onto from the Store. For more info, see examples in [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily).

You can edit the app package manifest to set various declarations, capabilities, and other settings that some features need. You can use the Visual Studio app package manifest editor to edit it. If the **Solution Explorer** is not shown, choose it from the **View** menu. Double-click **Package.appxmanifest**. This opens the manifest editor window. Select the appropriate tab to make changes and then save.

The next topic is [Troubleshooting](w8x-to-uwp-troubleshooting.md).

## Related topics

* [Develop apps for the Universal Windows Platform](/visualstudio/cross-platform/develop-apps-for-the-universal-windows-platform-uwp?view=vs-2015)
* [Jumpstart your Windows Runtime 8.x app using templates (C#, C++, Visual Basic)](/previous-versions/windows/apps/hh768232(v=win.10))
* [Creating Windows Runtime components](/previous-versions/windows/apps/hh441572(v=vs.140))
* [Cross-Platform Development with the Portable Class Library](/dotnet/standard/cross-platform/cross-platform-development-with-the-portable-class-library)