---
title: Packaging a CLI executable as MSIX
description: Learn how to package an existing command-line executable as an MSIX package for distribution using the winapp CLI.
ms.date: 02/20/2026
ms.topic: how-to
---

# Packaging a CLI executable as MSIX

This guide walks you through packaging an existing command-line executable as an MSIX package for distribution via Windows Package Manager (winget), the Microsoft Store, or direct distribution.

## Prerequisites

- An existing CLI executable (`.exe`) that you want to package
- Windows 10 version 1809 or later

## Steps

### 1. Organize your CLI application

Place your CLI executable and any dependencies in a dedicated folder:

```powershell
mkdir MyCliPackage
cd MyCliPackage
# Copy your CLI executable and dependencies here
```

### 2. Install winapp CLI

```powershell
winget install microsoft.winappcli --source winget
```

### 3. Generate the appxmanifest.xml

```powershell
winapp manifest generate --executable .\yourcli.exe
```

This creates an `appxmanifest.xml` file with default values populated from your executable.

### 4. Configure the manifest

Edit the generated `appxmanifest.xml` to add an execution alias, hide the app from the Start menu, and update application details.

#### 4.1 Add required namespace

Add the `uap5` namespace to the `Package` element:

```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  ...
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap5="http://schemas.microsoft.com/appx/manifest/uap/windows10/5"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap uap5 rescap">
```

#### 4.2 Hide from Start menu

In the `<uap:VisualElements>` element, add `AppListEntry="none"`:

```xml
<uap:VisualElements
    DisplayName="YourApp"
    Description="My Application"
    BackgroundColor="transparent"
    Square150x150Logo="Assets\Square150x150Logo.png"
    Square44x44Logo="Assets\Square44x44Logo.png"
    AppListEntry="none">
</uap:VisualElements>
```

#### 4.3 Add execution alias

Add the extension within the `<Application>` element:

```xml
<Extensions>
  <uap5:Extension Category="windows.appExecutionAlias">
    <uap5:AppExecutionAlias>
      <uap5:ExecutionAlias Alias="yourcli.exe" />
    </uap5:AppExecutionAlias>
  </uap5:Extension>
</Extensions>
```

Replace `yourcli.exe` with the desired command name for your CLI.

#### 4.4 Update application metadata

Update the `Identity`, `Properties`, and `VisualElements` sections to match your CLI application.

### 5. Generate a development certificate (optional)

For local testing and distribution outside the Microsoft Store:

```powershell
cd ~
winapp cert generate
winapp cert install
```

> [!IMPORTANT]
> Keep your development certificate outside the folder containing your CLI executable to avoid accidentally including it in the package.

### 6. Package your CLI

```powershell
winapp pack .\MyCliPackage --cert path\to\devcert.pfx
```

This creates an `.msix` file in the current directory.

> [!TIP]
> - The Microsoft Store signs the MSIX for you, no need to sign before submission.
> - You may need separate MSIX packages for each architecture you support (x64, Arm64).

## Related topics

- [winapp CLI reference](../usage.md)
- [MSIX packaging documentation](/windows/msix/)
