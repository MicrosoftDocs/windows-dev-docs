---
title: Grant package identity by packaging with external location manually
description: Learn how to grant package identity to an unpackaged Win32 app so that you can use modern Windows features in that app.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 10, desktop, sparse package, packaging with external location, package identity, external location, MSIX, Win32, unpackaged app
ms.localizationpriority: medium
ms.custom: RS5
---

# Grant package identity by packaging with external location manually

> **Why do this?** Granting your app a package identity (also called a *sparse package* or *packaging with external location*) unlocks Windows platform features that are otherwise unavailable to unpackaged apps: toast and push notifications, background tasks, app extensions, share targets, file associations, startup tasks, privacy consent prompts, and Windows AI Foundry APIs — all without switching to full MSIX packaging or changing your existing installer.

For more about the motivations behind adding package identity, as well as the differences between building
identity packages in Visual Studio and building them manually, see
[Overview](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps-overview).

This topic describes how to build and register an identity package manually.
For info about building an identity package in Visual Studio, see
[Grant package identity by packaging with external location in Visual Studio](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps-visual-studio).

These are the steps (which this topic describes in detail) to build and register an identity package manually:

1. [Create a package manifest for the identity package](#create-a-package-manifest-for-the-identity-package)
2. [Build and sign the identity package](#build-and-sign-the-identity-package)
3. [Add identity metadata to your desktop application manifests](#add-identity-metadata-to-your-desktop-application-manifests)
4. [Register the identity package in your installer](#register-the-identity-package-in-your-installer)
5. [Optional steps](#optional-steps)

## Create a package manifest for the identity package

The first step to creating an identity package is to create a package manifest based on the below template.
This is an MSIX manifest but is only used for identity and doesn't alter the app's runtime behavior.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Package IgnorableNamespaces="uap uap10"
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities">
  <Identity Name="ContosoPhotoStore" Publisher="CN=Contoso" Version="1.0.0.0" ProcessorArchitecture="neutral" />
  <Properties>
    <DisplayName>Contoso PhotoStore</DisplayName>
    <PublisherDisplayName>Contoso</PublisherDisplayName>
    <Logo>Assets\storelogo.png</Logo>
    <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
  </Properties>
  <Resources>
    <Resource Language="en-us" />
  </Resources>
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19041.0" MaxVersionTested="10.0.26100.0" />
  </Dependencies>
  <Capabilities>
    <rescap:Capability Name="runFullTrust" />
    <rescap:Capability Name="unvirtualizedResources"/>
  </Capabilities>
  <Applications>
    <Application Id="ContosoPhotoStore" Executable="ContosoPhotoStore.exe" uap10:TrustLevel="mediumIL" uap10:RuntimeBehavior="win32App"> 
      <uap:VisualElements AppListEntry="none" DisplayName="Contoso PhotoStore" Description="Contoso PhotoStore App" BackgroundColor="transparent" Square150x150Logo="Assets\Square150x150Logo.png" Square44x44Logo="Assets\Square44x44Logo.png" />
    </Application>
  </Applications>
</Package>
```

Note the below important details about this manifest:

* Fill in the `Identity` element attributes with the details of your application
  * `Name` is the desired name of the identity package
  * `Publisher` must match the `Subject` of the certificate used to sign the application
  * `Version` is the desired version of the identity package. A common practice is to align the
  identity package version with the application version. You will not be able to register a version
  of an identity package on a system if that version of the package is already registered.
  You must first unregister the existing package to reinstall a package with the same version.
  * `ProcessorArchitecture` should be `neutral` as shown so the identity package works across all
  architectures (x86, x64, and ARM64)
* Fill in the `DisplayName` and `PublisherDisplayName` elements with the details of your application
  * Unless you add additional features to the manifest beyond simple identity, these values
  are not displayed anywhere
* Update the `Logo` element to a relative path within your application's installation directory
that will resolve to a .png, .jpg, or .jpeg image
* Ensure the `AllowExternalContent` element is set to `true` as shown which enables reusing your
existing installer
* Set `TargetDeviceFamily` `MinVersion` and `MaxVersionTested` per below:
  * Choose a `MinVersion` value based on your minimum supported OS:
    * `10.0.19041.0` — recommended for maximum reach across Windows 10 and Windows 11
    * `10.0.26100.0` — use this only if your app targets Windows 11, version 24H2 or later exclusively
  * Set `MaxVersionTested` to `10.0.26100.0` as shown
  * Note: The `AllowExternalContent` feature used here was introduced in Windows build 10.0.19041.0.
  If your application runs further downlevel than that, you should perform an OS version check in your
  installer and not register the identity package on OS versions earlier than 10.0.19041.0. See
  [Register the identity package in your installer](#register-the-identity-package-in-your-installer).
* Ensure the `runFullTrust` and `unvirtualizedResources` capabilities are declared as shown for
Win32 compatibility
* Add an `Application` element as shown for each executable associated with your application
  * Ensure `TrustLevel` is `mediumIL` and `RuntimeBehavior` is `win32App` as shown for Win32 compatibility
* The `VisualElements` child element is required, but the `AppListEntry="none"` attribute ensures
the identity package isn't shown among installed apps
  * Update the `DisplayName` and `Description` attributes with relevant details and leave the other
  attributes as shown (the referenced image paths do not need to resolve)
  * See [Localization and Visual Assets](#localization-and-visual-assets) for scenarios where
  localization and images may be needed here.

The identity package created from this manifest will be connected to your application's
installation directory when you register the package in a later step.

## Build and sign the identity package

After you create your identity package manifest, build the identity package using the
[MakeAppx.exe tool](/windows/msix/package/create-app-package-with-makeappx-tool) in the Windows SDK.

```Console
MakeAppx.exe pack /o /d <path to directory that contains manifest> /nv /p <output path>\MyPackage.msix
```

Note: The `/nv` flag is required to bypass validation of referenced file paths in the manifest.

In order to be installed on end user computers, the identity package must be signed with a certificate
that is trusted on the target computer. You can
[create a new self-signed certificate for development purposes](/windows/msix/package/create-certificate-package-signing)
and sign your identity package using [SignTool](/windows/msix/package/sign-app-package-using-signtool),
which is available in the Windows SDK, but a production certificate from an IT Department or a service
like [Azure Trusted Signing](https://azure.microsoft.com/products/trusted-signing) will be required
to register the package on end user computers.

```Console
SignTool.exe sign /fd SHA256 /a /f <path to certificate>\MyCertificate.pfx /p <certificate password> <path to package with external location>\MyPackage.msix
```

> [!IMPORTANT]
> When using a **self-signed certificate** for local development, you must add its **public certificate** to the **Trusted People** certificate store before `Add-AppxPackage` will accept it. Without this step, registration fails with `CERT_E_UNTRUSTEDROOT` (0x800B0109).
>
> Keep the `.pfx` file private — it contains the private key and should only be used for signing. For the trust step, export a `.cer` (public cert only) and import that:
>
> ```PowerShell
> $cert = Get-PfxCertificate -FilePath "<path>\MyCertificate.pfx"
> Export-Certificate -Cert $cert -FilePath "<path>\MyCertificate.cer"
> Import-Certificate -FilePath "<path>\MyCertificate.cer" `
>     -CertStoreLocation Cert:\CurrentUser\TrustedPeople
> ```
>
> For machine-wide installs, use `Cert:\LocalMachine\TrustedPeople` instead (requires elevation).
>
> Production certificates issued by a trusted CA do not require this step.

Note: For how to build and sign the identity package within a CI/CD pipeline with production certificates,
see the [MSIX and CI/CD Pipeline Overview](/windows/msix/desktop/cicd-overview) for examples.

## Add identity metadata to your desktop application manifests

You connect the identity package with your application executables by including
[application manifests](/windows/win32/sbscs/application-manifests) (a.k.a side-by-side or fusion manifests)
with metadata that matches metadata from the identity package manifest.

In Visual Studio, you can add an [application manifest](/windows/win32/sbscs/application-manifests)
to an executable project by opening the **Project** context menu, and selecting **Add** > **New Item** > **Application Manifest File**.


Below is an example application manifest snippet demonstrating the `msix` element required
to connect your binaries with metadata from your identity package.


```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="0.0.0.0" name="ContosoPhotoStore"/>
  <msix xmlns="urn:schemas-microsoft-com:msix.v1"
          publisher="CN=Contoso"
          packageName="ContosoPhotoStore"
          applicationId="ContosoPhotoStore"
        />
</assembly>
```

The attributes of the `msix` element must match these values from the identity package manifest:

* The `packageName` and `publisher` attributes must match the `Name` and `Publisher` attributes in the
[`Identity`](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in your identity package manifest, respectively
* The `applicationId` attribute must match the `Id` attribute of the corresponding
[`Application`](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element in your identity package manifest

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

* Set `<PackagePath>` to the absolute path of the signed identity package produced in the previous step
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
powershell.exe -NoLogo -NoProfile -NonInteractive -WindowStyle Hidden -ExecutionPolicy Bypass -Command "$packages = Get-AppxPackage -AllUsers -Name <PackageName>; $provisioned = Get-AppxProvisionedPackage -Online | Where-Object { $_.DisplayName -eq '<PackageName>' }; foreach ($p in $provisioned) { Remove-AppxProvisionedPackage -PackageName $p.PackageName -Online }; foreach ($package in $packages) { Remove-AppxPackage -Package $package.PackageFullName -AllUsers }"
```

* Set `<PackageName>` to the package name you defined in your identity package manifest
(the **Name** attribute of the **Identity** element)

### PackageManager APIs

If you'd rather call OS APIs to register and unregister the identity package, the PackageManager API
provides equivalent functionality to PowerShell. The guidance differs for per-user installations vs.
machine-wide installations.

Below are snippets that demonstrate the API. For production-ready code in C# and C++, see [Sample apps](#sample-apps).

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

var result = await packageManager.AddPackageByUriAsync(packageUri, options);
if (result.ExtendedErrorCode != 0)
{
    throw new Exception($"Package registration failed: {result.ErrorText} (0x{result.ExtendedErrorCode:X8})");
}

...

// Unregister the identity package during uninstall

var packageManager = new PackageManager();
var packages = packageManager.FindPackagesForUserWithPackageTypes("", "<IdentityPackageFamilyName>", PackageTypes.Main);
foreach (var package in packages)
{
  await packageManager.RemovePackageAsync(package.Id.FullName);
}
```

Note the below important details about this code:

* Set `externalLocation` to the **absolute path** of your application's installation directory
(without any executable names). `new Uri(somePath)` produces a `file:///` URI as required by the API.
* Set `packagePath` to the **absolute path** of the signed identity package produced in the previous step
(with the file name)
* The `<IdentityPackageFamilyName>` can be found by running the `Get-AppxPackage <IdentityPackageName>`
PowerShell command on a system where the identity package is registered. The `PackageFamilyName` property
contains the value to use here.
* Check `result.ExtendedErrorCode` after registration to surface actionable error details. See [Troubleshooting](#troubleshooting) for common error codes.

#### Per-Machine (PackageManager)

The code listing below demonstrates registering the identity package by using the
[**StagePackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.stagepackagebyuriasync) and
[**ProvisionPackageForAllUsersAsync**](/uwp/api/windows.management.deployment.packagemanager.provisionpackageforallusersasync)
methods and unregistering the identity package by using the
[**DeprovisionPackageForAllUsersAsync**](/uwp/api/windows.management.deployment.packagemanager.deprovisionpackageforallusersasync)
and [**RemovePackageAsync**](/uwp/api/windows.management.deployment.packagemanager.removepackageasync) methods.

```csharp
// Register the identity package during install

var externalUri = new Uri(externalLocation);
var packageUri = new Uri(packagePath);

var packageManager = new PackageManager();

var options = new StagePackageOptions();
options.ExternalLocationUri = externalUri;

await packageManager.StagePackageByUriAsync(packageUri, options);
var packageFamilyName = "<IdentityPackageFamilyName>";
await packageManager.ProvisionPackageForAllUsersAsync(packageFamilyName);

...

// Unregister the identity package during uninstall

var packageManager = new PackageManager();

var packages = packageManager.FindPackagesForUserWithPackageTypes("", "<IdentityPackageFamilyName>", PackageTypes.Main);
foreach (var package in packages)
{
  await packageManager.DeprovisionPackageForAllUsersAsync(package.Id.FullName);
  await packageManager.RemovePackageAsync(package.Id.FullName, RemovalOptions.RemoveForAllUsers);
}
```

Note the below important details about this code:

* Set `externalLocation` to the absolute path of your application's installation directory
(without any executable names)
* Set `packagePath` to the absolute path of the signed identity package produced in the previous step
(with the file name)
* The `<IdentityPackageFamilyName>` can be found by running the `Get-AppxPackage <IdentityPackageName>`
PowerShell command on a system where the identity package is registered. The `PackageFamilyName`
property contains the value to use here.

## Troubleshooting

The table below lists the most common errors when registering an identity package and how to fix them.

| Error code | Symptom | Cause | Fix |
|---|---|---|---|
| `0x800B0109` / `CERT_E_UNTRUSTEDROOT` | `Add-AppxPackage` or `AddPackageByUriAsync` fails immediately | Self-signed certificate is not in the **Trusted People** store | Follow the [cert trust step](#build-and-sign-the-identity-package) above to import the public `.cer` into `Cert:\CurrentUser\TrustedPeople` |
| `0x80073CF9` | Registration fails with "version already registered" | The exact same package version is already registered on this machine | Unregister the existing package first (`Remove-AppxPackage` or `RemovePackageAsync`), then re-register |
| `0x80073D54` | Registration succeeds but identity is missing at runtime | `publisher`, `packageName`, or `applicationId` in the app's side-by-side manifest (`msix` element) don't match the identity package manifest | Ensure `Publisher`/`Name`/`Application Id` are identical in both manifests — see [Add identity metadata](#add-identity-metadata-to-your-desktop-application-manifests) |
| Identity absent at runtime (no error) | `Package.Current` is null or `GetPackage()` returns nothing | The `ExternalLocation` path passed at registration doesn't match the directory where the app actually runs | Verify the absolute path passed as `ExternalLocation` is exactly the app's install directory |
| `0x80073CF6` | Registration fails with "manifest invalid" | Manifest XML is malformed or a required attribute is missing | Validate the manifest with `MakeAppx.exe pack` — it reports schema errors. Ensure `uap10:AllowExternalContent` is `true` and `runFullTrust` capability is declared |

> [!TIP]
> For richer diagnostics, check the Windows **Event Viewer** under **Applications and Services Logs > Microsoft > Windows > AppxDeployment-Server**. It logs the full deployment error with context that isn't always surfaced in the API result or PowerShell output.

## Sample apps

See the [PackageWithExternalLocation](https://aka.ms/sparsepkgsample) samples for fully functional
C# and C++ apps that demonstrate how to register and unregister an identity package.

## Optional steps

### Localization and Visual Assets

Some features that understand package identity might result in strings and images from your
identity package manifest being displayed in the Windows OS. For example:

* An application that uses camera, microphone, or location APIs will have a dedicated control toggle
in Windows Privacy Settings along with a brokered consent prompt that users can use to grant or
deny access to those sensitive resources.
* An application that registers a share target will show up in the share dialog.

To localize the strings in the identity package manifest, see
[Localize the manifest](/windows/uwp/app-resources/using-mrt-for-converted-desktop-apps-and-games#phase-1-localize-the-manifest).

When providing paths to images in the `VisualElements` attributes in the identity package manifest,
the provided paths should be relative paths within your application's installation directory that
will resolve to a .png, .jpg, or .jpeg image. The attribute names indicate the expected dimensions
of the images (150x150 and 40x40).
