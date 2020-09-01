---
description: We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs.
title: Troubleshooting porting Windows Phone Silverlight to UWP
ms.assetid: d9a9a2a7-9401-4990-a992-4b13887f2661
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
#  Troubleshooting porting Windows Phone Silverlight to UWP


The previous topic was [Porting the project](wpsl-to-uwp-porting-to-a-uwp-project.md).

We highly recommend reading to the end of this porting guide, but we also understand that you're eager to forge ahead and get to the stage where your project builds and runs. To that end, you can make temporary progress by commenting or stubbing out any non-essential code, and then returning to pay off that debt later. The table of troubleshooting symptoms and remedies in this topic may be helpful to you at this stage, although it's not a substitute for reading the next few topics. You can always refer back to the table as you progress through the later topics.

## Tracking down issues

XAML parse exceptions can be difficult to diagnose, particularly if there are no meaningful error messages within the exception. Make sure that the debugger is configured to catch first-chance exceptions (to try and catch the parsing exception early on). You may be able to inspect the exception variable in the debugger to determine whether the HRESULT or message has any useful information. Also, check Visual Studio's output window for error messages output by the XAML parser.

If your app terminates and all you know is that an unhandled exception was thrown during XAML markup parsing, then that could be the result of a reference to a missing resource (that is, a resource whose key exists for Windows Phone Silverlight apps but not for Windows 10 apps, such as some system **TextBlock** Style keys). Or, it could be an exception thrown inside a **UserControl**, a custom control, or a custom layout panel.

A last resort is a binary split. Remove about half of the markup from a Page and re-run the app. You will then know whether the error is somewhere inside the half you removed (which you should now restore in any case) or in the half you did *not* remove. Repeat the process by splitting the half that contains the error, and so on, until you've zeroed in on the issue.

## TargetPlatformVersion

This section explains what to do if, on opening a Windows 10 project in Visual Studio, you see the message "Visual Studio update required. One or more projects require a platform SDK &lt;version&gt; that is either not installed or is included as part of a future update to Visual Studio."

-   First, determine the version number of the SDK for Windows 10 that you have installed. Navigate to **C:\\Program Files (x86)\\Windows Kits\\10\\Include\\&lt;versionfoldername&gt;** and make a note of *&lt;versionfoldername&gt;*, which will be in quad notation, "Major.Minor.Build.Revision".
-   Open your project file for edit and find the `TargetPlatformVersion` and `TargetPlatformMinVersion` elements. Edit them to look like this, replacing *&lt;versionfoldername&gt;* with the quad notation version number you found on disk:

```xml
   <TargetPlatformVersion><versionfoldername></TargetPlatformVersion>
   <TargetPlatformMinVersion><versionfoldername></TargetPlatformMinVersion>
```

## Troubleshooting symptoms and remedies

The remedy information in the table is intended to give you enough info to unblock yourself. You'll find further details about each of these issues as you read through later topics.

