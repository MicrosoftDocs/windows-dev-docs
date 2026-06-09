---
title: Publish a Command Palette extension to WinGet
description: Step-by-step guide to publishing your Command Palette extension to WinGet, including build scripts, GitHub Actions automation, and manifest submission.
ms.date: 04/10/2026
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to publish my Command Palette extension to WinGet.
---

# Publish to WinGet

Publishing to WinGet is the recommended distribution method for Command Palette extensions. When your extension is listed on WinGet with the correct tag, users can discover and install it directly from within Command Palette using the `Search WinGet` command. WinGet also handles updates automatically.

> [!TIP]
> **What is WinGet?**
> WinGet is Microsoft's open-source command-line package manager for Windows. It's similar to package managers like npm or pip, but for Windows applications. When you publish to WinGet, users can install your extension with a simple `winget install` command. It also enables automatic discovery within Command Palette.

## Required manifest configuration

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

This guide covers the WinGet publication process, from preparing your project and creating build scripts to setting up GitHub Actions automation and submitting your first package manifest. You'll learn how to create installer packages, configure automated builds, and navigate the WinGet submission workflow to make your extension easily discoverable and installable for users.

### Requirements

- [GitHub CLI](https://cli.github.com/)
- WingetCreate

    ```powershell
    # Install wingetcreate if not already installed
    winget install Microsoft.WingetCreate
    
    # Verify installation
    wingetcreate --version
    ```

### Prepare the project

1. In `<ExtensionName>.csproj`, from the `<PropertyGroup>`:
   - Remove `<PublishProfile>win-$(Platform).pubxml</PublishProfile>`
   - Add `<WindowsPackageType>None</WindowsPackageType>`
1. Locate `CLSID`
    1. Open the extension's main `.cs` file (for example, `<ExtensionName>.cs`).
    1. Look for the `[Guid("...")]` attribute above the class declaration.
    1. This GUID is your CLSID - Keep note of this because it will be used in the next step

       ```csharp
       // Example from <ExtensionName>.cs
       [Guid("0ab5d8ab-b206-4023-99f0-97dde26e14f2")]  // This is the CLSID
       public sealed partial class <ExtensionName> : IExtension
       ```

    > [!NOTE]
    > **What is a CLSID?**
    > A CLSID (Class Identifier) is a unique identifier that Windows uses to identify COM (Component Object Model) components. Each Command Palette extension needs a unique CLSID so Windows can properly register and load your extension. This GUID is automatically generated when you create your extension project.

1. Make sure that you're in the directory that contains your `<ExtensionName>.cs` for the next two files being created.
1. Create a `setup-template.iss` file. For a simple extension, you can copy and customize the following template:

**Template: `setup-template.iss`**

```ini
; TEMPLATE: Inno Setup Script for Command Palette Extensions
;
; To use this template for a new extension:
; 1. Copy this file to your extension's project folder as "setup-template.iss"
; 2. Replace EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
; 3. Replace DISPLAY_NAME with your extension's display name (e.g., My Extension)
; 4. Replace DEVELOPER_NAME with your name (e.g., Your Name Here)
; 5. Replace CLSID-HERE with extensions CLSID
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

1. Create a `build-exe.ps1` file. For a simple extension, you can copy and customize the following template:

**Template: `build-exe.ps1`**

```powershell
# TEMPLATE: PowerShell Build Script for Command Palette Extensions
#
# To use this template for a new extension:
# 1. Copy this file to your extension's project folder as "build-exe.ps1"
# 2. Update in param():
#   - EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension) 
#   - VERSION with your extension version (e.g., 0.0.1.0)



param(
    [string]$ExtensionName = "UPDATE",  # Change to your extension name
    [string]$Configuration = "Release",
    [string]$Version = "UPDATE",  # Change to your version
    [string]$Platform = @("x64", "arm64")
)

$ErrorActionPreference = "Stop"

Write-Host "Building $ExtensionName EXE installer..." -ForegroundColor GreenWrite-Host "Version: $Version" -ForegroundColor Yellow
Write-Host "Platforms: $($Platforms -join ', ')" -ForegroundColor Yellow


$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectFile = "$ProjectDir\$ExtensionName.csproj"

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

# Build for each platform
foreach ($Platform in $Platforms) {
    Write-Host "`n=== Building $Platform ===" -ForegroundColor Cyan
    
    # Build and publish
    Write-Host "Building and publishing $Platform application..." -ForegroundColor Yellow
    dotnet publish $ProjectFile `
        --configuration $Configuration `
        --runtime "win-$Platform" `
        --self-contained true `
        --output "$ProjectDir\bin\$Configuration\win-$Platform\publish"

    if ($LASTEXITCODE -ne 0) { 
        Write-Warning "Build failed for $Platform with exit code: $LASTEXITCODE"
        continue
    }
# Check if files were published
    $publishDir = "$ProjectDir\bin\$Configuration\win-$Platform\publish"
    $fileCount = (Get-ChildItem -Path $publishDir -Recurse -File).Count
    Write-Host "✅ Published $fileCount files to $publishDir" -ForegroundColor Green

    # Create platform-specific setup script
    Write-Host "Creating installer script for $Platform..." -ForegroundColor Yellow
    $setupTemplate = Get-Content "$ProjectDir\setup-template.iss" -Raw
    
    # Update version
    $setupScript = $setupTemplate -replace '#define AppVersion ".*"', "#define AppVersion `"$Version`""
    
    # Update output filename to include platform suffix
    $setupScript = $setupScript -replace 'OutputBaseFilename=(.*?)\{#AppVersion\}', "OutputBaseFilename=`$1{#AppVersion}-$Platform"
    
    # Update source path for the platform
    $setupScript = $setupScript -replace 'Source: "bin\\Release\\win-x64\\publish', "Source: `"bin\Release\win-$Platform\publish"
    
    # Add architecture settings after [Setup] section
    if ($Platform -eq "arm64") {
        $setupScript = $setupScript -replace '(\[Setup\][^\[]*)(MinVersion=)', "`$1ArchitecturesAllowed=arm64`r`nArchitecturesInstallIn64BitMode=arm64`r`n`$2"
    } else {
        $setupScript = $setupScript -replace '(\[Setup\][^\[]*)(MinVersion=)', "`$1ArchitecturesAllowed=x64compatible`r`nArchitecturesInstallIn64BitMode=x64compatible`r`n`$2"
    }
    
    $setupScript | Out-File -FilePath "$ProjectDir\setup-$Platform.iss" -Encoding UTF8

    # Create installer with Inno Setup
    Write-Host "Creating $Platform installer with Inno Setup..." -ForegroundColor Yellow
    $InnoSetupPath = "${env:ProgramFiles(x86)}\Inno Setup 6\iscc.exe"
    if (-not (Test-Path $InnoSetupPath)) { 
        $InnoSetupPath = "${env:ProgramFiles}\Inno Setup 6\iscc.exe" 
    }

    if (Test-Path $InnoSetupPath) {
        & $InnoSetupPath "$ProjectDir\setup-$Platform.iss"
        
        if ($LASTEXITCODE -eq 0) {
            $installer = Get-ChildItem "$ProjectDir\bin\$Configuration\installer\*-$Platform.exe" -ErrorAction SilentlyContinue | Select-Object -First 1
            if ($installer) {
                $sizeMB = [math]::Round($installer.Length / 1MB, 2)
                Write-Host "✅ Created $Platform installer: $($installer.Name) ($sizeMB MB)" -ForegroundColor Green
            } else {
                Write-Warning "Installer file not found for $Platform"
            }
        } else {
            Write-Warning "Inno Setup failed for $Platform with exit code: $LASTEXITCODE"
        }
    } else {
        Write-Warning "Inno Setup not found at expected locations"
    }
}

Write-Host "`n🎉 Build completed successfully!" -ForegroundColor Green
```

> [!TIP]
> You can test this process locally by installing [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0) and [Inno Setup](https://jrsoftware.org/isdl.php) installed.
>
> ```powershell
> # verify .Net 9 is installed
> dotnet --version
> 
> # verify Inno Setup is installed
> Test-Path "${env:ProgramFiles(x86)}\Inno Setup 6\iscc.exe"
>
> # build installer, this step takes a while
> .\build-exe.ps1 -Version "0.0.1.0"
>
> # verify that <ExtensionName>-Setup-0.0.1.0.exe is listed
> Get-ChildItem "bin\Release\installer\" -File
> ```

### Automate with GitHub Actions

> [!NOTE]
> **What are GitHub Actions?**
> GitHub Actions is a CI/CD platform that automates software workflows directly in your GitHub repository. For Command Palette extensions, GitHub Actions can automatically build your installer whenever you push code changes, create releases, and even submit updates to WinGet - eliminating manual build steps and ensuring consistent, reproducible builds.

Now set up GitHub Actions to automate the build and release process:

1. Run `cd ..` to go up a directory. You should be in the directory that contains `<ExtensionName>.sln`.
1. Create a new repo.

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
# 2. Update Global constants with your data:
# - GITHUB_REPO_URL with your GitHub repository URL (e.g., https://github.com/yourusername/YourRepository)
# - DISPLAY_NAME with your display name (e.g., My Extension)
# - EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
# - FOLDER_NAME with your project folder name (e.g., CmdPalMyExtension)

name: CmdPal Extension - Build EXE Installer

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


# Global constants: UPDATE THESE, example; DISPLAY_NAME: ${{ vars.DISPLAY_NAME || 'CmdPal Name' }}
env:
  DISPLAY_NAME: ${{ vars.DISPLAY_NAME || 'DISPLAY_NAME' }}
  EXTENSION_NAME: ${{ vars.EXTENSION_NAME || 'EXTENSION_NAME' }}
  FOLDER_NAME: ${{ vars.FOLDER_NAME || 'FOLDER_NAME' }}
  GITHUB_REPO_URL: ${{ vars.GITHUB_REPO_URL || 'GITHUB_REPO_URL' }}


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
          $projectFile = "${{ env.FOLDER_NAME }}/${{ env.EXTENSION_NAME }}.csproj"
          $xml = [xml](Get-Content $projectFile)
          $version = $xml.Project.PropertyGroup.AppxPackageVersion | Select-Object -First 1
          if (-not $version) { throw "Version not found in project file" }
        }
        echo "VERSION=$version" >> $env:GITHUB_OUTPUT
        Write-Host "Using version: $version"
      shell: pwsh
    
    - name: Build EXE installers (x64 and ARM64)
      run: |
        Set-Location "${{ env.FOLDER_NAME }}/${{ env.FOLDER_NAME }}"
        .\build-exe.ps1 -Version "${{ steps.version.outputs.VERSION }}" -Platforms @("x64", "arm64")
      shell: pwsh
    
    - name: Upload x64 installer artifact
      uses: actions/upload-artifact@v4
      with:
        name: ${{ env.EXTENSION_NAME }}-x64-installer
        path: ${{ env.FOLDER_NAME }}/bin/Release/installer/*-x64.exe
        if-no-files-found: error

    - name: Upload ARM64 installer artifact
      uses: actions/upload-artifact@v4
      with:
        name: ${{ env.EXTENSION_NAME }}-arm64-installer
        path: ${{ env.FOLDER_NAME }}/bin/Release/installer/*-arm64.exe
        if-no-files-found: warn
    
    - name: Create GitHub Release
      uses: softprops/action-gh-release@v1
      with:
        tag_name: ${{ env.EXTENSION_NAME }}-v${{ steps.version.outputs.VERSION }}
        name: "${{ env.DISPLAY_NAME }} v${{ steps.version.outputs.VERSION }}"
        body: |
          ## 🎯 ${{ env.DISPLAY_NAME }} ${{ steps.version.outputs.VERSION }}
          
          ## What's New
          ${{ github.event.inputs.release_notes }}
          
          ## 📦 Installation
          
          Download the installer for your system architecture:
          
          - **x64 (Intel/AMD)**: `${{ env.DISPLAY_NAME }}-Setup-${{ steps.version.outputs.VERSION }}-x64.exe`
          - **ARM64 (Windows on ARM)**: `${{ env.DISPLAY_NAME }}-Setup-${{ steps.version.outputs.VERSION }}-arm64.exe`
          
          1. Download the appropriate installer from the Assets section below
          2. Run the installer with administrator privileges
          3. The extension will be registered and available in Command Palette
          
          
          ## 🔗 More Information
          
          Repository: ${{ env.GITHUB_REPO_URL }}
        files: ${{ env.FOLDER_NAME }}/bin/Release/installer/*.exe
        draft: false
        prerelease: false
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
    
    - name: Build summary
      run: |
        Write-Host "🎉 ${{ env.DISPLAY_NAME }} Release Complete!" -ForegroundColor Green
        Write-Host "Version: ${{ steps.version.outputs.VERSION }}" -ForegroundColor Yellow
        Write-Host "📁 Installer uploaded to GitHub Release" -ForegroundColor Green
      shell: pwsh
```

This file is a GitHub Action script that does the following tasks:

- Setup (.NET, Inno Setup)
- Get Version (simple version detection)
- Build App (straightforward dotnet publish)
- Create Installer (simple Inno Setup call)
- Upload Results (clear artifact + release steps)

1. Update the placeholders in `release-extension.yml`.
1. Commit the three new files: `build-exe.ps1`, `setup.iss`, and `release-extension.yml`.
1. Push changes to GitHub.
1. Trigger the GitHub Action:

   ```powershell
   gh workflow run release-extension.yml --ref main -f create_release=true -f "release_notes= **First Release of <ExtensionName> Extension for Command Palette**
   The inaugural release of the <ExtensionName> for Command Palette..."
   ```

#### GitHub Actions validation

Verify your GitHub Actions setup by checking:

- ✅ GitHub Action workflow runs successfully without errors
- ✅ Installer EXE is created and uploaded to GitHub Release

**Typical build time**: 5-10 minutes for the GitHub Action to complete.

### WinGet submission

> [!IMPORTANT]
> You must manually submit the first version. `wingetcreate new` requires interactive input for package details

#### Manual first submission

1. Activate interactive wingetcreate:

   ```powershell
   # Use your actual GitHub release URLs 
   wingetcreate new "<PATH TO x64 .exe file>" "<PATH TO arm64 .exe file>"
   ```

   > [!TIP]
   > To get the GitHub Release URL: Go to your release page, under **Assets**, right-click the `.exe` file and select "Copy link address".

1. When `wingetcreate` prompts you, press **Enter** if the suggested response is pulled from the EXE file, for example: `PackageIdentifier`, `PackageVersion`, `Publisher`, and so on.
   - **For optional modification questions**, answer **No**:
     - "Would you like to modify the optional default locale fields?" → **No**
     - "Would you like to modify the optional installer fields?" → **No**
     - "Would you like to make changes to this manifest?" → **No**
   - **Final submission question**:
     - "Would you like to submit your manifest to the Windows Package Manager repository?" → **Yes**

After you answer "Yes" to submit:

- `wingetcreate` forks the microsoft/winget-pkgs repository to your GitHub account
- Creates a new branch with your package manifests
- Opens a pull request automatically
- Provides the PR URL for tracking

After you submit your pull request, the WinGet team reviews your manifest for compliance and accuracy. You can monitor the PR status on GitHub and respond to any feedback from reviewers. Once approved and merged, your extension will be available through WinGet within a few hours.

#### WinGet updates via GitHub Actions

You can use GitHub Actions to update your already submitted projects to WinGet.

Check out how [PowerToys](https://github.com/microsoft/PowerToys/blob/main/.github/workflows/package-submissions.yml) does this.

You can also use the following `.github\workflows\update-winget.yml`:

```yml
# To use this template for a new extension:
# 1. Copy this file to a new workflow file (e.g., update-winget.yml)
# 2. Update Environmental variables with your data:
# - GITHUB_REPO with your GitHub repo name
# - GITHUB_REPO with your github user name (e.g., chatasweetie)
# - EXTENSION_NAME with your extension name (e.g., CmdPalMyExtension)
# - YOUR_PACKAGE_IDENTITY_NAME_HERE with the AppxPackageIdentityName located in the <ExtensionName>.csproj


name: Update WinGet - EXTENSION_NAME Extension

on:
  release:
    types: [published]
  workflow_dispatch:
    inputs:
      version:
        description: 'Version number (e.g., 0.0.2.0)'
        required: false
        type: string
      release_tag:
        description: 'Release tag (e.g., EXTENSION_NAME-v0.0.2.0)'
        required: false
        type: string

# Global constants: UPDATE THESE, example; EXTENSION_NAME: ${{ vars.EXTENSION_NAME || 'CmdPalMyExtension' }}
env:
  EXTENSION_NAME: ${{ vars.EXTENSION_NAME || 'EXTENSION_NAME' }}
  GITHUB_USER_NAME: ${{ vars.GITHUB_USER_NAME || 'GITHUB_USER_NAME' }}
  GITHUB_REPO: ${{ vars.GITHUB_REPO || 'GITHUB_REPO' }}
  YOUR_PACKAGE_IDENTITY_NAME_HERE: ${{ vars.YOUR_PACKAGE_IDENTITY_NAME_HERE || 'YOUR_PACKAGE_IDENTITY_NAME_HERE' }}

jobs:
  update-winget:
    # Only run if this is a matching extension release
    if: github.event_name == 'workflow_dispatch' || startsWith(github.event.release.name, '${{ env.EXTENSION_NAME }} Extension')
    runs-on: windows-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v4
        
      - name: Get release info
        id: release
        run: |
          if ("${{ github.event_name }}" -eq "workflow_dispatch" -and "${{ inputs.version }}" -ne "") {
            # Use provided inputs for manual trigger
            echo "VERSION=${{ inputs.version }}" >> $env:GITHUB_OUTPUT
            echo "TAG=${{ inputs.release_tag }}" >> $env:GITHUB_OUTPUT
          } elseif ("${{ github.event_name }}" -eq "release") {
            # Extract from release event
            $version = "${{ github.event.release.tag_name }}" -replace "${{ env.EXTENSION_NAME }}-v", ""
            echo "VERSION=$version" >> $env:GITHUB_OUTPUT
            echo "TAG=${{ github.event.release.tag_name }}" >> $env:GITHUB_OUTPUT
          } else {
            # Get latest release
            $latestRelease = gh release list --limit 1 --json tagName,name | ConvertFrom-Json | Where-Object { $_.name -like "${{ env.EXTENSION_NAME }} Extension*" }
            $version = $latestRelease.tagName -replace "${{ env.EXTENSION_NAME }}-v", ""
            echo "VERSION=$version" >> $env:GITHUB_OUTPUT
            echo "TAG=$($latestRelease.tagName)" >> $env:GITHUB_OUTPUT
          }
        env:
          GH_TOKEN: ${{ secrets.GITHUB_TOKEN }}

      - name: Install wingetcreate
        run: |
          iwr https://aka.ms/wingetcreate/latest -OutFile wingetcreate.exe

      - name: Update WinGet manifest
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        run: |
          $version = "${{ steps.release.outputs.VERSION }}"
          $tag = "${{ steps.release.outputs.TAG }}"
          
          # URLs for both installers
          $x64Url = "https://github.com/${{ env.GITHUB_USER_NAME }}/${{ env.GITHUB_REPO }}/releases/download/$tag/${{ env.EXTENSION_NAME }}-Setup-$version-x64.exe"
          $arm64Url = "https://github.com/${{ env.GITHUB_USER_NAME }}/${{ env.GITHUB_REPO }}/releases/download/$tag/${{ env.EXTENSION_NAME }}-Setup-$version-arm64.exe"
          
          Write-Host "Updating WinGet manifest for version $version"
          Write-Host "x64 URL: $x64Url"
          Write-Host "ARM64 URL: $arm64Url"
          
          # Update the manifest with both architecture installers
          .\wingetcreate.exe update ${{ env.YOUR_PACKAGE_IDENTITY_NAME_HERE }} `
            --version $version `
            --urls "$x64Url|x64" "$arm64Url|arm64" `
            --token $env:GITHUB_TOKEN `
            --submit
```

## Related content

- [Publishing overview](publish-extension.md)
- [Publish to Microsoft Store](publish-extension-store.md)
- [Extension Gallery](extension-gallery.md)
