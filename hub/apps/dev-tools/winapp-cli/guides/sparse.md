---
title: Sparse Packaging for App Identity
description: Grant package identity to an unpackaged desktop app using an identity-only (sparse) MSIX and external-location registration with the winapp CLI.
ms.date: 08/14/2026
ms.topic: how-to
ai-usage: ai-assisted

#customer intent: As a desktop app developer, I want to grant package identity to my unpackaged app with a sparse package so that I can use identity-gated Windows APIs without switching to MSIX distribution.
---

# Grant package identity with sparse packaging

A standard desktop executable — built with `dotnet build`, MSBuild, CMake, or another toolchain — has no package identity. Without identity, your app can't use many modern Windows APIs, such as toast notifications, background tasks, share targets, startup tasks, and the app data APIs.

Sparse packaging grants identity to your app without moving its binaries into an MSIX. You ship a tiny identity-only `.msix` that contains just a manifest and register it alongside your normally installed app by using an external location. Your `.exe` stays exactly where your installer puts it.

In this article, you create the identity-only package, embed the identity reference into your app, register the package for local testing, and integrate registration into your installer.

## Prerequisites

- Windows 10, version 2004 (build 19041) or later. Sparse packages rely on `uap10:AllowExternalContent`, which requires build 19041 or later.
- A terminal, such as Windows PowerShell or Windows Terminal, to run the commands in this article.
- The winapp CLI. Install or update it from your terminal with winget:

  ```powershell
  winget install Microsoft.winappcli --source winget
  ```

- A code-signing certificate that's trusted on the target machine. For local testing, generate a development certificate with [`winapp cert generate`](../usage.md#cert) and trust it. Sign production packages with a certificate whose subject matches the manifest `Publisher`.

## How sparse packaging works

