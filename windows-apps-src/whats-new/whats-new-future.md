# Submit What's New entries here

## Features

### EXAMPLE: Page layouts with XAML

We've updated our [XAML page layout](../design/layout/layouts-with-xaml.md) docs with new information on fluid layouts and visual states. These new features allow for greater control over how the position of elements in your app respond and adapt to the available visual space.

![Margins and padding for XAML page layouts](../design/layout/images/xaml-layout-margins-padding.png)

### Private audience

If you want your app’s Store listing to be visible only to selected people that you specify, use the new **Private audience** option. The app will not be discoverable or available to anyone other than people in the group(s) you specify. This option is useful for beta testing, as it lets you distribute your app to testers without anyone else being able to get the app, or even see its Store listing. For more info, see [Choose visibility options](../publish/choose-visibility-options.md).

### C++/WinRT
[C++/WinRT](https://docs.microsoft.com/windows/uwp/cpp-and-winrt-apis/) is a new, entirely standard, modern C++17 language projection for Windows Runtime (WinRT) APIs. It's implemented solely in header files, and designed to provide you with first-class access to the modern Windows API. With C++/WinRT, you can author and consume WinRT APIs using any standards-compliant C++17 compiler. For your C++ applications&mdash;from Win32 to UWP&mdash;use C++/WinRT to keep your code standard, modern, and clean, and your application lightweight and fast.

### Package resource indexing (PRI) APIs and custom build systems
With the [package resource indexing (PRI) APIs](https://docs.microsoft.com/windows/uwp/app-resources/pri-apis-custom-build-systems), you can develop a custom build system for your UWP app's resources. The build system will be able to create, version, and dump package resource index (PRI) files to whatever level of complexity your UWP app needs. If you have a custom build system that currently uses the MakePri.exe command-line tool then, for increased performance and control, we recommend that you switch over to calling the PRI APIs instead of calling MakePri.exe.

### Expanded app manifest capabilities
Several features have been added to the App Package Manifest schema, including: broad file system access, enabling barcode scanners for point-of-service devices, defining a UWP console app, and more. 

See [What's different in Windows 10](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/what-s-changed-in-windows-10) for more details.

## Developer Guidance

### Download and install package updates from the Store

We've updated [Download and install package updates from the Store](../packaging/self-install-package-updates.md) with new guidance and examples about how to download and install package updates without displaying a notification UI to the user, uninstall an optional package, and get info about packages in the download and install queue for your app. These tasks require APIs that were introduced in Windows 10, version 1803.

## Videos

## Samples
