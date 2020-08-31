---
description: We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs.
title: Troubleshooting porting Windows Runtime 8.x to UWP'
ms.assetid: 1882b477-bb5d-4f29-ba99-b61096f45e50
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Troubleshooting porting Windows Runtime 8.x to UWP


The previous topic was [Porting the project](w8x-to-uwp-porting-to-a-uwp-project.md).

We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs. To that end, you can make temporary progress by commenting or stubbing out any non-essential code, and then returning to pay off that debt later. The table of troubleshooting symptoms and remedies in this topic may be helpful to you at this stage, although it's not a substitute for reading the next few topics. You can always refer back to the table as you progress through the later topics.

## Tracking down issues

XAML parse exceptions can be difficult to diagnose, particularly if there are no meaningful error messages within the exception. Make sure that the debugger is configured to catch first-chance exceptions (to try and catch the parsing exception early on). You may be able to inspect the exception variable in the debugger to determine whether the HRESULT or message has any useful information. Also, check Visual Studio's output window for error messages output by the XAML parser.

If your app terminates and all you know is that an unhandled exception was thrown during XAML markup parsing, then that could be the result of a reference to a missing resource (that is, a resource whose key exists for Universal 8.1 apps but not for Windows 10 apps, such as some system **TextBlock** Style keys). Or it could be an exception thrown inside a **UserControl**, a custom control, or a custom layout panel.

A last resort is a binary split. Remove about half of the markup from a Page and re-run the app. You will then know whether the error is somewhere inside the half you removed (which you should now restore in any case) or in the half you did *not* remove. Repeat the process by splitting the half that contains the error, and so on, until you've zeroed in on the issue.

## TargetPlatformVersion

This section explains what to do if, on opening a Windows 10 project in Visual Studio, you see the message "Visual Studio update required. One or more projects require a platform SDK \<version\> that is either not installed or is included as part of a future update to Visual Studio."

-   First, determine the version number of the SDK for Windows 10 that you have installed. Navigate to **C:\\Program Files (x86)\\Windows Kits\\10\\Include\\\<versionfoldername\>** and make a note of *\<versionfoldername\>*, which will be in quad notation, "Major.Minor.Build.Revision".
-   Open your project file for edit and find the `TargetPlatformVersion` and `TargetPlatformMinVersion` elements. Edit them to look like this, replacing *\<versionfoldername\>* with the quad notation version number you found on disk:

```xml
   <TargetPlatformVersion><versionfoldername></TargetPlatformVersion>
    <TargetPlatformMinVersion><versionfoldername></TargetPlatformMinVersion>
```

## Troubleshooting symptoms and remedies

The remedy information in the table is intended to give you enough info to unblock yourself. You'll find further details about each of these issues as you read through later topics.

| Symptom | Remedy |
|---------|--------|
| On opening a Windows 10 project in Visual Studio, you see the message "Visual Studio update required. One or more projects require a platform SDK &lt;version&gt; that is either not installed or is included as part of a future update to Visual Studio." | See the [TargetPlatformVersion](#targetplatformversion) section in this topic. |
| A System.InvalidCastException is thrown when InitializeComponent is called in a xaml.cs file.| This can happen when you have more than one xaml file (at least one of which is MRT-qualified) sharing the same xaml.cs file and elements have x:Name attributes that are inconsistent between the two xaml files. Try adding the same name to the same elements in both xaml files, or omit names altogether. |
| When run on the device, the app terminates, or when launched from Visual Studio, you see the error “Unable to activate Windows Runtime 8.x app \[…\]. The activation request failed with error ‘Windows was unable to communicate with the target application. This usually indicates that the target application’s process aborted. \[…\]”. | The problem could be the imperative code running in your own Pages or in bound properties (or other types) during initialization. Or it could be happening while parsing the XAML file about to be displayed when the app terminated (if launching from Visual Studio, that will be the startup page). Look for invalid resource keys, and/or try some of the guidance in the "Tracking down issues" section in this topic.|
| The XAML parser or compiler, or a runtime exception, gives the error "*The resource "\<resourcekey\>" could not be resolved.*". | The resource key doesn't apply to Universal Windows Platform (UWP) apps (this is the case with some Windows Phone resources, for example). Find the correct equivalent resource and update your markup. Examples you might encounter right away are system keys such as `PhoneAccentBrush`. |
| The C# compiler gives the error "*The type or namespace name '\<name\>' could not be found \[...\]*" or "*The type or namespace name '\<name\>' does not exist in the namespace \[...\]*" or "*The type or namespace name '\<name\>' does not exist in the current context*". | This is likely to mean that type is implemented in an extension SDK (although there may be cases where the remedy is not so straightforward). Use the [Windows APIs](/uwp/) reference content to determine what extension SDK implements the API and then use Visual Studio's **Add** > **Reference** command to add a reference to that SDK to your project. If your app targets the set of APIs known as the universal device family then it's vital that you use the [**ApiInformation**](/uwp/api/Windows.Foundation.Metadata.ApiInformation) class to test at runtime for the presence of extension SDK before you call them (this is called adaptive code). If a universal API exists, then that's always preferable to an API in an extension SDK. For more info, see [Extension SDKs](w8x-to-uwp-porting-to-a-uwp-project.md). |

The next topic is [Porting XAML and UI](w8x-to-uwp-porting-xaml-and-ui.md).