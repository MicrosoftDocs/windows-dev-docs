---
title: winapp CLI for .NET MAUI
description: Package and sign a .NET MAUI Windows app with winapp CLI by using the generated resizetizer manifest for reliable MSIX creation.
ms.date: 08/14/2026
ms.topic: how-to
ai-usage: ai-assisted

#customer intent: As a .NET MAUI developer, I want to package my MAUI Windows app into a signed MSIX with winapp CLI so that I can install and run it in my own environment.
---

# Package a .NET MAUI Windows app with winapp CLI

This article shows you how to package a .NET MAUI Windows app into a signed MSIX with winapp CLI. A signed MSIX is the package format Windows installs and runs, so producing one is what lets you get your app onto a machine and confirm it works.

MAUI has one packaging detail worth knowing upfront: the source manifest at `Platforms/Windows/Package.appxmanifest` contains `$placeholder$` tokens that packaging tools don't resolve. To create a reliable MSIX, you package with the generated resizetizer manifest that the Windows build produces instead of the source manifest.

You set up your tools, publish the Windows head of your app, package it with the generated resizetizer manifest, sign the package with a development certificate, and then trust the certificate, install the package, and run the app to verify the result.

## Prerequisites

- The .NET SDK with the .NET MAUI Windows workload (`maui-windows`).
- [winapp CLI](../index.md).
- A Windows machine where you can install and run MSIX packages.

## Install the .NET MAUI workload and winapp CLI

Install the .NET MAUI Windows workload and winapp CLI before you build. These tools provide the Windows build target and the packaging commands that the rest of this article uses.

1. Install the .NET SDK and the .NET MAUI Windows workload:

   ```powershell
   winget install Microsoft.DotNet.SDK.10 --source winget
   dotnet workload install maui-windows
   ```

2. Install winapp CLI:

   ```powershell
   winget install Microsoft.winappcli --source winget
   ```

## Create and publish the Windows app

Create a MAUI project, keep only the Windows target, and publish an unpackaged Windows build that winapp CLI can package. If you'd rather start from a finished project, use the [`samples/maui-app`](https://github.com/microsoft/WinAppCli/tree/main/samples/maui-app) sample.

1. Create a MAUI app and keep only the Windows head:

   ```powershell
   dotnet new maui -n mymauiapp --no-restore
   cd mymauiapp

   # The stock template targets every MAUI platform. Keep only Windows when
   # maui-windows is the only installed workload.
   [xml]$project = Get-Content .\mymauiapp.csproj
   $propertyGroup = $project.SelectSingleNode("/*[local-name()='Project']/*[local-name()='PropertyGroup'][1]")
   $project.SelectNodes("/*[local-name()='Project']/*[local-name()='PropertyGroup']/*[local-name()='TargetFrameworks']") |
     ForEach-Object { $_.ParentNode.RemoveChild($_) | Out-Null }
   $windowsTfm = $project.CreateElement("TargetFrameworks", $project.DocumentElement.NamespaceURI)
   $windowsTfm.InnerText = "net10.0-windows10.0.19041.0"
   $propertyGroup.PrependChild($windowsTfm) | Out-Null
   $project.Save((Resolve-Path .\mymauiapp.csproj).Path)
   ```

2. Add a conditional property group to your project to work around [Windows App SDK issue #3337](https://github.com/microsoft/WindowsAppSDK/issues/3337), which can cause the wrong runtime identifier to be resolved during packaging. This is the same workaround documented in [Publish an unpackaged .NET MAUI app for Windows with the CLI](/dotnet/maui/windows/deployment/publish-unpackaged-cli). Add the following property group to `mymauiapp.csproj`:

   ```xml
   <PropertyGroup Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows' and '$(RuntimeIdentifierOverride)' != ''">
     <RuntimeIdentifier>$(RuntimeIdentifierOverride)</RuntimeIdentifier>
   </PropertyGroup>
   ```

3. Publish the Windows head. Pass the runtime identifier through `-p:RuntimeIdentifierOverride=win-x64` instead of `-r win-x64`, so the conditional property group maps the override to `RuntimeIdentifier`:

   ```powershell
   dotnet publish .\mymauiapp.csproj `
     -c Release `
     -f net10.0-windows10.0.19041.0 `
     -p:RuntimeIdentifierOverride=win-x64 `
     -p:WindowsPackageType=None `
     -p:SelfContained=true `
     -p:WindowsAppSDKSelfContained=true `
     --output .\publish\win-x64
   ```

## Package the app with the generated resizetizer manifest

Package the published output by using the generated resizetizer manifest. The Windows build writes a fully resolved manifest under `obj\...\resizetizer\m\`, unlike the source `Platforms/Windows/Package.appxmanifest`, which still contains `$placeholder$` tokens.

1. Resolve the generated resizetizer manifest path and confirm it exists:

   ```powershell
   $manifest = ".\obj\Release\net10.0-windows10.0.19041.0\win-x64\resizetizer\m\Package.appxmanifest"
   if (-not (Test-Path $manifest)) { throw "Generated resizetizer manifest not found: $manifest" }
   ```

2. Generate a development certificate for the manifest:

   ```powershell
   winapp cert generate --manifest $manifest --if-exists overwrite
   ```

3. Package the published output into a signed MSIX:

   ```powershell
   winapp pack .\publish\win-x64 `
     --manifest $manifest `
     --executable mymauiapp.exe `
     --cert .\devcert.pfx
   ```

## Sign the unpackaged executable (optional)

If you also distribute the unpackaged executable, sign it with the same development certificate by using the documented `sign` syntax. Replace `<certificate-password>` with the password that protects your certificate's private key.

```powershell
winapp sign .\publish\win-x64\mymauiapp.exe --cert .\devcert.pfx --cert-password <certificate-password>
```

## Trust the certificate and install the app

Before Windows installs a self-signed MSIX, it must trust the signing certificate. Trust a self-signed certificate only on development machines. Don't use a development certificate to sign packages that you distribute to users.

1. Open PowerShell or Windows Terminal as an administrator, navigate to the project directory, and install the development certificate into the local machine certificate store:

   ```powershell
   winapp cert install .\devcert.pfx
   ```

2. Confirm the MSIX package was created, and capture its path:

   ```powershell
   $msix = Get-ChildItem -Path . -Filter *.msix -Recurse | Select-Object -First 1
   $msix.FullName
   ```

3. Install the package:

   ```powershell
   Add-AppxPackage $msix.FullName
   ```

4. Launch the app from the Start menu and confirm that it opens and runs as expected.

## Validate the sample (for WinAppCli maintainers)

The following validation is for maintainers of the [WinAppCli](https://github.com/microsoft/WinAppCli) repository and its CI, not for packaging your own app. Run it from the repository root to validate the bundled MAUI sample end to end.

Run the sample Pester test:

```powershell
.\scripts\test-samples.ps1 -Samples maui-app
```

This script runs `samples/maui-app/test.Tests.ps1`, which creates a MAUI app from scratch, publishes the Windows output, validates the generated resizetizer manifest path, packages an MSIX with winapp CLI, and signs the unpackaged executable. The same script runs in CI through the `maui-app` matrix entry in `.github/workflows/test-samples.yml`.
