---
title: Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged
description: This topic provides guidance about deploying apps that are packaged with external location, or are unpackaged, and that use the Windows App SDK.
ms.topic: article
ms.date: 08/07/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged

This topic provides guidance about deploying apps that are packaged with external location, or are unpackaged, and that use the Windows App SDK.

* Such apps are desktop apps (not UWP apps).
* They can be written in a .NET language such as C#, or in C++.
* For their user-interface, they can use WinUI 3, or WPF, or WinForms, or another UI framework.

## Overview

Developers of packaged with external location and unpackaged apps are responsible for deploying required Windows App SDK runtime packages to their end users. This can be done either by running the installer or by installing the MSIX packages directly. These options are described in more detail in the [Deploy Windows App SDK runtime](#deploy-windows-app-sdk-runtime) section below.

Packaged with external location and unpackaged apps also have extra runtime requirements. You must initialize access to the Windows App SDK runtime using the Bootstrapper API. In addition, the Dynamic Dependencies API can be used if your app makes use of other framework packages aside from the Windows App SDK. These requirements are described in more detail in the [Runtime requirements for apps packaged with external location or unpackaged](#runtime-requirements-for-apps-packaged-with-external-location-or-unpackaged) section below.

## Prerequisites

* [Download the latest installer & MSIX packages](downloads.md).
* For apps that are packaged with external location or unpackaged, the Visual C++ Redistributable is a requirement. For more info, see [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist).
* **C#**. .NET 6 or later is required. For more info, see [.NET Downloads](https://dotnet.microsoft.com/download/dotnet/).

### Additional prerequisites 

* [Experimental](experimental-channel.md) and [preview](preview-channel.md) versions of the Windows App SDK require that sideloading is enabled to install the runtime.
  - Sideloading is automatically enabled on Windows 10 version 2004 and later.
  - If your development computer or the deployment computer is running **Windows 11**, confirm whether sideloading is enabled:
    - **Settings** > **Privacy & security** > **For developers**. Make sure the **Developer mode** setting is turned on.
  - If your development computer or the deployment computer is running **Windows 10 version 1909 or an earlier version**, confirm whether sideloading is enabled:
    - **Settings** > **Update & Security** > **For developers** > **Use developer features**. Confirm that **Sideload apps** or **Developer mode** is selected.
  - The **Developer mode** setting includes sideloading as well as other features.

    > [!NOTE]
    > If the computer is managed in an enterprise environment, there might be a policy preventing these settings from being changed. In that case if you get an error when you or your app tries to install the Windows App SDK runtime, contact your IT Professional to enable sideloading or **Developer mode**.

## Deploy Windows App SDK runtime

Packaged with external location and unpackaged apps have two options to deploy the Windows App SDK runtime:

- **[Option 1: Use the Installer](#option-1-use-the-installer)**: The silent installer distributes all Windows App SDK MSIX packages. A separate installer is available for each of the `X64`,`X86` and `Arm64` architectures.
- **[Option 2: Install the packages directly](#option-2-deploy-windows-app-sdk-runtime-packages-directly)**: You can have your existing setup or MSI tool carry and install the MSIX packages for the Windows App SDK.

### Option 1: Use the Installer

You can deploy all Windows App SDK runtime packages by running the installer. The installer is available at [Downloads for the Windows App SDK](downloads.md). When running the installer (.exe), you should see an output similar to the following:

```console
Deploying package: Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.1.0_0.318.928.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0

Deploying package: MicrosoftCorporationII.WindowsAppRuntime.Main.1.0_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WindowsAppRuntime.Singleton_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.318.928.0-x6_0.318.928.0_x64__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

Deploying package: Microsoft.WinAppRuntime.DDLM.0.318.928.0-x8_0.318.928.0_x86__8wekyb3d8bbwe
Package deployment result : 0x0
Provisioning result : 0x0

All install operations successful.
```

You can run the installer with no user interaction and suppress all text output with the `--quiet` option:

```console
WindowsAppRuntimeInstall.exe --quiet
```

You can also choose to force update the MSIX packages and shutdown any currently running Windows App SDK processes using the `--force` option. This feature is introduced in 1.1. 

```console
WindowsAppRuntimeInstall.exe --force
```

To see all installer command line options, run `WindowsAppRuntimeInstall --h`.

After the installation is complete, you can run your packaged with external location or unpackaged app. For an example of how to build and run a packaged with external location or unpackaged app that uses the Windows App SDK, see [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

#### Chain the Windows App SDK installer to your app's setup

If you have a custom setup program for your app, you can chain the Windows App SDK setup process in your app's setup process. The Windows App SDK installer currently does not provide a default UI so you will need to chain by using your setup's custom UI.

You can silently launch and track the Windows App SDK setup while showing your own view of the setup progress by using [ShellExecute](/windows/win32/shell/launch). The Windows App SDK installer silently unpacks the Windows App MSIX bundle and calls the [PackageManager.AddPackageAsync](/uwp/api/windows.management.deployment.packagemanager.addpackageasync) method to complete the installation. This is very similar to other runtime installers you may have used, like .NET, Visual C++, or DirectX.

For a code example that demonstrates how to run the Windows App SDK installer from your setup program, see the **RunInstaller** function in the [installer functional tests](https://aka.ms/testruninstaller).

#### Installer sample 

See the sample below to see how to launch the installer from a Win32 setup program without popping up a console window during setup:

> [!div class="button"]
> [Explore Installer sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Installer)

#### Troubleshooting

**Return codes**

The following table lists the most common return codes for the Windows App SDK .exe installer. The return codes are the same for all versions of the installer.

| Return code | Description                                                                         |
|-------------|-------------------------------------------------------------------------------------|
| 0x0         | Package installation or provisioning was completed successfully.                                    |
| 0x80073d06  | One or more packages failed to install.                                             |
| 0x80070005  | System-wide install or provisioning was not possible because the app is not running elevated or the user doing the installation doesn't have admin privileges.                                |

**Installation errors**

If the Windows App SDK installer returns an error during installation, it will return an error code that describes the problem.

- See the list of [common error codes](/windows/win32/appxpkg/troubleshooting#common-error-codes).
- If the error code doesn't provide enough information, you can find more diagnostic information in the [detailed event logs](/windows/win32/appxpkg/troubleshooting#get-diagnostic-information).
- Please [file an issue](https://github.com/microsoft/WindowsAppSDK/issues) with the error code and event logs so the issue can be investigated.

### Option 2: Deploy Windows App SDK runtime packages directly 

As an alternative to using the Windows App SDK installer for deployment to end users, you can manually deploy the MSIX packages through your app's program or MSI. This option can be best for developers who want more control.

For an example that demonstrates how your setup program can install the MSIX packages, see [install.cpp](https://aka.ms/testinstallpackages) in the Windows App SDK installer code.

To check whether the Windows App SDK is installed already (and, if so, what version), you can check for specific package families by calling [PackageManager.FindPackagesForUserWithPackageTypes](/uwp/api/windows.management.deployment.packagemanager.findpackagesforuserwithpackagetypes#windows-management-deployment-packagemanager-findpackagesforuserwithpackagetypes(system-string-system-string-system-string-windows-management-deployment-packagetypes)).

From a *mediumIL* (full trust) unpackaged process (see [Application element](/uwp/schemas/appxpackage/uapmanifestschema/element-application)), you can use the following code to check for a package registered to the current user:

```csharp
using Windows.Management.Deployment;

public class WindowsAppSDKRuntime
{
    public static IsPackageRegisteredForCurrentUser(
        string packageFamilyName,
        PackageVersion minVersion,
        Windows.System.ProcessorArchitecture architecture,
        PackageTypes packageType)
    {
        ulong minPackageVersion = ToVersion(minVersion);

        foreach (var p : PackageManager.FindPackagesForUserWithPackageTypes(
            string.Empty, packageFamilyName, packageType)
        {
            // Is the package architecture compatible?
            if (p.Id.Architecture != architecture)
            {
                continue;
            }

            // Is the package version sufficient for our needs?
            ulong packageVersion = ToVersion(p.Id.Version);
            if (packageVersion < minPackageVersion)
            {
                continue;
            }

            // Success.
            return true;
        }

        // No qualifying package found.
        return false;
    }

    private static ulong ToVersion(PackageVersion packageVersion)
    {
        return ((ulong)packageVersion.Major << 48) |
               ((ulong)packageVersion.Minor << 32) |
               ((ulong)packageVersion.Build << 16) |
               ((ulong)packageVersion.Revision);
    }
}
```

For the scenario above, calling **FindPackagesForUserWithPackageTypes** is preferable to calling **FindPackagesForUser**. That's because you can narrow the search to (for this example), just *framework* or main *packages*. And that avoids matching other types of packages (such as *resource*, *optional*, or *bundle*) which aren't of interest for this example.

To use the current/calling user context, set the *userSecurityId* parameter is to an empty string.

And now some info to help you decide *how* to call the function in the code example above. A properly installed runtime is composed of multiple packages that depend on the system's CPU architecture:
* On an x86 machine: Fwk=\[x86], Main=\[x86], Singleton=\[x86], DDLM=\[x86].
* On an x64 machine: Fwk=\[x86, x64], Main=\[x64], Singleton=\[x64], DDLM=\[x86, x64].
* On an arm64 machine: Fwk=\[x86, x64, arm64], Main=\[arm64], Singleton=\[arm64], DDLM=\[x86, x64, arm64].

For the *Main* and *Singleton* packages, their architecture should match the system's CPU architecture; for example, x64 packages on an x64 system. For the *Framework* package, an x64 system can run both x64 and x86 apps; similarly an arm64 system can run arm64, x64, and x86 apps. A *DDLM* package check is similar to a *Framework* check, except that `PackageType=main`, and the *packagefamilyname* differs, and more than one (different) *packagefamilyname* could be applicable, due to *DDLM*'s unique naming scheme. For more info, see the  [MSIX packages](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/Deployment/MSIXPackages.md) spec. So the checks are more like this:

```csharp
public static bool IsRuntimeRegisteredForCurrentUser(PackageVersion minVersion)
{
    ProcessorArchitecture systemArchitecture = DetectSystemArchitecture();

    return IsFrameworkRegistered(systemArchitecture, minVersion) &&
           IsMainRegistered(systemArchitecture, minVersion) &&
           IsSingletonRegistered(systemArchitecture, minVersion) &&
           IsDDLMRegistered(systemArchitecture, minVersion);
}

private static ProcecssorArchitecture DetectSystemArchitecture()
{
    // ...see the call to IsWow64Process2(), and how the result is used...
    // ...as per `IsPackageApplicable()` in
    // [install.cpp](https://github.com/microsoft/WindowsAppSDK/blob/main/installer/dev/install.cpp)
    // line 99-116...
    // ...WARNING: Use IsWow64Process2 to detect the system architecture....
    // ...         Other similar APIs exist, but don't give reliably accurate results...
}

private static bool IsFrameworkRegistered(ProcessorArchitecture systemArchitecture,
    PackageVersion minVersion)
{
    // Check x86.
    if (!IsPackageRegisteredForCurrentUser(
        global::Microsoft.WindowsAppSDK.Runtime.Packages.Framework.PackageFamilyName,
        minVersion, ProcessorArchitecture.X86,
        PackageTypes.Framework))
    {
        return false;
    }

    // Check x64 (if necessary).
    if ((systemArchitecture == ProcessorArchitecture.X64) || 
        (systemArchitecture == ProcessorArchitcture.Arm64))
    {
        if (!IsPackageRegisteredForCurrentUser(
            global::Microsoft.WindowsAppSDK.Runtime.Packages.Framework.PackageFamilyName,
            minVersion, ProcessorArchitecture.X64,
            PackageTypes.Framework))
        {
            return false;
        }
    }

    // Check arm64 (if necessary).
    if (systemArchitecture == ProcessorArchitcture.Arm64)
    {
        if (!IsPackageRegisteredForCurrentUser(
            global::Microsoft.WindowsAppSDK.Runtime.Packages.Framework.PackageFamilyName,
            minVersion, ProcessorArchitecture.Arm64,
            PackageTypes.Framework))
        {
            return false;
        }
    }

    return true;
}

private static bool IsMainRegistered(ProcessorArchitecture systemArchitecture,
    PackageVersion minVersion)
{
    return IsPackageRegisteredForCurrentUser(
        global::Microsoft.WindowsAppSDK.Runtime.Packages.Main.PackageFamilyName,
        minVersion,
        systemArchitecture,
        PackageTypes.Main);
}

private static bool IsSingletonRegistered(ProcessorArchitecture systemArchitecture,
    PackageVersion minVersion)
{
    return IsPackageRegisteredForCurrentUser(
        global::Microsoft.WindowsAppSDK.Runtime.Packages.Singleton.PackageFamilyName,
        minVersion,
        systemArchitecture,
        PackageTypes.Main);
}

private static bool IsDDLMRegistered(ProcessorArchitecture systemArchitecture,
    PackageVersion minVersion)
{
    // ...similar to IsFrameworkRegistered, but the packageFamilyName is more complicated...
    // ...and no predefined constant is currently available...
    // ...for more details, see
    // https://github.com/microsoft/WindowsAppSDK/blob/main/specs/Deployment/MSIXPackages.md.
}
```

The info and code above covers the basic detection scenario. To detect whether the runtime is provisioned for all users, or to do the above from an App Container, and/or do it from a packaged `mediumIL` process, additional logic is needed.

## Deployment scenarios

- **Installing the Windows App SDK Runtime system-wide**: System-wide install alters the machine for all users, including new users that are added in the future. If the app is running elevated and the user doing the installation has admin privileges, then the installer will register the MSIX packages system-wide by calling the [ProvisionPackageForAllUsersAsync](/uwp/api/windows.management.deployment.packagemanager.provisionpackageforallusersasync). If system-wide registration is not successful, the installation will be performed for the current user doing the installation only. In a managed Enterprise environment, the IT admin should be able to provision for everyone as usual.

- **Architectures redistributed by the Windows App SDK installer**: The Windows App SDK installer is available in the `x86`, `x64`, and `Arm64` architectures. Each version of the installer includes the MSIX packages for just the specific architecture it's named for. For example, if you run the `x86` `WindowsAppRuntimeInstall.exe` on an x64 or and Arm64 device, then that `x86` installer will deploy onto that device only the packages for the x86 architecture.

- **All Windows App SDK MSIX packages are already installed on the computer**: MSIX packages are installed to a system-wide location with only one copy on disk. If an app attempts installation of the Windows App SDK when all the MSIX package dependencies are already installed on the machine, then the installation is not performed.

- **One or more of the Windows App SDK MSIX packages are not installed on the computer**: When deploying the Windows App SDK, always attempt to install all the MSIX packages (framework, main, singleton, DDLM) to ensure that all dependencies are installed and you avoid disruption to the end-user experience.

## Runtime requirements for apps packaged with external location or unpackaged

Apps that are packaged with external location or unpackaged have extra runtime requirements to use the Windows App SDK runtime. This involves referencing and initializing the Windows App SDK Framework package at runtime. In addition, the Dynamic Dependencies API can be used to reference other framework packages outside of the Windows App SDK.

### Use the Windows App SDK runtime

Packaged with external location and unpackaged apps must call the Bootstrapper API to use the Windows App SDK at run time. This is required before the app can use Windows App SDK features such as WinUI, App Lifecycle, MRT Core, and DWriteCore. A bootstrapper component enables packaged with external location and unpackaged apps to perform these important tasks:

- Find and load the Windows App SDK framework package to the app's package graph.
- Initialize the Dynamic Dependency Lifetime Manager (DDLM) for the Windows App SDK framework package. The purpose of the DDLM is to prevent servicing of the Windows App SDK framework package while it is in use by a packaged with external location or unpackaged app. 

The simplest way to load the Windows App SDK runtime for packaged with external location and unpackaged apps is by setting the `<WindowsPackageType>None</WindowsPackageType>` property in your project file (.csproj or .vcxproj). You may also call the bootstrapper API directly in your app's startup code for more control over the initialization. For more details, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md) and [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](tutorial-unpackaged-deployment.md).

Dynamic Dependencies support allows packaged with external location and unpackaged apps to keep their existing deployment mechanism, such as MSI or any installer, and be able to leverage the Windows App SDK in their application. Dynamic dependencies can be used by packaged, packaged with external location, and unpackaged apps; although it is primarily intended to be used by packaged with external location and unpackaged apps.

There is one DDLM for each version and architecture of the Windows App SDK framework package. This means on an `x64` computer, you may have both an `x86` and an `x64` version of the DDLM to support apps of both architectures.

### Reference other framework packages using Dynamic Dependencies API

If you want to use features in other framework packages outside of the Windows App SDK (e.g., DirectX), packaged with external location and unpackaged apps can call the Dynamic Dependencies API. In addition to the bootstrapper component, the Windows App SDK also provides a broader set of C/C++ functions and WinRT classes that implement the *dynamic dependency API*. This API is designed to be used to reference any framework package dynamically at run time. 

For more information, see [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/index.md) and the [Dynamic Dependencies sample](/samples/microsoft/windowsappsdk-samples/dynamicdependencies/)

## Deploy .winmd files to the target machine

Along with your app, we recommend that you go ahead and deploy Windows Metadata (`.winmd`) files. Metadata can be used by various APIs and behaviors at runtime, and its absence can limit or break functionality. For example, metadata can be used to marshal objects across apartments boundaries; and the need to marshal can be a function of machine performance. Since there's no deterministic way to know whether you need metadata, you should deploy `.winmd`s unless you're extremely concerned about size.

## Related topics

* [Deployment architecture for the Windows App SDK](deployment-architecture.md)
- [Windows App SDK deployment guide for framework-dependent packaged apps](deploy-packaged-apps.md)
- [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](tutorial-unpackaged-deployment.md)
- [Check for installed versions of the Windows App SDK runtime](check-windows-app-sdk-versions.md)
- [Remove outdated Windows App SDK runtime versions from your development computer](remove-windows-app-sdk-versions.md)