Sparse packaging is the production counterpart to [`winapp create-debug-identity`](../usage.md#create-debug-identity), which is for developer-time debugging only. It grants [package identity](../../../desktop/modernize/package-identity-overview.md) to an app that you distribute with your own installer, so the app can call identity-gated Windows APIs.

The CLI steps in this article map to the first three steps of the [Grant identity to non-packaged apps](../../../desktop/modernize/grant-identity-to-nonpackaged-apps.md) workflow:

| Step | Command | Result |
|------|---------|--------|
| 1. Create the identity manifest | `winapp init --exe <exe> --sparse` | `sparse/appxmanifest.xml` + `sparse/Assets/` |
| 2. Build and sign the identity package | `winapp pack <appxmanifest.xml> --cert <pfx>` | `<PackageName>.identity.msix` |
| 3. Embed identity into the app | `winapp embed-identity <exe>` | `<msix>` element in the exe's fusion manifest |

Step 4 of that workflow — register and unregister the package — is your installer's responsibility; step 5 is optional work. This article shows how to register for local testing and how to integrate registration into your installer.

Use sparse packaging when:

- You already have a mature installer (Inno Setup, WiX, NSIS, MSI) and don't want to switch to MSIX for distribution, but you need identity-gated Windows APIs.
- Your app must install to a path or with a layout that MSIX doesn't allow.
- You want a minimal, additive change: keep your existing install flow and add one `.msix` registration step.

If you're starting fresh and can distribute as MSIX, a full packaged app (`winapp init` + `winapp pack <folder>`) is simpler.

For a complete, working end-to-end example (a WPF app with an Inno Setup installer), see the [sparse-app](https://github.com/microsoft/WinAppCli/tree/main/samples/sparse-app) sample.

The examples that follow assume a built executable at `./bin/Release/net8.0-windows/MyApp.exe`.

## Create the sparse identity manifest

Generate the sparse manifest from your built executable:

```powershell
winapp init --exe ./bin/Release/net8.0-windows/MyApp.exe --sparse
```

This command infers the package name, publisher, version, and description from the exe's file version info and prompts you to accept or override them. Add `--use-defaults` (or `--no-prompt`) to skip the prompts in CI, and `--name` or `--publisher` to override specific values:

```powershell
winapp init --exe ./bin/Release/net8.0-windows/MyApp.exe --sparse --use-defaults `
  --name "Contoso.MyApp" --publisher "CN=Contoso"
```

By default, the command writes the following to a dedicated `sparse/` folder in the current directory (override with `--output-dir`):

- `appxmanifest.xml` — a sparse manifest with `<uap10:AllowExternalContent>true</uap10:AllowExternalContent>` (an element under `<Properties>`), `ProcessorArchitecture="neutral"`, a `win32App` application, and the exe name filled into `Executable`.
- `Assets/` — placeholder visual assets, extracted from the exe's icon when possible.

The manifest and `Assets/` are build-time inputs consumed by `winapp pack` and `winapp embed-identity`. Nothing reads them from beside the exe at runtime, so a dedicated, source-controlled `sparse/` folder keeps them out of a build-output directory (like `bin/`) that a clean or rebuild would wipe. `winapp pack` and `winapp embed-identity` look in `sparse/` automatically, so you rarely need to name the path.

> [!NOTE]
> The sparse init flow deliberately skips all SDK and package installation. Identity-only packages have no SDK dependencies.

If an `appxmanifest.xml` already exists in the target directory, init stops rather than overwriting it (and its `Assets/`). Re-run with `--force` to regenerate it. Make sure the `Publisher` in the generated manifest matches the certificate you sign with — edit `appxmanifest.xml` if needed, or pass `--publisher` when you generate it.

## Build and sign the identity package

Point `winapp pack` at the sparse manifest (a file, not a folder):

```powershell
winapp pack ./sparse/appxmanifest.xml --cert ./devcert.pfx
```

Because the manifest declares `AllowExternalContent`, `winapp pack` builds an identity-only `.msix` that contains just the manifest — no binaries, no assets. Sibling files next to the manifest are ignored, so the package never includes your assets or binaries. The output defaults to `<PackageName>.identity.msix` in the current directory; use `--output` to change it. Signing happens only when you pass `--cert` (or `--generate-cert`).

## Embed identity into your app

Embed the `<msix>` element so Windows connects the running exe to the identity package. You can modify the built binary in place or maintain a checked-in side-by-side manifest.

To modify the built binary directly:

```powershell
# EXE mode — modify the built binary in place
winapp embed-identity ./bin/Release/net8.0-windows/MyApp.exe
```

To maintain the side-by-side manifest as a checked-in file and rebuild:

```powershell
# XML mode — update an external SxS manifest, then rebuild your app
winapp embed-identity ./app.manifest
```

In XML mode, the `<msix>` element is inserted into (or replaced in) the target manifest. Reference that manifest from your project (for .NET, set `<ApplicationManifest>app.manifest</ApplicationManifest>`) and rebuild so the element is embedded in the exe.

Both modes read identity from a sparse `appxmanifest.xml`. When you omit `--manifest`, winapp looks in a `sparse/` folder beside the target first, then in the current directory. Pass `--manifest` to point elsewhere.

> [!NOTE]
> EXE mode rewrites the binary, which invalidates any existing Authenticode signature. Re-sign the exe (for example, `winapp sign ./MyApp.exe --cert ./devcert.pfx --cert-password <certificate-password>`) before you distribute it.

## Register the identity package for local testing

Registration is part of step 4 of the workflow. In production, your installer performs this step; for local testing, run it yourself.

Before you register, make sure the certificate you signed the package with is trusted on this machine — Windows rejects an identity package signed by an untrusted certificate. For local development, open PowerShell or Windows Terminal as an administrator, navigate to the working directory, and install the generated PFX certificate into the local machine's Trusted People store:

```powershell
winapp cert install .\devcert.pfx
```

> [!WARNING]
> Trust a certificate only for local development on your own machine. Don't ship or trust a self-signed development certificate on user machines. Sign production packages with a certificate from a trusted certificate authority whose subject matches the manifest `Publisher`.

The manifest's logos are resolved from the external location at runtime, not from the identity-only `.msix`. Copy the generated assets next to your exe (the external location) before you register — otherwise Windows registers a layout that's missing every logo the manifest references:

```powershell
# Copy the generated assets into the external location (beside your exe)
Copy-Item ./sparse/Assets -Destination .\bin\Release\net8.0-windows\Assets -Recurse -Force
```

Register the identity package against that folder (the external location):

```powershell
Add-AppxPackage -Path .\MyApp.identity.msix `
  -ExternalLocation (Resolve-Path .\bin\Release\net8.0-windows)
```

Launch the app and confirm identity is present. For example, `Windows.ApplicationModel.Package.Current.Id.FamilyName` returns your package family name instead of throwing.

## Unregister the package

Unregistration is also part of step 4 of the workflow. Remove the registration when you finish local testing or when your installer uninstalls the app.

Find the package with `Get-AppxPackage`, then pipe it to `Remove-AppxPackage`:

```powershell
Get-AppxPackage -Name MyApp | Remove-AppxPackage
```

Replace `MyApp` with your package name. If you're unsure of the exact name, list matches first with `Get-AppxPackage -Name *MyApp*`.

## Integrate registration into your installer

Registration and unregistration are the installer's job, and the pattern is the same across installer tools:

- **Install:** copy your app binaries, the `Assets/` folder, and the `.msix` to the install directory, then run `Add-AppxPackage -Path "<install-dir>\MyApp.identity.msix" -ExternalLocation "<install-dir>"`.
- **Uninstall:** run `Remove-AppxPackage <full-package-name>` before deleting files.

Because assets are resolved from the external location, always deploy the `Assets/` folder alongside your app in the layout the manifest expects.

> [!WARNING]
> The install directory is resolved at install time and might contain characters (such as a single quote) that break out of a PowerShell string literal. Always escape or validate the path before you interpolate it into a `-Command` string. Prefer passing paths as arguments to a `-File` script over inline `-Command` interpolation. The WiX and NSIS snippets below assume a trusted install path, while the Inno Setup example demonstrates safe escaping.

### Inno Setup

Build the PowerShell arguments in a `[Code]` function so the runtime install path is escaped for the single-quoted PowerShell literal (an install directory that contains a `'` must not be able to inject script):

```pascal
[Files]
Source: "dist\*"; DestDir: "{app}"; Flags: recursesubdirs
Source: "MyApp.identity.msix"; DestDir: "{app}"

[Run]
Filename: "powershell.exe"; Parameters: "{code:RegisterParams}"; Flags: runhidden

[UninstallRun]
Filename: "powershell.exe"; \
  Parameters: "-NoProfile -ExecutionPolicy Bypass -Command ""Get-AppxPackage -Name 'MyApp' | Remove-AppxPackage"""; \
  Flags: runhidden

[Code]
function EscapePSLiteral(const Value: string): string;
var S: string;
begin
  S := Value; StringChange(S, '''', ''''''); Result := S;
end;

function RegisterParams(Param: string): string;
var AppDir: string;
begin
  AppDir := ExpandConstant('{app}');
  { -ErrorAction Stop + try/catch make a registration failure terminating, so powershell.exe
    exits nonzero and the AfterInstall callback (see the full sample) can abort with rollback. }
  Result := '-NoProfile -ExecutionPolicy Bypass -Command "try { Add-AppxPackage -Path ''' +
    EscapePSLiteral(AppDir + '\MyApp.identity.msix') +
    ''' -ExternalLocation ''' + EscapePSLiteral(AppDir) + ''' -ErrorAction Stop } catch { Write-Error $_; exit 1 }"';
end;
```

For a complete, working `setup.iss`, see the [sparse-app](https://github.com/microsoft/WinAppCli/tree/main/samples/sparse-app) sample.

### Registration script for WiX and NSIS

The WiX and NSIS examples invoke a small `register-sparse.ps1` through `-File` so the install path is passed as a parameter (PowerShell binds it as data) instead of being interpolated into a `-Command` string. This avoids script injection through a crafted install directory (for example, a folder name that contains a quote or `$(...)`):

```powershell
# register-sparse.ps1 — ship this alongside your installer
param(
  [Parameter(Mandatory)] [string] $MsixPath,
  [Parameter(Mandatory)] [string] $ExternalLocation,
  [Parameter(Mandatory)] [string] $PackageName
)
$ErrorActionPreference = 'Stop'
try {
  # Add-AppxPackage emits NON-terminating errors by default, so a failure would otherwise leave
  # the process exit code at 0 and let the installer complete without identity. Try the add
  # directly first: a fresh install or a version-bumped upgrade registers/updates in place
  # without touching any existing registration. -ErrorAction Stop + the outer trap make a real
  # failure terminating so the installer (WiX Return="check" / NSIS) sees it.
  try {
    Add-AppxPackage -Path $MsixPath -ExternalLocation $ExternalLocation -ErrorAction Stop
  } catch {
    # Only ONE failure is safe to resolve by unregister+retry: the exact same version is already
    # registered (HRESULT 0x80073CFB, ERROR_PACKAGE_ALREADY_EXISTS — "already installed,
    # reinstallation blocked"), which Add-AppxPackage rejects. Re-throw everything else
    # (untrusted/corrupt .msix, unsupported OS, ...) so a bad new package can NEVER unregister a
    # working prior registration and strip the installed app of the identity it already had.
    if ($_.Exception.HResult -ne 0x80073CFB) { throw }
    Get-AppxPackage -Name $PackageName | Remove-AppxPackage -ErrorAction SilentlyContinue
    Add-AppxPackage -Path $MsixPath -ExternalLocation $ExternalLocation -ErrorAction Stop
  }
} catch {
  Write-Error $_
  exit 1
}
```

### WiX (v3)

Register per user (`Impersonate="yes"`), because `Add-AppxPackage` registers the package for the account that runs it. A deferred action with `Impersonate="no"` runs as `LocalSystem`, which doesn't grant identity to the installing user (and is commonly rejected). For a per-machine MSI, run the registration impersonated so it applies to the invoking user.

A deferred custom action can't read `INSTALLFOLDER` directly (deferred actions run in a context without access to properties), and simply declaring the action doesn't run it. Marshal the paths in through `CustomActionData` — an immediate type-51 action whose `Property` name equals the deferred action's `Id` — and schedule both after `InstallFiles`:

```xml
<!-- Immediate: stash the command line (with the resolved paths) into the deferred action's
     CustomActionData. Windows Installer copies the value of the property named the same as a
     deferred action into that action's CustomActionData. -->
<CustomAction Id="SetRegisterSparseCmd" Property="RegisterSparse" Execute="immediate"
  Value="powershell.exe -NoProfile -ExecutionPolicy Bypass -File &quot;[INSTALLFOLDER]register-sparse.ps1&quot; -MsixPath &quot;[INSTALLFOLDER]MyApp.identity.msix&quot; -ExternalLocation &quot;[INSTALLFOLDER]&quot; -PackageName &quot;MyPackageIdentityName&quot;" />

<!-- Deferred + impersonated: CAQuietExec reads its command line from CustomActionData when run
     deferred, so it registers the package for the invoking user. Return="check" fails the
     install if registration fails. -->
<CustomAction Id="RegisterSparse" BinaryKey="WixCA" DllEntry="CAQuietExec"
  Execute="deferred" Impersonate="yes" Return="check" />

<InstallExecuteSequence>
  <Custom Action="SetRegisterSparseCmd" After="InstallFiles">NOT Installed</Custom>
  <Custom Action="RegisterSparse" After="SetRegisterSparseCmd">NOT Installed</Custom>
</InstallExecuteSequence>
```

`CAQuietExec` ships in the WiX util extension (`WixUtilExtension`); reference it so the `WixCA` binary is available.

> [!NOTE]
> A single impersonated action registers identity only for the user who runs the installer. To provision every user of a per-machine install, register on first launch (per-user) instead, or use a provisioning mechanism such as `Add-AppxProvisionedPackage`.

### NSIS

```nsis
Section
  # Capture the PowerShell exit code and abort if registration failed. register-sparse.ps1 exits
  # nonzero on failure (it sets $ErrorActionPreference='Stop' and traps), so without this check the
  # installer would complete even though the app has no identity.
  ExecWait 'powershell.exe -NoProfile -ExecutionPolicy Bypass -File "$INSTDIR\register-sparse.ps1" -MsixPath "$INSTDIR\MyApp.identity.msix" -ExternalLocation "$INSTDIR" -PackageName "MyPackageIdentityName"' $0
  IntCmp $0 0 +2
    Abort "Registering the sparse identity package failed (exit code $0). The app requires package identity."
SectionEnd
```

## Troubleshoot common issues

### Package.Current throws or reports no identity at runtime

- The identity package isn't registered, or the exe's fusion manifest is missing the `<msix>` element. Re-run [`winapp embed-identity`](../usage.md#embed-identity) (and rebuild if you're using XML mode), then re-register with `Add-AppxPackage -ExternalLocation`.
- The `<msix packageName>`, `publisher`, and `applicationId` in the exe must exactly match the registered package's identity.

### Assets or logos don't appear

- Ensure the `Assets/` folder is deployed at the external location with the same relative paths the manifest expects. Assets are resolved from the external location, not the `.msix`.

### Add-AppxPackage fails with a signing or trust error

- The `.msix` must be signed by a certificate that's trusted on the machine and whose subject matches the manifest `Publisher`. For local testing, generate and trust a development certificate with [`winapp cert generate`](../usage.md#cert), and make sure the manifest `Publisher` matches it.

### MakeAppx reports that a win32App must not declare EntryPoint

- A sparse `win32App` application must not declare `EntryPoint`. Manifests generated by `winapp init --sparse` are already correct; remove any `EntryPoint` attribute if you hand-edited the manifest.

### "Input is a file but not a sparse manifest"

- `winapp pack <file>` accepts only a manifest that declares `<uap10:AllowExternalContent>true</uap10:AllowExternalContent>`. Generate one with `winapp init --exe <exe> --sparse`, or pass an input folder to build a full MSIX.

## Related content

- [CLI usage: `init`](../usage.md#init), [`pack`](../usage.md#pack), and [`embed-identity`](../usage.md#embed-identity)
- [Grant identity to non-packaged apps](../../../desktop/modernize/grant-identity-to-nonpackaged-apps.md)
- [Debugging with package identity](../debugging.md)
