---
title: Windows App Development CLI reference
description: Complete command reference for the Windows App Development CLI (winapp CLI), including setup, packaging, manifest, certificate, signing, and utility commands.
ms.date: 02/20/2026
ms.topic: article
---

# Windows App Development CLI reference

> [!IMPORTANT]
> The Windows App Development CLI is currently in **public preview**. Features and commands may change before the final release.

This page documents all available commands for the [winapp CLI](index.md).

## Global options

All commands support these global options:

| Option | Description |
|--------|-------------|
| `--verbose`, `-v` | Enable verbose output for detailed logging |
| `--quiet`, `-q` | Suppress progress messages |
| `--help`, `-h` | Show command help |

## Global cache directory

WinApp creates a directory to cache files that can be shared between multiple projects. By default, this is `$UserProfile/.winapp`.

To use a different location, set the `WINAPP_CLI_CACHE_DIRECTORY` environment variable:

```powershell
$env:WINAPP_CLI_CACHE_DIRECTORY = "d:\temp\.winapp"
```

---

## Setup commands

### init

Initialize a directory with Windows SDK, Windows App SDK, and required assets for modern Windows development.

```bash
winapp init [base-directory] [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `base-directory` | Base/root directory for the app/workspace (default: current directory) |

**Options:**

| Option | Description |
|--------|-------------|
| `--config-dir <path>` | Directory to read/store configuration (default: current directory) |
| `--setup-sdks` | SDK installation mode: `stable` (default), `preview`, `experimental`, or `none` |
| `--ignore-config`, `--no-config` | Don't use configuration file for version management |
| `--no-gitignore` | Don't update .gitignore file |
| `--use-defaults`, `--no-prompt` | Do not prompt, and use default of all prompts |
| `--config-only` | Only handle configuration file operations, skip package installation |

**What it does:**

- Creates `winapp.yaml` configuration file
- Downloads Windows SDK and Windows App SDK packages
- Generates C++/WinRT headers and binaries
- Creates AppxManifest.xml
- Sets up build tools and enables developer mode
- Updates .gitignore to exclude generated files

**Automatic .NET project detection:**

When a `.csproj` file is found in the target directory, `init` uses a streamlined .NET-specific flow:

- Validates and updates the `TargetFramework` to a Windows-compatible TFM (for example, `net10.0-windows10.0.26100.0`)
- Adds `Microsoft.WindowsAppSDK` and `Microsoft.Windows.SDK.BuildTools` as NuGet `PackageReference` entries directly in the `.csproj`
- Generates `appxmanifest.xml`, assets, and a development certificate
- Does **not** create a `winapp.yaml` or download C++ projections (use `dotnet restore` for NuGet packages)

**Examples:**

```bash
# Initialize current directory
winapp init

# Initialize with experimental packages
winapp init --setup-sdks experimental

# Initialize specific directory without prompts
winapp init ./my-project --use-defaults

