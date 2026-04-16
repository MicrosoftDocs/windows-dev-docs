---
title: Publish a Command Palette extension to Microsoft Store
description: Step-by-step guide to publishing your Command Palette extension to the Microsoft Store using MSIX packages and Partner Center.
ms.date: 04/10/2026
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to publish my Command Palette extension to the Microsoft Store.
---

# Publish to Microsoft Store

You can publish Command Palette extensions to the Microsoft Store. The publishing process is similar to other apps or extensions. You create a new submission in Partner Center and upload your `.msix` package. Command Palette automatically discovers your extension when users install it from the Microsoft Store.

Registration as an individual developer is free. To get started, visit the [Microsoft Store developer page](https://developer.microsoft.com/en-us/microsoft-store) to create your account and learn about the publishing process.

> [!NOTE]
> **MSIX packages explained**
> MSIX is Microsoft's modern app packaging format that provides secure installation, automatic updates, and clean uninstallation. It replaces older formats like MSI and ensures your extension integrates properly with Windows security and deployment features.

> [!NOTE]
> This guide provides basic Microsoft Store publishing steps specific to Command Palette extensions. For comprehensive Microsoft Store publishing guidance, including detailed submission requirements, certification processes, and best practices, see [Publish Windows apps and games](/windows/apps/publish/).

## Prerequisites

> [!IMPORTANT]
> **What is Partner Center?**
> Partner Center is Microsoft's portal for app developers to manage Microsoft Store submissions, track analytics, and handle app certification.

- [Register as a Windows app developer in Partner Center](/windows/apps/publish/partner-center/partner-center-developer-account)
- Create all required app icons and ensure they're properly sized ([Create icons using Visual Studio's asset generation tool](/windows/apps/design/style/iconography/visual-studio-asset-generation))

