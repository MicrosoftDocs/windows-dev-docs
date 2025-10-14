---
title: Publish Command Palette extensions
description: Learn how to publish Command Palette extensions to Microsoft Store and WinGet to share your custom experiences with users.
ms.date: 02/28/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to publish an extension for the Command Palette.
---

# Publish Command Palette extensions

This article provides instructions for Command Palette extensions that were created using the Command Palette template.

You can publish your Command Palette extension through the Microsoft Store, WinGet, or both. This article includes instructions for preparing and publishing your extension to both distribution platforms.

## Microsoft Store

Command Palette extensions can be published to the Microsoft Store. The publishing process is similar to other apps or extensions. You create a new submission in Partner Center and upload your `.msix` package. Command Palette automatically discovers your extension when users install it from the Microsoft Store.

> [!NOTE]
> **MSIX packages explained**
> MSIX is Microsoft's modern app packaging format that provides secure installation, automatic updates, and clean uninstallation. It replaces older formats like MSI and ensures your extension integrates properly with Windows security and deployment features.

Command Palette can't search for or install extensions that are only listed in the store. You can find those extensions by running the following command:

```cmd
ms-windows-store://assoc/?Tags=AppExtension-com.microsoft.commandpalette
```

You can run this command from the "Run commands" command in Command Palette, from the command line, or from the Run dialog.

## Guide to Microsoft Store publishing