# Initialize a .NET project (auto-detected from .csproj)
cd my-dotnet-app
winapp init
```

> [!TIP]
> If you ran `init` with `--setup-sdks none` and later need the SDKs, re-run `winapp init --use-defaults --setup-sdks stable`. This preserves existing files (manifest, etc.).

### restore

Restore packages and regenerate files based on existing `winapp.yaml` configuration.

```bash
winapp restore [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--config-dir <path>` | Directory containing winapp.yaml (default: current directory) |

**What it does:**

- Reads existing `winapp.yaml` configuration
- Downloads/updates SDK packages to specified versions
- Regenerates C++/WinRT headers and binaries

> [!NOTE]
> For .NET projects initialized with `winapp init`, there is no `winapp.yaml`. Use `dotnet restore` to restore NuGet packages instead.

**Examples:**

```bash
# Restore from winapp.yaml in current directory
winapp restore
```

### update

Update packages to their latest versions and update the configuration file.

```bash
winapp update [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--config-dir <path>` | Directory containing winapp.yaml (default: current directory) |
| `--setup-sdks` | SDK installation mode: `stable` (default), `preview`, `experimental`, or `none` |

**What it does:**

- Reads existing `winapp.yaml` configuration
- Updates all packages to their latest available versions
- Updates the `winapp.yaml` file with new version numbers
- Regenerates C++/WinRT headers and binaries

**Examples:**

```bash
# Update packages to latest versions
winapp update

# Update including experimental packages
winapp update --setup-sdks experimental
```

---

## Packaging commands

### pack

Create MSIX packages from prepared application directories. Requires appxmanifest.xml file to be present in the target directory, in the current directory, or passed with the `--manifest` option.

```bash
winapp pack <input-folder> [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `input-folder` | Directory containing the application files to package |

**Options:**

| Option | Description |
|--------|-------------|
| `--output <filename>` | Output MSIX file name (default: `<name>.msix`) |
| `--name <name>` | Package name (default: from manifest) |
| `--manifest <path>` | Path to AppxManifest.xml (default: auto-detect) |
| `--cert <path>` | Path to signing certificate (enables auto-signing) |
| `--cert-password <password>` | Certificate password (default: "password") |
| `--generate-cert` | Generate a new development certificate |
| `--install-cert` | Install certificate to machine |
| `--publisher <name>` | Publisher name for certificate generation |
| `--self-contained` | Bundle Windows App SDK runtime |
| `--skip-pri` | Skip PRI file generation |
| `--executable <path>` | Path to the executable relative to the input folder. Used to resolve `$targetnametoken$` placeholders in the manifest. |

**What it does:**

- Validates and processes AppxManifest.xml files
- Resolves `$placeholder$` tokens in the manifest (see [Manifest placeholders](#manifest-placeholders))
- Ensures proper framework dependencies
- Updates side-by-side manifests with registrations
- Handles self-contained Windows App SDK deployment
- Signs package if certificate provided

**Examples:**

```bash
# Package directory with auto-detected manifest
winapp pack ./dist

# Package with custom output name and certificate
winapp pack ./dist --output MyApp.msix --cert ./cert.pfx

# Package with generated and installed certificate and self-contained runtime
winapp pack ./dist --generate-cert --install-cert --self-contained

# Package with explicit executable
winapp pack ./dist --executable MyApp.exe
```

### create-debug-identity

Create app identity for debugging without full MSIX packaging using [external location/sparse packaging](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

```bash
winapp create-debug-identity [entrypoint] [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `entrypoint` | Path to executable (.exe) or script that needs identity |

**Options:**

| Option | Description |
|--------|-------------|
| `--manifest <path>` | Path to AppxManifest.xml (default: `./appxmanifest.xml`) |
| `--no-install` | Don't install the package after creation |
| `--keep-identity` | Keep the manifest identity as-is, without appending `.debug` to the package name and application ID |

**What it does:**

- Modifies executable's side-by-side manifest
- Registers sparse package for identity
- Enables debugging of identity-requiring APIs

**Examples:**

```bash
# Add identity to executable using local manifest
winapp create-debug-identity ./bin/MyApp.exe

# Add identity with custom manifest location
winapp create-debug-identity ./dist/app.exe --manifest ./custom-manifest.xml
```

---

## Manifest commands

### manifest generate

Generate AppxManifest.xml from templates.

```bash
winapp manifest generate [directory] [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `directory` | Directory to generate manifest in (default: current directory) |

**Options:**

| Option | Description |
|--------|-------------|
| `--package-name <name>` | Package name (default: folder name) |
| `--publisher-name <name>` | Publisher CN (default: `CN=<current user>`) |
| `--version <version>` | Version (default: "1.0.0.0") |
| `--description <text>` | Description (default: "My Application") |
| `--entrypoint <path>` | Entry point executable or script |
| `--template <type>` | Template type: `packaged` (default) or `sparse` |
| `--logo-path <path>` | Path to logo image file |
| `--if-exists <Error\|Overwrite\|Skip>` | Behavior if the file already exists (default: Error) |

**Templates:**

- `packaged` - Standard packaged app manifest
- `sparse` - App manifest using [sparse/external location packaging](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps)

#### Manifest placeholders

Generated manifests use `$placeholder$` tokens (dollar-sign delimited) that are resolved automatically at packaging time:

| Placeholder | Resolved to | Example |
|-------------|-------------|---------|
| `$targetnametoken$` | Executable name without extension | `Executable="$targetnametoken$.exe"` becomes `Executable="MyApp.exe"` |
| `$targetentrypoint$` | `Windows.FullTrustApplication` | Always resolved automatically |

**How placeholders are resolved:**

- **`winapp pack`** resolves `$targetnametoken$` using the `--executable` option or by auto-detecting the single `.exe` in the input folder.
- **`winapp create-debug-identity`** resolves `$targetnametoken$` from the entrypoint argument when provided.
- **`winapp manifest generate --executable`** extracts metadata from the executable but keeps `$targetnametoken$.exe` in the manifest for later resolution.

> [!TIP]
> Keeping `$targetnametoken$` in your checked-in manifest avoids hard-coding executable names and works with both `winapp pack` and Visual Studio builds.

**Examples:**

```bash
# Generate standard manifest interactively
winapp manifest generate

# Generate with all options specified
winapp manifest generate ./src --package-name MyApp --publisher-name "CN=My Company" --if-exists overwrite
```

### manifest update-assets

Generate all required MSIX image assets from a single source image.

```bash
winapp manifest update-assets <image-path> [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `image-path` | Path to source image file (PNG, JPG, GIF, etc.) |

**Options:**

| Option | Description |
|--------|-------------|
| `--manifest <path>` | Path to AppxManifest.xml file (default: search current directory) |

Takes a single source image and automatically generates all 12 required MSIX image assets at the correct dimensions. Assets are saved to the `Assets` directory relative to the manifest location.

**Examples:**

```bash
# Generate assets with auto-detected manifest
winapp manifest update-assets mylogo.png

# Specify manifest location explicitly
winapp manifest update-assets mylogo.png --manifest ./dist/appxmanifest.xml
```

---

## Certificate and signing commands

### cert generate

Generate development certificates for package signing.

```bash
winapp cert generate [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--manifest <appxmanifest.xml>` | Extract publisher information from appxmanifest.xml |
| `--publisher <name>` | Publisher name for certificate |
| `--output <path>` | Output certificate file path |
| `--password <password>` | Certificate password (default: "password") |
| `--valid-days <days>` | Number of days the certificate is valid (default: 365) |
| `--install` | Install the certificate to the local machine store after generation |
| `--if-exists <Error\|Overwrite\|Skip>` | Behavior if the certificate file already exists (default: Error) |

### cert install

Install certificate to machine certificate store.

```bash
winapp cert install <cert-path>
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `cert-path` | Path to certificate file to install |

**Examples:**

```bash
# Generate certificate for specific publisher
winapp cert generate --publisher "CN=My Company" --output ./mycert.pfx

# Install certificate to machine
winapp cert install ./mycert.pfx
```

### sign

Sign MSIX packages and executables with certificates.

```bash
winapp sign <file-path> [options]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `file-path` | Path to MSIX package or executable to sign |

**Options:**

| Option | Description |
|--------|-------------|
| `--cert <path>` | Path to signing certificate |
| `--cert-password <password>` | Certificate password (default: "password") |

**Examples:**

```bash
# Sign MSIX package
winapp sign MyApp.msix --cert ./mycert.pfx

# Sign executable
winapp sign ./bin/MyApp.exe --cert ./mycert.pfx --cert-password mypassword
```

---

## Utility commands

### tool

Access Windows SDK tools directly. Uses tools available in [Microsoft.Windows.SDK.BuildTools](https://www.nuget.org/packages/Microsoft.Windows.SDK.BuildTools/).

```bash
winapp tool <tool-name> [tool-arguments]
```

**Available tools:**

- `makeappx` - Create and manipulate app packages
- `signtool` - Sign files and verify signatures
- `mt` - Manifest tool for side-by-side assemblies
- And other Windows SDK tools from Microsoft.Windows.SDK.BuildTools

**Examples:**

```bash
# Use signtool to verify signature
winapp tool signtool verify /pa MyApp.msix
```

### store

Run a Microsoft Store Developer CLI command. This command downloads the Microsoft Store Developer CLI if not already downloaded. Learn more at [Microsoft Store Developer CLI](https://aka.ms/msstoredevcli).

```bash
winapp store [args...]
```

**Arguments:**

| Argument | Description |
|----------|-------------|
| `args...` | Arguments to pass directly to the `msstore` CLI |

**Examples:**

```bash
# List all apps in your Microsoft Partner Center account
winapp store app list

# Publish a package to the Microsoft Store
winapp store publish ./myapp.msix --appId <your-app-id>
```

### get-winapp-path

Get paths to installed Windows SDK components.

```bash
winapp get-winapp-path [options]
```

Returns paths to the `.winapp` workspace directory, package installation directories, and generated header locations.

---

## Node.js/Electron commands

These commands are available in the NPM package only.

### node create-addon

Generate native C++ or C# addon templates with Windows SDK and Windows App SDK integration.

```bash
npx winapp node create-addon [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--name <name>` | Addon name (default: "nativeWindowsAddon") |
| `--template` | Select type of addon: `cs` or `cpp` (default: `cpp`) |
| `--verbose` | Enable verbose output |

**What it does:**

- Creates addon directory with template files
- Generates binding.gyp and addon files with Windows SDK examples
- Installs required npm dependencies
- Adds build script to package.json

**Examples:**

```bash
# Generate addon with default name
npx winapp node create-addon

# Generate custom named C# addon
npx winapp node create-addon --name myWindowsAddon --template cs
```

### node add-electron-debug-identity

Add app identity to Electron development process by using sparse packaging. Requires an appxmanifest.xml (create one with `winapp init` or `winapp manifest generate`).

> [!IMPORTANT]
> There is a known issue with sparse packaging Electron applications which causes the app to crash on start or not render web content. The issue has been fixed in Windows but hasn't propagated to all devices yet. You can [disable sandboxing in your Electron app](https://www.electronjs.org/docs/latest/tutorial/sandbox#disabling-chromiums-sandbox-testing-only) with the `--no-sandbox` flag as a workaround. This issue does not affect full MSIX packaging.
>
> To undo the Electron debug identity, use `winapp node clear-electron-debug-identity`.

```bash
npx winapp node add-electron-debug-identity [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--manifest <path>` | Path to custom appxmanifest.xml (default: appxmanifest.xml in current directory) |
| `--no-install` | Do not install or modify dependencies; only configure the Electron debug identity |
| `--keep-identity` | Keep the manifest identity as-is, without appending `.debug` |
| `--verbose` | Enable verbose output |

**Examples:**

```bash
# Add identity to Electron development process
npx winapp node add-electron-debug-identity

# Use a custom manifest file
npx winapp node add-electron-debug-identity --manifest ./custom/appxmanifest.xml
```

### node clear-electron-debug-identity

Remove package identity from the Electron debug process by restoring the original electron.exe from backup.

```bash
npx winapp node clear-electron-debug-identity [options]
```

**Options:**

| Option | Description |
|--------|-------------|
| `--verbose` | Enable verbose output |

**Examples:**

```bash
# Remove identity from Electron development process
npx winapp node clear-electron-debug-identity
```

## Related topics

- [winapp CLI overview](index.md)
- [Framework guides](guides/index.md)
- [winapp CLI on GitHub](https://github.com/microsoft/WinAppCli)