> [!TIP]
>
> - [List of icons and variations](/windows/apps/design/style/iconography/app-icon-construction#complete-list-of-icons-and-variations)
> - Make sure you generate the following files:
>
> | File Name                | Size       |
> |--------------------------|------------|
> | Square44x44Logo          | 44×44      |
> | SmallTile                | 71×71      |
> | Square150x150Logo        | 150×150    |
> | LargeTile                | 310×310    |
> | Wide310x150Logo          | 310×150    |
> | SplashScreen             | 620×300    |
> | StoreLogo                | 50×50      |

## Set up Microsoft Store

1. Go to the [Microsoft Partner Center](https://partner.microsoft.com/dashboard/home).
1. Under **Workspaces**, select **Apps and games**.
1. Select **+ New Product**.
1. Select **MSIX or PWA app**.
1. Create or reserve a product name.
1. Start the submission and complete as much as you can until you reach the **Packages** section.
1. In the left navigation, under **Product Management**, select **Product identity**.
1. Copy the following values for use in the next steps:

> [!IMPORTANT]
> **Copy these values from Partner Center:**
>
> - **Package/Identity/Name**: `_________________`
> - **Package/Identity/Publisher**: `_________________`
> - **Package/Properties/PublisherDisplayName**: `_________________`
>
> Use these exact values in the code examples below.

## Prepare the extension

1. In your IDE, open `<ExtensionName>\Package.appxmanifest`.
1. Replace the values with the information you copied from Partner Center.

```xml
<Identity
    Name="YOUR_PACKAGE_IDENTITY_NAME_HERE" 
    Publisher="YOUR_PACKAGE_IDENTITY_PUBLISHER_HERE" 
    Version="0.0.1.0" />

  <Properties>
    <DisplayName>YOUR_EXTENSION_DISPLAY_NAME</DisplayName> <!-- Replace with the reserved name from Partner Center -->
    <PublisherDisplayName>YOUR_PUBLISHER_DISPLAY_NAME_HERE</PublisherDisplayName> <!-- Replace with your Package/Properties/PublisherDisplayName -->
    <Logo>Assets\StoreLogo.png</Logo> <!-- Confirm that this image exist -->
  </Properties>
```

1. In your IDE, open `<ExtensionName>.csproj`.
1. Locate a `PropertyGroup` element (with no conditions) and add the following properties by using your Partner Center values:

```xml
    <AppxPackageIdentityName>YOUR_PACKAGE_IDENTITY_NAME_HERE</AppxPackageIdentityName>
    <AppxPackagePublisher>YOUR_PACKAGE_IDENTITY_PUBLISHER_HERE</AppxPackagePublisher>
    <AppxPackageVersion>0.0.1.0</AppxPackageVersion>
```

1. Update the `ItemGroup` for images to get all of them by removing:

```xml
  <ItemGroup>
    <Content Include="Assets\SplashScreen.scale-200.png" />
    <Content Include="Assets\LockScreenLogo.scale-200.png" />
    <Content Include="Assets\Square150x150Logo.scale-200.png" />
    <Content Include="Assets\Square44x44Logo.scale-200.png" />
    <Content Include="Assets\Square44x44Logo.altform-unplated_targetsize-32.png" />
    <Content Include="Assets\Wide310x150Logo.scale-200.png" />
  </ItemGroup>
```

with

```xml
  <ItemGroup>
    <Content Include="Assets\**\*.png" />
  </ItemGroup>
```

1. Under the `<ItemGroup>` you just updated, add:

```xml
  <Target Name="PrepareAssets" BeforeTargets="BeforeBuild">
    <!-- Copy scale-specific assets to base filenames expected by store validations -->
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\Square150x150Logo.scale-200.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\Square150x150Logo.png"
          SkipUnchangedFiles="true" />
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\Square44x44Logo.scale-200.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\SmallTile.png"
          SkipUnchangedFiles="true" />
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\Wide310x150Logo.scale-200.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\Wide310x150Logo.png"
          SkipUnchangedFiles="true" />
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\SplashScreen.scale-200.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\SplashScreen.png"
          SkipUnchangedFiles="true" />
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\Square150x150Logo.scale-200.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\LargeTile.png"
          SkipUnchangedFiles="true" />
    <Copy SourceFiles="$(MSBuildProjectDirectory)\Assets\StoreLogo.scale-100.png"
          DestinationFiles="$(MSBuildProjectDirectory)\Assets\StoreLogo.png"
          SkipUnchangedFiles="true" />
  </Target>
```

## Build MSIX

1. In the terminal, move to the `<ExtensionName>\<ExtensionName>` directory.
1. Create an x64 build MSIX with the following command:

   ```powershell
   dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=x64 -p:AppxPackageDir="AppPackages\x64\"

   ```

1. Create an ARM64 build MSIX with the following command:

   ```powershell
   dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=ARM64 -p:AppxPackageDir="AppPackages\ARM64\"
   ```

> [!NOTE]
> You need the `AppxPackageDir="AppPackages\x64\"` setting so that the ARM64 build doesn't overwrite the x64 build.

1. Locate the MSIX files:

   ```powershell
   dir AppPackages -Recurse -Filter "*.msix"
   ```

> [!TIP]
> If you don't see your MSIX files, try `dir bin\ -Recurse -Filter "*.msix"`.

1. Note the locations of the `<ExtensionName>_<VersionNumber>_x64.msix` and `<ExtensionName>_<VersionNumber>_arm64.msix` files.

1. In your current location in the directory, create a new `bundle_mapping.txt` file and include the following content, updating the paths to your MSIX files:

   ```text
   [Files]
   "AppPackages\<ExtensionName>_<VersionNumber>_x64_Test\<ExtensionName>_<VersionNumber>_x64.msix" "<ExtensionName>_<VersionNumber>_x64.msix"
   "AppPackages\t<ExtensionName>_<VersionNumber>_arm64_Test\<ExtensionName>_<VersionNumber>_arm64.msix" "<ExtensionName>_<VersionNumber>_arm64.msix"
   ```

1. Create a bundle that combines both architectures into a single package for Microsoft Store submission. Update the `<ExtensionName>` and `<VersionNumber>`:

   ```powershell
   makeappx bundle /v /d bin\Release\ /p <ExtensionName>_<VersionNumber>_Bundle.msixbundle
   ```

   > [!NOTE]
   > If `makeappx` isn't recognized, find it on your machine:
   >
   > ```powershell
   > $arch = switch ($env:PROCESSOR_ARCHITECTURE) { "AMD64" { "x64" } "x86" { "x86" } "ARM64" { "arm64" } default { "x64" } }; Write-Host "Detected: $arch"; $found = Get-ChildItem "C:\Program Files (x86)\Windows Kits\10\bin\*\$arch\makeappx.exe" -ErrorAction SilentlyContinue | Sort-Object Name -Descending | Select-Object -First 1; if ($found) { Write-Host "SUCCESS: $($found.FullName)" -ForegroundColor Green; $found.FullName } else { Write-Host "Not found for $arch" -ForegroundColor Red }
   > ```
   >
   > Then update the following script your machine's path:
   >
   > ```powershell
   > & "<PATH>\makeappx.exe" bundle /f bundle_mapping.txt /p <ExtensionName>_<VersionNumber>_Bundle.msixbundle
   > ```

1. Locate the bundle:

   ```powershell
   dir *.msixbundle
   ```

1. You should find the file: `<ExtensionName>_<VersionNumber>_Bundle.msixbundle`.

### MSIX build validation

Verify your MSIX build is ready by checking:

- ✅ You updated `Package.appxmanifest` with correct Identity and Properties
- ✅ You updated `<ExtensionName>.csproj` with AppxPackage properties
- ✅ Both x64 and ARM64 MSIX files were built successfully
- ✅ The `bundle_mapping.txt` file contains correct paths to both MSIX files
- ✅ The `.msixbundle` file was created without errors
- ✅ You can locate the final bundle file

If any items are missing or failed, review the build commands and check for error messages before continuing.

## Microsoft Store submission

1. Go to the [Microsoft Partner Center](https://partner.microsoft.com/dashboard/home) and open your newly created extension project.
1. In **Packages**, upload the created MSIX bundle.
1. Complete the rest of the submission. The following suggestions can help you:
   1. In **Languages supported in packages**, under your supported language (for example, English (United States)), in **Description**, make sure to include `<ExtensionName> integrates with the Windows Command Palette to...`
   1. In the left navigation, locate **Supplemental info** and select **Additional Testing Information**. Add instructions about needing PowerToys and Command Palette. Here's an [example](https://github.com/chatasweetie/CmdPalExtensions/blob/main/microsoftStoreResources/TesterInstructions.txt).
1. Submit your extension to the store.

After submission, Microsoft reviews your extension for certification. Monitor your submission status in Partner Center and check for email notifications about approval. Once approved, your extension is available in the Microsoft Store within a few hours.

## Related content

- [Publishing overview](publish-extension.md)
- [Publish to WinGet](publish-extension-winget.md)
- [Extension Gallery](extension-gallery.md)
