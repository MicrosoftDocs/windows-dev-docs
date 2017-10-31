---
author: normesta
Description: This article provides a deeper dive on how the Desktop Bridge works under the covers.
title: Behind the scenes of the Desktop Bridge
ms.author: normesta
ms.date: 05/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: a399fae9-122c-46c4-a1dc-a1a241e5547a
localizationpriority: medium
---

# Behind the scenes of the Desktop Bridge

This article provides a deeper dive on how the Desktop Bridge works under the covers.

A key goal of the Desktop Bridge is to separate application state from system state as much as possible while maintaining compatibility with other apps. The bridge accomplishes this by placing the application inside a Universal Windows Platform (UWP) package, and then detecting and redirecting some changes  it makes to the file system and registry at runtime.

Packages that you create for your desktop app are desktop-only, full-trust applications and are not virtualized or sandboxed. This allows them to interact with other apps the same way classic desktop applications do.

## Installation

App packages are installed under *C:\Program Files\WindowsApps\package_name*, with the executable titled *app_name.exe*. Each package folder contains a manifest (named AppxManifest.xml) that contains a special XML namespace for packaged apps. Inside that manifest file is an ```<EntryPoint>``` element, which references the full-trust app. When that app is launched, it does not run inside an app container, but instead it runs as the user as it normally would.

After deployment, package files are marked read-only and heavily locked down by the operating system. Windows prevents apps from launching if these files are tampered with.

## File system

In order to contain app state, the bridge attempts to capture changes the app makes to AppData. All write to the user's AppData folder (e.g., *C:\Users\user_name\AppData*), including create, delete, and update, are copied on write to a private per-user, per-app location. This creates the illusion that the packaged app is editing the real AppData when it is actually modifying a private copy. By redirecting writes this way, the system can track all file modifications made by the app. This allows the system to clean up those files when the app is uninstalled, thus reducing system "rot" and providing a better app removal experience for the user.

In addition to redirecting AppData, the bridge also dynamically merges Windows' well-known folders (System32, Program Files (x86), etc) with corresponding directories in the app package. Each package contains a folder named "VFS" at its root. Any reads of directories or files in the VFS directory are merged at runtime with their respective native counterparts. For example, an app could contain *C:\Program Files\WindowsApps\package_name\VFS\SystemX86\vc10.dll* as part of its app package, but the file would appear to be installed at *C:\Windows\System32\vc10.dll*.  This maintains compatibility with desktop applications that may expect files to live in non-package locations.

Writes to files/folders in the app package are not allowed. Writes to files and folders that are not part of the package are ignored by the bridge and are allowed as long as the user has permission.

### Common operations

This short reference table shows common file system operations and how the bridge handles them.

Operation | Result | Example
:--- | :--- | :---
Read or enumerate a well-known Windows file or folder | A dynamic merge of *C:\Program Files\package_name\VFS\well_known_folder* with the local system counterpart. | Reading *C:\Windows\System32* returns the contents of *C:\Windows\System32* plus the contents of *C:\Program Files\WindowsApps\package_name\VFS\SystemX86*.
Write under AppData | Copy-on-written to a per-user, per-app location. | AppData is typically *C:\Users\user_name\AppData*.  
Write inside the package | Not allowed. The package is read-only. | Writes under *C:\Program Files\WindowsApps\package_name* are not allowed.
Writes outside the package | Ignored by the bridge. Allowed if the user has permissions. | A write to *C:\Windows\System32\foo.dll* is allowed if the package does not contain *C:\Program Files\WindowsApps\package_name\VFS\SystemX86\foo.dll* and the user has permissions.

### Packaged VFS locations

The following table shows where files shipping as part of your package are overlaid on the system for the app. Your app will perceive these files to be in the listed system locations, when in fact they are in the redirected locations inside *C:\Program Files\WindowsApps\package_name\VFS*. The FOLDERID locations are from the [**KNOWNFOLDERID**](https://msdn.microsoft.com/library/windows/desktop/dd378457.aspx) constants.

System Location | Redirected Location (Under [PackageRoot]\VFS\) | Valid on architectures
 :--- | :--- | :---
FOLDERID_SystemX86 | SystemX86 | x86, amd64
FOLDERID_System | SystemX64 | amd64
FOLDERID_ProgramFilesX86 | ProgramFilesX86 | x86, amd6
FOLDERID_ProgramFilesX64 | ProgramFilesX64 | amd64
FOLDERID_ProgramFilesCommonX86 | ProgramFilesCommonX86 | x86, amd64
FOLDERID_ProgramFilesCommonX64 | ProgramFilesCommonX64 | amd64
FOLDERID_Windows | Windows | x86, amd64
FOLDERID_ProgramData | Common AppData | x86, amd64
FOLDERID_System\catroot | AppVSystem32Catroot | x86, amd64
FOLDERID_System\catroot2 | AppVSystem32Catroot2 | x86, amd64
FOLDERID_System\drivers\etc | AppVSystem32DriversEtc | x86, amd64
FOLDERID_System\driverstore | AppVSystem32Driverstore | x86, amd64
FOLDERID_System\logfiles | AppVSystem32Logfiles | x86, amd64
FOLDERID_System\spool | AppVSystem32Spool | x86, amd64

## Registry

The bridge handles the registry similarly to the file system. App packages contain a registry.dat file, which serves as the logical equivalent of *HKLM\Software* in the real registry. At runtime, this virtual registry merges the contents of this hive into the native system hive to provide a singular view of both. For example, if registry.dat contains a single key "Foo", then a read of *HKLM\Software* at runtime will also appear to contain "Foo" (in addition to all the native system keys).

Only keys under *HKLM\Software* are part of the package; keys under *HKCU* or other parts of the registry are not. Writes to keys or values in the package are not allowed. Writes to keys or values not part of the package are ignored by the bridge and allowed as long as the user has permission.

All writes under HKCU are copy-on-written to a private per-user, per-app location. This provides the same benefits as the bridge's handling of the file system with respect to uninstall cleanup. Traditionally, uninstallers are unable to clean *HKEY_CURRENT_USER* because the registry data for logged out users is unmounted and unavailable.

All writes are kept during package upgrade and only deleted when the app is removed entirely.

### Common operations

This short reference table shows common registry operations and how the bridge handles them.

Operation | Result | Example
:--- | :--- | :---
Read or enumerate *HKLM\Software* | A dynamic merge of the package hive with the local system counterpart. | If registry.dat contains a single key "Foo," at runtime a read of *HKLM\Software* will show the contents of both *HKLM\Software* plus *HKLM\Software\Foo*.
Writes under HKCU | Copy-on-written to a per-user, per-app private location. | The same as AppData for files.
Writes inside the package. | Not allowed. The package is read-only. | Writes under *HKLM\Software* are not allowed if a corresponding key/value exist in the package hive.
Writes outside the package | Ignored by the bridge. Allowed if the user has permissions. | Writes under *HKLM\Software* are allowed as long as a corresponding key/value does not exist in the package hive and the user has the correct access permissions.

## Uninstallation

When a package is uninstalled by the user, all files and folders located under *C:\Program Files\WindowsApps\package_name* are removed, as well as any redirected writes to AppData or the registry that were captured by the bridge.

## Next steps

**Find answers to your questions**

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge).