| Symptom | Remedy |
|---------|--------|
| The XAML parser or compiler gives the error "_The name "&lt;typename&gt;" does not exist in the namespace […]._" | If &lt;typename&gt; is a custom type then, in your namespace prefix declarations in XAML markup, change "clr-namespace" to "using", and remove any assembly tokens. For platform types, this means that the type doesn't apply to the Universal Windows Platform (UWP), so find the equivalent and update your markup. Examples you might encounter right away are `phone:PhoneApplicationPage` and `shell:SystemTray.IsVisible`. | 
| The XAML parser or compiler gives the error "_The member "&lt;membername&gt;" is not recognized or is not accessible._" or "_The property "&lt;propertyname&gt;" was not found in type [...]._". | These errors will begin to show up after you've ported some type names, such as the root **Page**. The member or property doesn't apply to the UWP, so find the equivalent and update your markup. Examples you might encounter right away are `SupportedOrientations` and `Orientation`. |
| The XAML parser or compiler gives the error "_The attachable property [...] was not found [...]._" or "_Unknown attachable member [...]._". | This is likely to be caused by the type rather than the attached property; in which case, you will already have an error for the type and this error will go away once you fix that. Examples you might encounter right away are `phone:PhoneApplicationPage.Resources` and `phone:PhoneApplicationPage.DataContext`. | 
|The XAML parser or compiler, or a runtime exception, gives the error "_The resource "&lt;resourcekey&gt;" could not be resolved._". | The resource key doesn't apply to Universal Windows Platform (UWP) apps. Find the correct equivalent resource and update your markup. Examples you might encounter right away are system **TextBlock** Style keys such as `PhoneTextNormalStyle`. |
| The C# compiler gives the error "_The type or namespace name '&lt;name&gt;' could not be found [...]_" or "_The type or namespace name '&lt;name&gt;' does not exist in the namespace [...]_" or "_The type or namespace name '&lt;name&gt;' does not exist in the current context_". | This is likely to mean that the compiler doesn't yet know the correct UWP namespace for a type. Use Visual Studio's **Resolve** command to fix that. <br/>If the API is not in the set of APIs known as the universal device family (in other words, the API is implemented in an extension SDK), then use the [Extension SDKs](wpsl-to-uwp-porting-to-a-uwp-project.md).<br/>There may be other cases where port is less straightforward. Examples you might encounter right away are `DesignerProperties` and `BitmapImage`. | 
|When run on the device, the app terminates, or when launched from Visual Studio, you see the error “Unable to activate Windows Runtime 8.x app […]. The activation request failed with error ‘Windows was unable to communicate with the target application. This usually indicates that the target application’s process aborted. […]”. | The problem could be the imperative code running in your own Pages or in bound properties (or other types) during initialization. Or, it could be happening while parsing the XAML file about to be displayed when the app terminated (if launching from Visual Studio, that will be the startup page). Look for invalid resource keys, and/or try some of the guidance in the [Tracking down issues](#tracking-down-issues) section in this topic.|
| _XamlCompiler error WMC0055: Cannot assign text value '&lt;your stream geometry&gt;' into property 'Clip' of type 'RectangleGeometry'_ | In the UWP, the type of the [Microsoft DirectX](/windows/desktop/directx) and XAML C++ UWP app. |
| _XamlCompiler error WMC0001: Unknown type 'RadialGradientBrush' in XML namespace [...]_ | The UWP doesn't have the **RadialGradientBrush** type. Remove the **RadialGradientBrush** from markup and use some other type of [Microsoft DirectX](/windows/desktop/directx) and XAML C++ UWP app. |
| _XamlCompiler error WMC0011: Unknown member 'OpacityMask' on element '&lt;UIElement type&gt;'_ | The UWP [Microsoft DirectX](/windows/desktop/directx) and XAML C++ UWP app. |
| _A first chance exception of type 'System.Runtime.InteropServices.COMException' occurred in SYSTEM.NI.DLL. Additional information: The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))._ | The work you're doing needs to be done on the UI thread. Call the [**CoreWindow.GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread)). |
| An animation is running, but it's having no effect on its target property. | Either make the animation independent, or set `EnableDependentAnimation="True"` on it. See [Animation](wpsl-to-uwp-porting-xaml-and-ui.md). |
| On opening a Windows 10 project in Visual Studio, you see the message "Visual Studio update required. One or more projects require a platform SDK &lt;version&gt; that is either not installed or is included as part of a future update to Visual Studio." | See the [TargetPlatformVersion](#targetplatformversion) section in this topic. |
| A System.InvalidCastException is thrown when InitializeComponent is called in a xaml.cs file. | This can happen when you have more than one xaml file (at least one of which is MRT-qualified) sharing the same xaml.cs file and elements have x:Name attributes that are inconsistent between the two xaml files. Try adding the same name to the same elements in both xaml files, or omit names altogether. | 

The next topic is [Porting XAML and UI](wpsl-to-uwp-porting-xaml-and-ui.md).