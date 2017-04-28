---
author: normesta
Description: This article lists things you need to know before converting your app with the Desktop to UWP Bridge. You may not need to do much to get your app ready for the conversion process.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Prepare
ms.author: normesta
ms.date: 04/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 71a57ca2-ca00-471d-8ad9-52f285f3022e
---

# Prepare to convert an app (Desktop to UWP Bridge)

This article lists the things you need to know before you convert your app by using the Desktop to UWP Bridge. You might not have to do much to get your app ready for the conversion process, but if any of the items below applies to your application, you need to address it before conversion. Remember that the Windows Store handles licensing and automatic updating for you, so you can remove any features that relate to those tasks from your codebase.

+ __Your app uses a version of .NET earlier than 4.6.1__. Only .NET 4.6.1 is supported. You'll have to retarget your app to .NET 4.6.1 before you convert it.

+ __Your app always runs with elevated security privileges__. Your app needs to work while running as the interactive user. Users who install your app from the Windows Store may not be system administrators, so requiring your app to run elevated means that it won't run correctly for standard users.

+ __Your app requires a kernel-mode driver or a Windows service__. The bridge is suitable for an app, but it does not support a kernel-mode driver or a Windows service that needs to run under a system account. Instead of a Windows service, use a [background task](https://msdn.microsoft.com/windows/uwp/launch-resume/create-and-register-a-background-task).

+ __Your app's modules are loaded in-process to processes that are not in your Windows app package__. This isn't permitted, which means that in-process extensions, like [shell extensions](https://msdn.microsoft.com/library/windows/desktop/dd758089.aspx), aren't supported. But if you have two apps in the same package, you can do inter-process communication between them.

+ __Your app calls [SetDllDirectory](https://msdn.microsoft.com/library/windows/desktop/ms686203) or [AddDllDirectory](https://msdn.microsoft.com/library/windows/desktop/hh310513)__. These functions are not currently supported for converted apps. We are working on adding support in a future release. As a workaround, you can copy any .dlls you were locating using these functions to your package root.

+ __Your app uses a custom Application User Model ID (AUMID)__. If your process calls [SetCurrentProcessExplicitAppUserModelID](https://msdn.microsoft.com/library/windows/desktop/dd378422.aspx) to set its own AUMID, then it may only use the AUMID generated for it by the app model environment/Windows app package. You can't define custom AUMIDs.

+ __Your app modifies the HKEY_LOCAL_MACHINE (HKLM) registry hive__. Any attempt by your app to create an HKLM key, or to open one for modification, will result in an access-denied failure. Remember that your app has its own private virtualized view of the registry, so the notion of a user- and machine-wide registry hive (which is what HKLM is) does not apply. You will need to find another way of achieving what you were using HKLM for, like writing to HKEY_CURRENT_USER (HKCU) instead.

+ __Your app uses a ddeexec registry subkey as a means of launching another app__. Instead, use one of the DelegateExecute verb handlers as configured by the various Activatable* extensions in your [app package manifest](https://msdn.microsoft.com/library/windows/apps/br211474.aspx).

+ __Your app writes to the AppData folder with the intention of sharing data with another app__. After conversion, AppData is redirected to the local app data store, which is a private store for each UWP app. Use a different means of inter-process data sharing. For more info, see [Store and retrieve settings and other app data](https://msdn.microsoft.com/windows/uwp/app-settings/store-and-retrieve-app-data).

+ __Your app writes to the install directory for your app__. For example, your app writes to a log file that you put in the same directory as your exe. This isn't supported, so you'll need to find another location, like the local app data store.

+ __Your app installation requires user interaction__. Your app installer must be able to run silently, and it must install all of its prerequisites that aren't on by default on a clean OS image.

+ __Your app uses the Current Working Directory__. At runtime, your converted app won't get the same Working Directory that you previously specified in your desktop .LNK shortcut. You need to change your CWD at runtime if having the correct directory is important for your app to function correctly.

+ __Your app requires UIAccess__. If your application specifies `UIAccess=true` in the `requestedExecutionLevel` element of the UAC manifest, conversion to UWP isn't supported currently. For more info, see [UI Automation Security Overview](https://msdn.microsoft.com/library/ms742884.aspx).

+ __Your app exposes COM objects__. Processes and extensions from within the package can register and use COM & OLE servers, both in-process and out-of-process (OOP).  The Creators Update adds Packaged COM support which provides the ability to register OOP COM & OLE servers that are now visible outside the package.  See [COM Server and OLE Document support for Desktop Bridge](https://blogs.windows.com/buildingapps/2017/04/13/com-server-ole-document-support-desktop-bridge/#bjPyETFgtpZBGrS1.97).

   Packaged COM support works with existing COM APIs, but will not work for application extensions that rely upon directly reading the registry, as the location for Packaged COM is in a private location.

+ __Your app exposes GAC assemblies for use by other processes__. In the current release, your app cannot expose GAC assemblies for use by processes originating from executables external to your Windows app package. Processes from within the package can register and use GAC assemblies as normal, but they will not be visible externally. This means interop scenarios like OLE will not function if invoked by external processes.

+ __Your app is linking C runtime libraries (CRT) in an unsupported manner__. The Microsoft C/C++ runtime library provides routines for programming for the Microsoft Windows operating system. These routines automate many common programming tasks that are not provided by the C and C++ languages. If your app utilizes C/C++ runtime library, you need to ensure it is linked in a supported manner.

	Visual Studio 2015 supports both dynamic linking, to let your code use common DLL files, or static linking, to link the library directly into your code, to the current version of the CRT. If possible, we recommend your app use dynamic linking with VS 2015.

	Support in previous versions of Visual Studio varies. See the following table for details:

	<table>
	<th>Visual Studio version</td><th>Dynamic linking</th><th>Static linking</th></th>
	<tr><td>2005 (VC 8)</td><td>Not supported</td><td>Supported</td>
	<tr><td>2008 (VC 9)</td><td>Not supported</td><td>Supported</td>
	<tr><td>2010 (VC 10)</td><td>Supported</td><td>Supported</td>
	<tr><td>2012 (VC 11)</td><td>Supported</td><td>Not supported</td>
	<tr><td>2013 (VC 12)</td><td>Supported</td><td>Not supported</td>
	<tr><td>2015 (VC 14)</td><td>Supported</td><td>Supported</td>
	</table>

	Note: In all cases, you must link to the latest publically available CRT.

+ __Your app installs and loads assemblies from the Windows side-by-side folder__. For example, your app uses C runtime libraries VC8 or VC9 and is dynamically linking them from Windows side-by-side folder, meaning your code is using the common DLL files from a shared folder. This is not supported. You will need to statically link them by linking to the redistributable library files directly into your code.

+ __Your app uses a dependency in the System32/SysWOW64 folder__. To get these DLLs to work, you must include them in the virtual file system portion of your Windows app package. This ensures that the app behaves as if the DLLs were installed in the **System32**/**SysWOW64** folder. In the root of the package, create a folder called **VFS**. Inside that folder create a **SystemX64** and **SystemX86** folder. Then, place the 32-bit version of your DLL in the **SystemX86** folder, and place the 64-bit version in the **SystemX64** folder.

+ __Your app uses the Dev11 VCLibs framework package__. The VCLibs 11 libraries can be directly installed from the Windows Store if they are defined as a dependency in the Windows app package. To do this, make the following change to your app package manifest: Under the `<Dependencies>` node, add:  
`<PackageDependency Name="Microsoft.VCLibs.110.00.UWPDesktop" MinVersion="11.0.24217.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />`  
During installation from the Windows Store, the appropriate version (x86 or x64) of the VCLibs 11 framework will get installed prior to the installation of the app.  
The dependencies will not get installed if the app is installed by sideloading. To install the dependencies manually on your machine, you must download and install the [VC 11.0 framework packages for Desktop Bridge](https://www.microsoft.com/download/details.aspx?id=53340&WT.mc_id=DX_MVP4025064). For more information on these scenarios, see [Using Visual C++ Runtime in Centennial project](https://blogs.msdn.microsoft.com/vcblog/2016/07/07/using-visual-c-runtime-in-centennial-project/).

+ __Your app contains a custom jump list__. There are several issues and caveats to aware of when using jump lists.

	- __Your app's architecture does not match the OS.__  Jump lists currently do not function correctly if the app and OS architectures do not match (e.g., an x86 app running on x64 Windows). At this time, there is no workaround other than to recompile your app to the matching architecture.

	- __Your app creates jump list entries and calls [ICustomDestinationList::SetAppID](https://msdn.microsoft.com/library/windows/desktop/dd378403(v=vs.85).aspx) or [SetCurrentProcessExplicitAppUserModelID](https://msdn.microsoft.com/library/windows/desktop/dd378422(v=vs.85).aspx)__. Do not programmatically set your AppID in code. Doing so will cause your jump list entries to not appear. If your app needs a custom Id, specify it using the manifest file. Refer to [Manually convert your app to UWP using the Desktop Bridge](desktop-to-uwp-manual-conversion.md) for instructions. The AppID for your application is specified in the *YOUR_PRAID_HERE* section.

	- __Your app adds a jump list shell link that references an executable in your package__. You cannot directly launch executables in your package from a jump list (with the exception of the absolute path of an app’s own .exe). Instead, register an app execution alias (which allows your converted app to start via a keyword as though it were on the PATH) and set the link target path to the alias instead. For details on how to use the appExecutionAlias extension, see [Integrate your app with Windows 10 (Windows Desktop Bridge)](desktop-to-uwp-extensions.md). Note that if you require assets of the link in jump list to match the original .exe, you will need to set assets such as the icon using [**SetIconLocation**](https://msdn.microsoft.com/library/windows/desktop/bb761047(v=vs.85).aspx) and the display name with PKEY_Title like you would for other custom entries.

	- __Your app adds a jump list entries that references assets in the app's package by absolute paths__. The installation path of an app may change when its packages are updated, changing the location of assets (such as icons, documents, executable, and so on). If jump list entries reference such assets by absolute paths, then the app should refresh its jump list periodically (such as on app launch) to ensure paths resolve correctly. Alternatively, use the UWP [**Windows.UI.StartScreen.JumpList**](https://msdn.microsoft.com/library/windows/apps/windows.ui.startscreen.jumplist.aspx) APIs instead, which allow you to reference string and image assets using the package-relative ms-resource URI scheme (which is also language, DPI, and high contrast aware).

+ __Your app starts a utility to perform tasks__. Avoid starting command utilities such as PowerShell and Cmd.exe. Apps often start them because they provide a convenient way to obtain information from the operating system, access the registry, or access system capabilities. Use UWP APIs to accomplish these sorts of tasks instead. Those APIs are more performant because they don’t need a separate executable to run, but more importantly, they keep app from reaching outside of the package. The app’s design stays consistent with the isolation, trust, and security that comes with a desktop bridge app.

+ __Your app hosts add-ins, plug-ins, or extensions__.   In many cases, COM-style extensions will likely continue to work as long as the extension has not been converted by using the desktop bridge, and it installs as full trust. That's because those installers can use their full-trust capabilities to modify the registry and place extension files wherever your host app expects to find them. However, if those extensions are converted by using the desktop bridge, and then installed as Windows app package, they won't work because each package (the host app and the extension) will be isolated from one another. If you intend to convert your extensions, or you plan to encourage your contributors to convert them, consider how you might facilitate communication between the host app package and any extension packages. One way that you might be able to do this is by using an [app service](../launch-resume/app-services.md).

+ __Your app generates code__. Your app can generate code that it consumes in memory, but we recommend that apps avoid writing generated code to disk because the Windows App Certification process can't validate that code prior to app submission.

+ __Your app uses the MAPI API__. The [Outlook MAPI API](https://msdn.microsoft.com/library/office/cc765775.aspx(d=robot)) is not currently supported in desktop bridge apps.

## Next steps

**Convert your app**

See [Convert](desktop-to-uwp-root.md#convert)

**Find answers to specific questions**

Our team monitors these [StackOverflow tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge).

**Give feedback about this article**

Use the comments section below.
