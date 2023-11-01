---
title: Create your package manifest
description: If you want to submit a software package to the Windows Package Manager repository, start by creating a package manifest.
ms.date: 11/01/2023
ms.topic: article
ms.localizationpriority: medium
---

# Create your package manifest

If you want to submit a software package to the [Windows Package Manager Community Repository](repository.md), start by creating a package manifest. The manifest is a YAML file that describes the application to be installed.

You may either use the [Windows Package Manager Manifest Creator](https://github.com/microsoft/winget-create), the [YAMLCreate](#using-the-yamlcreateps1) PowerShell script, or you can craft a manifest manually following the instructions below.

> [!NOTE]
> See the [Manifest FAQ](#manifest-faq) below for more general high-level information explaining manifests, packages, and versions.

## Options for Manifest Creation

### Using WinGetCreate Utility

You can install the `wingetcreate` utility using the command below.

```powershell
winget install wingetcreate
```

After installation, you can run `wingetcreate new` to create a new package and fill in the prompts. The last option in the **WinGetCreate** prompts is to submit the manifest to the packages repository. If you choose yes, you will automatically submit your Pull Request (PR) to the [Windows Package Manager Community Repository](https://github.com/microsoft/winget-pkgs).

### Using the YAMLCreate.ps1

To help author manifest files, we have provided a YAMLCreate.ps1 powershell script located in the Tools folder on the [Windows Package Manager Community Repository](https://github.com/microsoft/winget-pkgs). You can use the script by cloning the repo on your PC and running the script directly from the **Tools** folder. The script will prompt you for the URL to the installer, then will prompt you to fill in metadata. Similar to using **WinGetCreate**, this script will offer the option to submit your manifest automatically.

## YAML basics

The YAML format was chosen for package manifests because of its relative ease of human readability and consistency with other Microsoft development tools. If you are not familiar with YAML syntax, you can learn the basics at [Learn YAML in Y Minutes](https://learnxinyminutes.com/docs/yaml/).

> [!NOTE]
> Manifests for Windows Package Manager currently do not support all YAML features. Unsupported YAML features include anchors, complex keys, and sets.

## Conventions

These conventions are used in this article:

* To the left of `:` is a literal keyword used in manifest definitions.
* To the right of `:` is a data type. The data type can be a primitive type like **string** or a reference to a rich structure defined elsewhere in this article.
* The notation `[` *datatype* `]` indicates an array of the mentioned data type. For example, `[ string ]` is an array of strings.
* The notation `{` *datatype* `:` *datatype* `}` indicates a mapping of one data type to another. For example, `{ string: string }` is a mapping of strings to strings.

## Manifest contents

A package manifest consists of required items and optional items that can help improve the customer experience of installing your software. This section provides a brief summary of the required manifest schema and complete manifest schemas with examples of each.

Each field in the manifest file must be Pascal-cased and cannot be duplicated.

For a complete list and descriptions of items in a manifest, see the [manifest specification](https://github.com/microsoft/winget-pkgs/tree/master/doc/manifest/schema) in the [Windows Package Manager Community Repository](https://github.com/microsoft/winget-pkgs).

### Minimal required schema

As specified in the [singleton JSON schema](https://github.com/microsoft/winget-cli/blob/master/schemas/JSON/manifests/v1.0.0/manifest.singleton.1.0.0.json),
only certain fields are required. The minimal supported YAML file would look like the example below. The singleton format is only valid for packages containing a single installer and a single locale. If more than one installer or locale is provided, the multiple YAML file format and schema must be used.

The partitioning scheme was added to help with GitHub's UX. Folders with thousands of children do not render well in the browser.

#### [Minimal required schema](#tab/minschema/)

```YAML
PackageIdentifier:  # Publisher.package format.
PackageVersion:     # Version numbering format.
PackageLocale:      # BCP 47 format (e.g. en-US)
Publisher:          # The name of the publisher.
PackageName:        # The name of the application.
License:            # The license of the application.
ShortDescription:   # The description of the application.
Installers: 
 - Architecture:    # Enumeration of supported architectures.
   InstallerType:   # Enumeration of supported installer types (exe, msi, msix, inno, wix, nullsoft, appx).
   InstallerUrl:    # Path to download installation file.
   InstallerSha256: # SHA256 calculated from installer.
ManifestType:       # The manifest file type
ManifestVersion: 1.0.0
```

#### [Example](#tab/minexample/)

Path: manifests / m / Microsoft / WindowsTerminal / 1.6.10571.0 / Microsoft.WindowsTerminal.yaml

```YAML
PackageIdentifier: Microsoft.WindowsTerminal
PackageVersion: 1.6.10571.0
PackageLocale: en-US
Publisher: Microsoft
PackageName: Windows Terminal
License: MIT
ShortDescription: The new Windows Terminal, a tabbed command line experience for Windows.
Installers: 
 - Architecture: x64
   InstallerType: msix
   InstallerUrl: https://github.com/microsoft/terminal/releases/download/v1.6.10571.0/Microsoft.WindowsTerminal_1.6.10571.0_8wekyb3d8bbwe.msixbundle
   InstallerSha256: 092aa89b1881e058d31b1a8d88f31bb298b5810afbba25c5cb341cfa4904d843
   SignatureSha256: e53f48473621390c8243ada6345826af7c713cf1f4bbbf0d030599d1e4c175ee
ManifestType: singleton
ManifestVersion: 1.0.0
```

* * *

### Multiple manifest files

To provide the best user experience, manifests should contain as much meta-data as possible. In order to separate concerns for validating installers
and providing localized metadata, manifests should be split into multiple files. The minimum number of YAML files for this kind of manifest is three. Additional locales should also be provided.

* A [version](https://github.com/microsoft/winget-cli/blob/master/schemas/JSON/manifests/v1.0.0/manifest.version.1.0.0.json) file.
* The [default locale](https://github.com/microsoft/winget-cli/blob/master/schemas/JSON/manifests/v1.0.0/manifest.defaultLocale.1.0.0.json) file.
* An [installer](https://github.com/microsoft/winget-cli/blob/master/schemas/JSON/manifests/v1.0.0/manifest.installer.1.0.0.json) file.
* Additional [locale](https://github.com/microsoft/winget-cli/blob/master/schemas/JSON/manifests/v1.0.0/manifest.locale.1.0.0.json) files.

The example below shows many optional metadata fields and multiple locales. Note the default locale has more requirements than additional locales. In the [show command](../winget/show.md), any required fields that aren't provided for additional locales will display fields from the default locale.

#### [Version file example](#tab/version-example/)

Path: manifests / m / Microsoft / WindowsTerminal / 1.6.10571.0 / Microsoft.WindowsTerminal.yaml

```YAML
PackageIdentifier: "Microsoft.WindowsTerminal"
PackageVersion: "1.6.10571.0"
DefaultLocale: "en-US"
ManifestType: "version"
ManifestVersion: "1.0.0"
```

#### [Default locale file example](#tab/default-locale-example/)

Path: manifests / m / Microsoft / WindowsTerminal / 1.6.10571.0 / Microsoft.WindowsTerminal.locale.en-US.yaml

```YAML
PackageIdentifier: "Microsoft.WindowsTerminal"
PackageVersion: "1.6.10571.0"
PackageLocale: "en-US"
Publisher: "Microsoft"
PublisherUrl: "https://www.microsoft.com/"
PrivacyUrl: "https://privacy.microsoft.com/"
PackageName: "Windows Terminal"
PackageUrl: "https://learn.microsoft.com/windows/terminal/"
License: "MIT"
LicenseUrl: "https://github.com/microsoft/terminal/blob/master/LICENSE"
ShortDescription: "The new Windows Terminal, a tabbed command line experience for Windows."
Tags: 
- "Console"
- "Command-Line"
- "Shell"
- "Command-Prompt"
- "PowerShell"
- "WSL"
- "Developer-Tools"
- "Utilities"
- "cli"
- "cmd"
- "ps"
- "terminal"
ManifestType: "defaultLocale"
ManifestVersion: "1.0.0"
```

#### [Additional locale file example](#tab/additional-locale-example/)

Path: manifests / m / Microsoft / WindowsTerminal / 1.6.10571.0 / Microsoft.WindowsTerminal.locale.fr-FR.yaml

```YAML
PackageIdentifier: "Microsoft.WindowsTerminal"
PackageVersion: "1.6.10571.0"
PackageLocale: "fr-FR"
Publisher: "Microsoft"
ShortDescription: "Le nouveau terminal Windows, une expérience de ligne de commande à onglets pour Windows."
ManifestType: "locale"
ManifestVersion: "1.0.0"
```

#### [Installer file example](#tab/installer-example/)

Path: manifests / m / Microsoft / WindowsTerminal / 1.6.10571.0 / Microsoft.WindowsTerminal.installer.yaml

```YAML
PackageIdentifier: "Microsoft.WindowsTerminal"
PackageVersion: "1.6.10571.0"
Platform: 
 - "Windows.Desktop"
MinimumOSVersion: "10.0.18362.0"
InstallerType: "msix"
InstallModes: 
 - "silent"
PackageFamilyName: "Microsoft.WindowsTerminal_8wekyb3d8bbwe"
Installers: 
 - Architecture: "x64"
   InstallerUrl: "https://github.com/microsoft/terminal/releases/download/v1.6.10571.0/Microsoft.WindowsTerminal_1.6.10571.0_8wekyb3d8bbwe.msixbundle"
   InstallerSha256: 092aa89b1881e058d31b1a8d88f31bb298b5810afbba25c5cb341cfa4904d843
   SignatureSha256: e53f48473621390c8243ada6345826af7c713cf1f4bbbf0d030599d1e4c175ee
 - Architecture: "arm64"
   InstallerUrl: "https://github.com/microsoft/terminal/releases/download/v1.6.10571.0/Microsoft.WindowsTerminal_1.6.10571.0_8wekyb3d8bbwe.msixbundle"
   InstallerSha256: 092aa89b1881e058d31b1a8d88f31bb298b5810afbba25c5cb341cfa4904d843
   SignatureSha256: e53f48473621390c8243ada6345826af7c713cf1f4bbbf0d030599d1e4c175ee
 - Architecture: "x86"
   InstallerUrl: "https://github.com/microsoft/terminal/releases/download/v1.6.10571.0/Microsoft.WindowsTerminal_1.6.10571.0_8wekyb3d8bbwe.msixbundle"
   InstallerSha256: 092aa89b1881e058d31b1a8d88f31bb298b5810afbba25c5cb341cfa4904d843
   SignatureSha256: e53f48473621390c8243ada6345826af7c713cf1f4bbbf0d030599d1e4c175ee
ManifestType: "installer"
ManifestVersion: "1.0.0"
```

* * *

> [!NOTE]
> If your installer is an .exe and it was built using Nullsoft or Inno, you may specify those values instead. When Nullsoft or Inno are specified, the client will automatically set the silent and silent with progress install behaviors for the installer.

## Installer switches

You can often figure out what silent `Switches` are available for an installer by passing in a `-?` to the installer from the command line. Here are some common silent `Switches` that can be used for different installer types.

| Installer | Command  | Documentation |  
| :--- | :-- | :--- |  
| MSI | `/q` | [MSI Command-Line Options](/windows/win32/msi/command-line-options) |
| InstallShield | `/s`  | [InstallShield Command-Line Parameters](https://community.flexera.com/t5/InstallShield-Knowledge-Base/Installshield-Setup-exe-Command-Line-Parameters/ta-p/4270) |
| Inno Setup | `/SILENT or /VERYSILENT` | [Inno Setup documentation](https://jrsoftware.org/ishelp/) |
| Nullsoft | `/S` | [Nullsoft Silent Installers/Uninstallers](https://nsis.sourceforge.io/Docs/Chapter4.html#silent) |

## Tips and best practices

* The package identifier must be unique. You cannot have multiple submissions with the same package identifier. Only one pull request per package version is allowed.
* Avoid creating multiple publisher folders. For example, do not create "Contoso Ltd." if there is already a "Contoso" folder.
* All tools must support a silent install. If you have an executable that does not support a silent install, then we cannot provide that tool at this time.
* Provide as many fields as possible.  The more meta-data you provide the better the user experience will be. In some cases, the fields may not yet be supported by the Windows Package Manager client (winget.exe). For example, the `AppMoniker` field is optional. However, if you include this field, customers will see results associated with the `Moniker` value when performing the [search](../winget/search.md) command (for example, **vscode** for **Visual Studio Code**). If there is only one app with the specified `Moniker` value, customers can install your application by specifying the moniker rather than the fully qualified package identifier.
* The length of strings in this specification should be limited to 100 characters before a line break.
* The `PackageName` should match the entry made in **Add / Remove Programs** to help the correlation with manifests to support **export**, and **upgrade**.
* The `Publisher` should match the entry made in **Add / Remove Programs** to help the correlation with manifests to support **export**, and **upgrade**.
* Package installers in MSI format use [product codes](/windows/win32/msi/product-codes) to uniquely identify applications. The product code for a given version of a package should be included in the manifest to help ensure the best **upgrade** experience.
* When more than one installer type exists for the specified version of the package, an instance of `InstallerType` can be placed under each of the `Installers`.

## Manifest FAQ

### What is a manifest?

Manifests are YAML files containing metadata used by the Windows Package Manager to install and upgrade software on the Windows operating system. There are thousands of these files partitioned under the [manifests directory in the winget-pkgs repository on GitHub](https://github.com/microsoft/winget-pkgs/tree/master/manifests). The Windows Package Manager directory structure had to be partitioned so you don't have to scroll as much in the site when looking for a manifest.

### What is a package?

Think of a package as an application or a software program. Windows Package Manager uses a "PackageIdentifier" to represent a unique package. These are generally in the form of `Publisher.Package`. Sometimes you might see additional values separated by a second period.

### What is a version?

Package versions are associated with a specific release. In some cases you will see a perfectly formed semantic version numbers and in other cases you might see something different. These may be date driven or they might have other characters with some package-specific meaning. The YAML key for a package version is "PackageVersion".

For more information on understanding the directory structure and creating your first manifest, see [Authoring Manifests](https://github.com/microsoft/winget-pkgs/blob/master/AUTHORING_MANIFESTS.md) in the winget-pkgs repo on GitHub.
