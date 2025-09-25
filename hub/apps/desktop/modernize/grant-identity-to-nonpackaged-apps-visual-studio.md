---
title: Grant package identity by packaging with external location in Visual Studio
description: How to use Visual Studio to grant package identity to an unpackaged Win32 app so that you can use modern Windows features in that app.
ms.date: 05/09/2025
ms.topic: how-to
keywords: windows 11, windows 10, desktop, sparse, package, identity, external, location, MSIX, Win32, Visual Studio
ms.localizationpriority: medium
---

# Grant package identity by packaging with external location in Visual Studio

For the motivations behind adding package identity, as well as the differences between building
identity packages in Visual Studio and building them manually, see
[Overview](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps-overview).

This topic describes how to build and register an identity package by using Visual Studio.
For info about building an identity package manually, see
[Grant package identity by packaging with external location manually](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

These are the steps (which this topic describes in detail) to build and register an identity package by using Visual Studio:

1. [Install Visual Studio components](#install-visual-studio-components)
2. [Add a Packaging Project to your solution](#add-a-packaging-project-to-your-solution)
3. [Configure the Packaging Project for signing](#configure-the-packaging-project-for-signing)
4. [Build and test the Packaging Project in Release Mode](#build-and-test-the-packaging-project-in-release-mode)
5. [Register the identity package in your installer](#register-the-identity-package-in-your-installer)
6. [Optional steps](#optional-steps)

## Install Visual Studio components

Creating an identity package in Visual Studio requires the **Windows Application Packaging Project**
and the **Package with External Location** extension.

Install the **Windows Application Packaging Project** components as described in
[Required Visual Studio version and workload](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net#required-visual-studio-version-and-workload).

In Visual Studio, via the **Extensions** > **Manage Extensions** menu item,
install the **Package with External Location** extension.

## Add a Packaging Project to your solution

To add a Packaging Project to your solution with a Project Reference to your application project,
see [Set up the Windows Application Packaging Project in your solution](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net#set-up-the-windows-application-packaging-project-in-your-solution).

Enable packaging with external location by right-clicking the Packaging Project in Solution Explorer,
navigating to the **External Location** tab provided by the **Package with External Location** extension,
enabling the **Package with External Location** option, and saving the changes.

Set the **Package name** and **Publisher display name** fields of the identity package by
double-clicking `Package.appxmanifest` in the Packaging Project to open the visual manifest editor,
navigating to the Packaging tab, and setting the **Package name** and **Publisher display name** fields
to the desired values. See [Localization and Visual Assets](#localization-and-visual-assets) for
scenarios where localization and images may be needed here.

If you have a custom application manifest in your application project, then for info about
synchronizing the values with the values from `Package.appxmanifest`, see
[Add identity metadata to your desktop application manifests](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps#add-identity-metadata-to-your-desktop-application-manifests).
The **Package with External Location** extension uses **App** for the **applicationId**.

If you don't have a custom application manifest, then Visual Studio will produce the appropriate
artifacts during the build process. .NET projects embed a manifest by default, which conflicts with
the produced manifest artifacts. To resolve that, right-click the Project, open **Properties**,
and in the **Application** tab under the **Manifest** section, change **Embed manifest with default settings**
to **Create application without a manifest**.

## Configure the Packaging Project for signing

Generate a certificate for signing by walking through the **Publish** > **Create App Packages** wizard
shown in [Create an app package using the packaging wizard](/windows/msix/package/packaging-uwp-apps#create-an-app-package-using-the-packaging-wizard).

On the first screen, ensure that **Sideloading** is selected, and **Enable automatic updates** is unchecked.
On the second screen, create a self-signed certificate if necessary, then click the **Trust** button
to trust it in the Local Machine Trusted People certificate store. On the final screen,
set **Generate app bundle** to *Never*, and click **Create** to complete the signing configuration.

## Build and test the Packaging Project in Release Mode

To avoid complications from Debug mode dependencies, set the Build configuration to Release mode,
and build the Packaging Project.

Building the Packaging Project produces a **PackageWithExternalLocation** folder in the build output.
That folder contains the `MSIX` file representing the identity package, as well as `Install` and `Remove`
PowerShell scripts to register and unregister the generated identity package locally for testing.

The `Install` PowerShell script registers the generated identity package locally, and connects it
with the `ExternalLocation` sibling folder for testing purposes. To test the application with identity,
run the application executable from the `ExternalLocation` folder.

To associate identity with your application in production, you'll need to ship the generated
identity package with your application, and register it in your installer.

## Register the identity package in your installer

The last step to associate identity with your application is to register the identity package
in your installer and associate it with your application's installation directory.

### PowerShell

Executing powershell.exe with the right parameters is the simplest way to register the package.
The guidance differs for per-user installations vs. machine-wide installations.

#### Per-User (PowerShell)

To register the identity package during a per-user installation:

```Console
powershell.exe -NoLogo -NoProfile -NonInteractive -WindowStyle Hidden -ExecutionPolicy Bypass -Command "Add-AppxPackage -Path <PackagePath> -ExternalLocation <ExternalLocation>"
```

* Set `<PackagePath>` to the absolute path of the signed identity package producedin the previous step
(with the file name).
* Set `<ExternalLocation>` to the absolute path of your application's installation directory
(without any executable names).

To unregister the identity package during a per-user uninstallation:

```Console
powershell.exe -NoLogo -NoProfile -NonInteractive -WindowStyle Hidden -ExecutionPolicy Bypass -Command "Get-AppxPackage <PackageName> | Remove-AppxPackage"
```

* Set `<PackageName>` to the package name you defined in your identity package manifest
(the **Name** attribute of the **Identity** element)

#### Per-Machine (PowerShell)

To register the identity package during a machine-wide installation:

```Console
powershell.exe -NoLogo -NoProfile -NonInteractive -WindowStyle Hidden -ExecutionPolicy Bypass -Command "Add-AppxPackage -Stage <PackagePath> -ExternalLocation <ExternalLocation>; Add-AppxProvisionedPackage -Online -PackagePath <PackagePath>"
```

* Set `<PackagePath>` to the absolute path of the signed identity package produced in the previous step
(with the file name).
* Set `<ExternalLocation>` to the absolute path of your application's installation directory
(without any executable names).

To unregister the identity package during a machine-wide uninstallation:

```Console
powershell.exe -NoLogo -NoProfile -NonInteractive -WindowStyle Hidden -ExecutionPolicy Bypass -Command "$packages = Get-AppxPackage <PackageName>; foreach ($package in $packages) { Remove-AppxProvisionedPackage -PackageName $package.PackageFullName -Online }; foreach ($package in $packages) { Remove-AppxPackage -Package $package.PackageFullName -AllUsers }
```

* Set `<PackageName>` to the package name you defined in your identity package manifest
(the **Name** attribute of the **Identity** element)

### PackageManager APIs

If you'd rather call OS APIs to register and unregister the identity package, the PackageManager API
provides equivalent functionality to PowerShell. The guidance differs for per-user installations vs.
machine-wide installations.

Below are snippets that demonstrate the API. For production-ready code in C# and C++, see
[Sample apps](#sample-apps).

#### Per-User (PackageManager)

The code listing below demonstrates registering the identity package by using the
[**AddPackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync)
method and unregistering the identity package by using the
[**RemovePackageAsync**](/uwp/api/windows.management.deployment.packagemanager.removepackageasync)
method.

```csharp
using Windows.Management.Deployment;

...

// Register the identity package during install

var externalUri = new Uri(externalLocation);
var packageUri = new Uri(packagePath);

var packageManager = new PackageManager();

var options = new AddPackageOptions();
options.ExternalLocationUri = externalUri;

await packageManager.AddPackageByUriAsync(packageUri, options);

...

// Unregister the identity package during uninstall

var packageManager = new PackageManager();

var packages = packageManager.FindPackagesForUserWithPackageTypes("", "<IdentityPackageFamilyName>", PackageType.Main);
foreach (var package in packages)
{
  await packageManager.RemovePackageAsync(package.Id.FamilyName);
}
```

Note the below important details about this code:

* Set `externalLocation` to the absolute path of your application's installation directory
(without any executable names)
* Set `packagePath` to the absolute path of the signed identity package produced in the previous step
(with the file name)
* The `<IdentityPackageFamilyName>` can be found in the visual manifest editor in Visual Studio
under **Packaging** > **Package family name**.

#### Per-Machine (PackageManager)

The code listing below demonstrates registering the identity package by using the
[**StagePackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.stagepackagebyuriasync) and 
[**ProvisionPackageForAllUsersAsync**](/uwp/api/windows.management.deployment.packagemanager.provisionpackageforallusersasync) methods and unregistering the identity package by using the
[**RemovePackageAsync**](/uwp/api/windows.management.deployment.packagemanager.removepackageasync) method.

```csharp
// Register the identity package during install

var externalUri = new Uri(externalLocation);
var packageUri = new Uri(packagePath);

var packageManager = new PackageManager();

var options = new StagePackageOptions();
options.ExternalLocationUri = externalUri;

await packageManager.StagePackageByUriAsync(packageUri, options);
await packageManager.ProvisionPackageForAllUsersAsync(packageFamilyName);

...

// Unregister the identity package during uninstall

var packageManager = new PackageManager();
var packages = packageManager.FindPackagesForUserWithPackageTypes("", "<IdentityPackageFamilyName>", PackageType.Main);
foreach (var package in packages)
{
  await packageManager.DeprovisionPackageForAllUsersAsync(package.Id.FamilyName);
  await packageManager.RemovePackageAsync(package.Id.FamilyName, RemovalOptions.RemoveForAllUsers);
}
```

Note the below important details about this code:

* Set `externalLocation` to the absolute path of your application's installation directory
(without any executable names)
* Set `packagePath` to the absolute path of the signed identity package produced in the previous step
(with the file name)
* The `<IdentityPackageFamilyName>` can be found in the visual manifest editor in Visual Studio
under **Packaging** > **Package family name**.

## Sample apps

For fully functional C# and C++ apps that demonstrate how to register an identity package, see the [PackageWithExternalLocation](https://aka.ms/sparsepkgsample) samples.

## Optional steps

### Localization and Visual Assets

Some features that understand package identity might result in strings and images from your
identity package manifest being displayed in the Windows OS. For example:

* An application that uses camera, microphone, or location APIs will have a dedicated control toggle
in Windows Privacy Settings along with a brokered consent prompt that users can use to grant or deny
access to those sensitive resources.
* An application that registers a share target will show up in the share dialog.

To localize the strings in the identity package manifest, see
[Localize the manifest](/windows/uwp/app-resources/using-mrt-for-converted-desktop-apps-and-games#phase-1-localize-the-manifest).

When providing paths to images in the `VisualElements` attributes in the identity package manifest,
the provided paths should be relative paths within your application's installation directory that will
resolve to a .png, .jpg, or .jpeg image. The attribute names indicate the expected dimensions of the
images (150x150 and 40x40).