> [!NOTE]
> This guide provides basic Microsoft Store publishing steps specific to Command Palette extensions. For comprehensive Microsoft Store publishing guidance, including detailed submission requirements, certification processes, and best practices, see [Publish Windows apps and games](https://learn.microsoft.com/windows/apps/publish/).

### Prerequisites

> [!IMPORTANT]
> **What is Partner Center?**
> Partner Center is Microsoft's portal for app developers to manage Microsoft Store submissions, track analytics, and handle app certification.

- [Register as a Windows app developer in Partner Center](https://learn.microsoft.com/en-us/windows/apps/publish/partner-center/partner-center-developer-account)
- Create all required app icons and ensure they're properly sized ([Create icons using Visual Studio's asset generation tool](https://learn.microsoft.com/en-us/windows/apps/design/style/iconography/visual-studio-asset-generation))

### Set up Microsoft Store

1. Navigate to the [Microsoft Partner Center](https://partner.microsoft.com/dashboard/home).
1. Under **Workspaces**, select **Apps and games**.
1. Select **+ New Product**.
1. Select **MSIX or PWA app**.
1. Create a name or reserve a product name.
1. Start the submission and complete as much as you can until you reach the **Packages** section.
1. In the left navigation, under **Product Management**, select **Product identity**.
1. Note the `Package/Identity/Name`, `Package/Identity/Publisher`, and `Package/Properties/PublisherDisplayName` for the following steps.

### Prepare the extension

1. In your IDE, open `<ExtensionName>\Package.appxmanifest`.
1. Update the publisher and publisher display name:

```xml
<Identity
    Name="TemplateCmdPalExtension" <!-- Replace with Package/Identity/Name -->
    Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" <!-- Replace with Package/Identity/Publisher -->
    Version="0.0.1.0" />

  <Properties>
    <DisplayName>TemplateDisplayName</DisplayName> <!-- Replace with the reserved name -->
    <PublisherDisplayName>A Lone Developer</PublisherDisplayName> <!-- Replace with Package/Properties/PublisherDisplayName -->
    <Logo>Assets\StoreLogo.png</Logo> <!-- Update -->
  </Properties>
```

1. In your IDE, open `<ExtensionName>.csproj`.
1. Locate the `PropertyGroup` element and add the following properties:

```xml
    <AppxPackageIdentityName>Package/Identity/Name</AppxPackageIdentityName>
    <AppxPackagePublisher>CN=########-####-####-####-############</AppxPackagePublisher>
    <AppxPackageVersion>0.0.1.0</AppxPackageVersion>
```

### Build MSIX

1. In the terminal, change to the `<ExtensionName>\<ExtensionName>` directory.
1. Create an x64 build MSIX with the following command:

   ```powershell
   dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=x64
   ```

1. Create an ARM64 build MSIX with the following command:

   ```powershell
   dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=ARM64
   ```

1. Locate the MSIX files:

   ```powershell
   cd <ExtensionName>; dir bin -Recurse -Filter "*.msix"
   ```

1. Note the locations of the `<ExtensionName>_<VersionNumber>_x64.msix` and `<ExtensionName>_<VersionNumber>_arm64.msix` files.

1. Create a new `bundle_mapping.txt` file and include the following content, updating the paths to your MSIX files:

   ```text
   [Files]
   "bin\x64\Release\PATH\<ExtensionName>_<VersionNumber>_x64.msix" "<ExtensionName>_<VersionNumber>_x64.msix"
   "bin\ARM64\Release\PATH\<ExtensionName>_<VersionNumber>_arm64.msix" "<ExtensionName>_<VersionNumber>_arm64.msix"
   ```

1. Create a bundle that combines both architectures into a single package for Microsoft Store submission. Update the `<ExtensionName>` and `<VersionNumber>`:

   ```powershell
   makeappx bundle /v /d bin\Release\ /p <ExtensionName>_<VersionNumber>_Bundle.msixbundle
   ```

   > [!NOTE]
   > If `makeappx` isn't recognized, check which version of Windows SDK you have installed:
   > 
   >    ```powershell
   >    Get-ChildItem "HKLM:\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\Windows\v*" | Select-Object PSChildName
   >    ```
   > 
   >    Then update the following script with the version number:
   > 
   >    ```powershell
   >    & "C:\Program Files (x86)\Windows Kits\<VersionNumber>\bin\[version]\x64\makeappx.exe" bundle /f bundle_mapping.txt /p <ExtensionName>_<VersionNumber>_Bundle.msixbundle
   >    ```

1. Locate the bundle:

   ```powershell
   ls *.msixbundle
   ```

1. You should find the file: `<ExtensionName>_<VersionNumber>_Bundle.msixbundle`.

#### MSIX build validation

Verify your MSIX build is ready by checking:

- ✅ You've updated `Package.appxmanifest` with correct Identity and Properties
- ✅ You've updated `<ExtensionName>.csproj` with AppxPackage properties
- ✅ Both x64 and ARM64 MSIX files were built successfully
- ✅ The `bundle_mapping.txt` file contains correct paths to both MSIX files
- ✅ The `.msixbundle` file was created without errors
- ✅ You can locate the final bundle file

If any items are missing or failed, review the build commands and check for error messages before continuing.


### Microsoft Store submission

1. Navigate to the [Microsoft Partner Center](https://partner.microsoft.com/dashboard/home) and open your newly created extension project.
1. In **Packages**, upload the created MSIX bundle.
1. Complete the rest of the submission. The following suggestions can help you:
   1. In **Languages supported in packages**, under your supported language (for example, English (United States)), in **Description**, make sure to include `<ExtensionName> integrates with the Windows Command Palette to...`
   1. **Tip**: Add additional testing information.
      - In the left navigation, locate **Supplemental info** and select **Additional Testing Information**. Here's an [example](https://github.com/chatasweetie/CmdPalExtensions/blob/main/microsoftStoreResources/TesterInstructions.txt).
1. Submit your extension to the store.

After submission, Microsoft will review your extension for certification. Monitor your submission status in Partner Center and check for email notifications about approval. Once approved, your extension will be available in the Microsoft Store within a few hours.

## WinGet

> [!TIP]
> **What is WinGet?**
> WinGet is Microsoft's open-source command-line package manager for Windows. It's similar to package managers like npm or pip, but for Windows applications. Publishing to WinGet allows users to install your extension with a simple `winget install` command and enables automatic discovery within Command Palette.

Publishing packages to WinGet is the recommended way to share your extensions with users. Extension packages that are listed on WinGet can be discovered and installed directly from Command Palette.

Before submitting your manifest to WinGet, check the following two requirements:

### Add the `windows-commandpalette-extension` tag

Command Palette uses the special `windows-commandpalette-extension` tag to discover extensions. Make sure that your manifest includes this tag so that Command Palette can discover your extension. Add the following code to each `.locale.*.yaml` file in your manifest:

```yaml
Tags:
- windows-commandpalette-extension
```

### Ensure WindowsAppSdk is listed as a dependency

If you're using Windows App SDK, make sure that it's listed as a dependency of your package. Add the following code to your `.installer.yaml` manifest:

```yaml
Dependencies:
  PackageDependencies:
  - PackageIdentifier: Microsoft.WindowsAppRuntime.#.#
```

## Guide to WinGet publishing

### Requirements

- [GitHub CLI](https://cli.github.com/)
- WingetCreate
    ```powershell
    # Install wingetcreate if not already installed
    winget install Microsoft.WingetCreate
    
    # Verify installation
    wingetcreate --version
    ```

### Prepare Github Repo

TODO: add instructions on adding secreats 

### Prepare the project

1. In `<ExtensionName>.csproj`, from the `<PropertyGroup>`:
   - Remove `<PublishProfile>win-$(Platform).pubxml</PublishProfile>`
   - Add `<WindowsPackageType>None</WindowsPackageType>`
1. `cd` into `CmdPalRandomRiddleExtension\CmdPalRandomRiddleExtension\Properties\`


1. `cd` into the directory that contains your `<ExtensionName>.cs`
1. Create a `build-exe.ps1` file, for a simple extension you can copy and customize the following:

**Template: `build-exe.ps1`**
```powershell
# TEMPLATE: PowerShell Build Script for Command Palette Extensions
#
# To use this template for a new extension:
# 1. Copy this file to your extension's project folder as "build-exe.ps1"
# 2. Replace EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
# 2. Replace <VERSION> with your extension version (e.g., 0.0.1.0)
# 3. Update the default version to match your project file's AppxPackageVersion
# 4. Ensure you have a setup-template.iss file in the same directory

param(
    [string]$Configuration = "Release",
    [string]$Version = "<VERSION>",  # UPDATE: Change this to match your project's default version
    [string]$Platform = "x64"
)

$ErrorActionPreference = "Stop"

Write-Host "Building EXTENSION_NAME EXE installer..." -ForegroundColor Green
Write-Host "Version: $Version" -ForegroundColor Yellow

$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectFile = "$ProjectDir\EXTENSION_NAME.csproj"

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path "$ProjectDir\bin") { 
    Remove-Item -Path "$ProjectDir\bin" -Recurse -Force -ErrorAction SilentlyContinue 
}
if (Test-Path "$ProjectDir\obj") { 
    Remove-Item -Path "$ProjectDir\obj" -Recurse -Force -ErrorAction SilentlyContinue 
}

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore $ProjectFile

# Build and publish
Write-Host "Building and publishing application..." -ForegroundColor Yellow
dotnet publish $ProjectFile `
    --configuration $Configuration `
    --runtime "win-$Platform" `
    --self-contained true `
    --output "$ProjectDir\bin\$Configuration\win-$Platform\publish"

if ($LASTEXITCODE -ne 0) { 
    throw "Build failed with exit code: $LASTEXITCODE" 
}

# Check if files were published
$publishDir = "$ProjectDir\bin\$Configuration\win-$Platform\publish"
$fileCount = (Get-ChildItem -Path $publishDir -Recurse -File).Count
Write-Host "✅ Published $fileCount files to $publishDir" -ForegroundColor Green

# Update version in setup.iss
Write-Host "Updating installer script version..." -ForegroundColor Yellow
$setupTemplate = Get-Content "$ProjectDir\setup-template.iss" -Raw
$setupScript = $setupTemplate -replace '#define AppVersion ".*"', "#define AppVersion `"$Version`""
$setupScript | Out-File -FilePath "$ProjectDir\setup.iss" -Encoding UTF8

# Create installer with Inno Setup
Write-Host "Creating installer with Inno Setup..." -ForegroundColor Yellow
$InnoSetupPath = "${env:ProgramFiles(x86)}\Inno Setup 6\iscc.exe"
if (-not (Test-Path $InnoSetupPath)) { 
    $InnoSetupPath = "${env:ProgramFiles}\Inno Setup 6\iscc.exe" 
}

if (Test-Path $InnoSetupPath) {
    & $InnoSetupPath "$ProjectDir\setup.iss"
    
    if ($LASTEXITCODE -eq 0) {
        $installer = Get-ChildItem "$ProjectDir\bin\$Configuration\installer\*.exe" | Select-Object -First 1
        if ($installer) {
            $sizeMB = [math]::Round($installer.Length / 1MB, 2)
            Write-Host "✅ Created installer: $($installer.Name) ($sizeMB MB)" -ForegroundColor Green
        }
    } else {
        throw "Inno Setup failed with exit code: $LASTEXITCODE"
    }
} else {
    Write-Warning "Inno Setup not found at expected locations"
}

Write-Host "🎉 Build completed successfully!" -ForegroundColor Green
```

1. Locate `CLSID`
    1. Open the extension's main `.cs` file (for example, `<ExtensionName>.cs`).
    1. Look for the `[Guid("...")]` attribute above the class declaration.
    1. This GUID is your CLSID - copy it exactly as shown.

       ```csharp
       // Example from <ExtensionName>.cs
       [Guid("0ab5d8ab-b206-4023-99f0-97dde26e14f2")]  // This is the CLSID
       public sealed partial class <ExtensionName> : IExtension
       ```

    > [!NOTE]
    > **What is a CLSID?**
    > A CLSID (Class Identifier) is a unique identifier that Windows uses to identify COM (Component Object Model) components. Each Command Palette extension needs a unique CLSID so Windows can properly register and load your extension. This GUID is automatically generated when you create your extension project.

1. Create a `setup-template.iss` file, for a simple extension you can copy and customize the following:

**Template: `setup-template.iss`**
```ini
; TEMPLATE: Inno Setup Script for Command Palette Extensions
;
; To use this template for a new extension:
; 1. Copy this file to your extension's project folder as "setup-template.iss"
; 2. Replace EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
; 3. Replace DISPLAY_NAME with your extension's display name (e.g., My Extension)
; 4. Replace DEVELOPER_NAME with your name (e.g., Your Name Here)
; 5. Replace CLSID-HERE with a new CLSID for COM registration
; 6. Update the default version to match your project file

#define AppVersion "0.0.1.0"

[Setup]
AppId={{GUID-HERE}}
AppName=DISPLAY_NAME
AppVersion={#AppVersion}
AppPublisher=DEVELOPER_NAME
DefaultDirName={autopf}\EXTENSION_NAME
OutputDir=bin\Release\installer
OutputBaseFilename=EXTENSION_NAME-Setup-{#AppVersion}
Compression=lzma
SolidCompression=yes
MinVersion=10.0.19041

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\DISPLAY_NAME"; Filename: "{app}\EXTENSION_NAME.exe"

[Registry]
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{CLSID-HERE}}"; ValueData: "EXTENSION_NAME"
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{CLSID-HERE}}\LocalServer32"; ValueData: "{app}\EXTENSION_NAME.exe -RegisterProcessAsComServer"
```

Note: you can test this locally by having .NET 9 `dotnet` and Inno Setup (todo add links)

```powershell
# verify .Net 9 is installed
dotnet --version

# verify Inno Setup is installed
Test-Path "${env:ProgramFiles(x86)}\Inno Setup 6\iscc.exe"

# build installer, this will take a while
.\build-exe.ps1 -Version "0.0.1.0"

# verify that <ExtensionName>-Setup-0.0.1.0.exe is listed
Get-ChildItem "bin\Release\installer\" -File
```

TODO: Explain Github Actions briefly

1. `cd ..` up a directory, you should be in the directory that contains `<ExtensionName>.sln`
1. create a new repo:

```powershell
mkdir .github/workflows
```

1. In the `workflows` directory, create a new file called `release-extension.yml`.
1. Add the following content to the new file:

```yml
# TEMPLATE: Extension EXE Installer Build and Release Workflow
# 
# To use this template for a new extension:
# 1. Copy this file to a new workflow file (e.g., release-myextension-exe.yml)
# 2. Replace all instances of DEVELOPER_NAME with your developer name (e.g., Your Name Here)
# 3. Replace all instances of GITHUB_REPO_URL with your GitHub repository URL (e.g., https://github.com/yourusername/YourRepository)
# 4. Replace all instances of DISPLAY_NAME with your display name (e.g., My Extension)
# 5. Replace all instances of EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
# 6. Replace all instances of FOLDER_NAME with your project folder name (e.g., CmdPalMyExtension)
# 7. Replace all instances of GENERATE-NEW-GUID-HERE with your project's CLSID
# 8. Update the default version in the build script to match your project file

name: DISPLAY_NAME - Build EXE Installer

on:
  workflow_dispatch:
    inputs:
      version:
        description: 'Version number (leave empty to auto-detect)'
        required: false
        type: string
      release_notes:
        description: 'What is new in this version'
        required: false
        default: 'New release with latest updates and improvements.'
        type: string

jobs:
  build:
    runs-on: windows-2022
    permissions:
      contents: write
      actions: read
    
    steps:
    - name: Checkout code
      uses: actions/checkout@v4
    
    - name: Setup .NET 9
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '9.0.x'
    
    - name: Install Inno Setup
      run: choco install innosetup -y --no-progress
      shell: pwsh
    
    - name: Get version from project
      id: version
      run: |
        if ("${{ github.event.inputs.version }}" -ne "") {
          $version = "${{ github.event.inputs.version }}"
        } else {
          $projectFile = "FOLDER_NAME/FOLDER_NAME/EXTENSION_NAME.csproj"
          $xml = [xml](Get-Content $projectFile)
          $version = $xml.Project.PropertyGroup.AppxPackageVersion | Select-Object -First 1
          if (-not $version) { throw "Version not found in project file" }
        }
        echo "VERSION=$version" >> $env:GITHUB_OUTPUT
        Write-Host "Using version: $version"
      shell: pwsh
    
    - name: Build EXE installer using PowerShell script
      run: |
        Set-Location "FOLDER_NAME/FOLDER_NAME"
        .\build-exe.ps1 -Version "${{ steps.version.outputs.VERSION }}"
      shell: pwsh
    
    - name: Upload installer artifact
      uses: actions/upload-artifact@v4
      with:
        name: EXTENSION_NAME-installer
        path: FOLDER_NAME/FOLDER_NAME/bin/Release/installer/*.exe
    
    - name: Create GitHub Release
      uses: softprops/action-gh-release@v1
      with:
        tag_name: EXTENSION_NAME-v${{ steps.version.outputs.VERSION }}
        name: "DISPLAY_NAME v${{ steps.version.outputs.VERSION }}"
        body: |
          ## 🎯 DISPLAY_NAME
          
          ${{ github.event.inputs.release_notes }}
          
          ## 📦 Installation
          
          1. Download `EXTENSION_NAME-Setup-${{ steps.version.outputs.VERSION }}.exe`
          2. Run as Administrator
          3. Extension will be available in Command Palette
          
          ## 🔗 More Information
          
          Repository: GITHUB_REPO_URL
        files: FOLDER_NAME/FOLDER_NAME/bin/Release/installer/*.exe
        draft: false
        prerelease: false
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
    
    - name: Build summary
      run: |
        Write-Host "🎉 DISPLAY_NAME Release Complete!" -ForegroundColor Green
        Write-Host "Version: ${{ steps.version.outputs.VERSION }}" -ForegroundColor Yellow
        Write-Host "📁 Installer uploaded to GitHub Release" -ForegroundColor Green
      shell: pwsh
```

This file is a Github Action scrip that does the following:

    - Setup (.NET, Inno Setup)
    - Get Version (simple version detection)
    - Build App (straightforward dotnet publish)
    - Create Installer (simple Inno Setup call)
    - Upload Results (clear artifact + release steps)
1. Update the placeholders in `release-extension.yml`:
   - DEVELOPER_NAME
   - GITHUB_REPO_URL
   - EXTENSION_NAME
   - EXTENSION_NAME
   - FOLDER_NAME
   - GENERATE-NEW-GUID-HERE
1. git commit the 3 new files: `build-exe.ps1`, `setup.iss`,`release-extension.yml`
1. Push changes to Github.
1. Trigger the GitHub Action:

   ```powershell
   gh workflow run release-extension.yml --ref main -f create_release=true -f "release_notes= **First Release of <ExtensionName> Extension for Command Palette**
   The inaugural release of the <ExtensionName> for Command Palette..."
   ```

### WinGet submission

> [!IMPORTANT]
> The first submission must be manual. A GitHub Action template will be linked at the end for post-first submission updates.

**Why manual first?**

- `wingetcreate new` requires interactive input for package details
- GitHub Actions can't provide interactive console input

#### Manual first submission

1. Activate interactive wingetcreate:

   ```powershell
   # Use your actual GitHub release URL (example with version 0.0.1 for first release)
   wingetcreate new "https://github.com/<yourusername>/<ExtensionName>/releases/download/<ExtensionName>-v0.0.1/<ExtensionName>-Setup-0.0.1.exe"
   ```
    Note: FOr the Github Release URL, you can go to the release page, under **Assets** you can right click the .exe file and

1. When `wingetcreate` prompts you, press **Enter** if the suggested response is pulled from the EXE file, for example: `PackageIdentifier`, `PackageVersion`, `Publisher`, etc.
   - **For optional modification questions**, answer **No**:
     - "Would you like to modify the optional default locale fields?" → **No**
     - "Would you like to modify the optional installer fields?" → **No**
     - "Would you like to make changes to this manifest?" → **No**
   - **Final submission question**:
     - "Would you like to submit your manifest to the Windows Package Manager repository?" → **Yes**

After answering "Yes" to submit:

- `wingetcreate` forks the microsoft/winget-pkgs repository to your GitHub account
- Creates a new branch with your package manifests
- Opens a pull request automatically
- Provides the PR URL for tracking

After submitting your pull request, the WinGet team will review your manifest for compliance and accuracy. You can monitor the PR status on GitHub and respond to any feedback from reviewers. Once approved and merged, your extension will be available through WinGet within a few hours.

#### WinGet updates via GitHub Actions

You can use GitHub Actions to update your already submitted projects to WinGet.

Check out how [PowerToys](https://github.com/microsoft/PowerToys/blob/main/.github/workflows/package-submissions.yml) does this.

## Related content

- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
- [PowerToys Command Palette utility](overview.md)
