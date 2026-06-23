---
title: WinGet Configuration file v3 schema reference
description: Reference documentation for creating WinGet Configuration files using the dscv3 processor and DSC v3 schema.
ms.date: 05/01/2026
ms.topic: reference
---

# WinGet Configuration file v3 schema reference

WinGet Configuration files using the v3 schema leverage the [DSC v3](https://github.com/PowerShell/DSC) processor for evaluating and applying desired state. The v3 schema introduces a simplified document structure, native DSC v3 resources, and improved resource type naming.

> [!NOTE]
> The v3 configuration schema requires WinGet version 1.11 or later with the dscv3 processor. To check your version, run `winget --version`. The dscv3 processor is distributed as a separate package (`Microsoft.DesiredStateConfiguration`) that WinGet installs automatically.

## File format

WinGet Configuration v3 files use the same `.winget` file extension and YAML format as v2 files. The key structural difference is that v3 files use the DSC v3 document schema directly, rather than wrapping resources inside a `properties:` node.

### File naming convention

The convention for naming a WinGet Configuration file is using the `.winget` file extension (like `configuration.winget`). For Git-based projects, the default configuration should be stored in a `.config` directory at: `./.config/configuration.winget`.

## Document structure

A v3 configuration file has three top-level keys:

```yaml
$schema: https://raw.githubusercontent.com/PowerShell/DSC/main/schemas/2023/08/config/document.json
metadata:
  winget:
    processor:
      identifier: dscv3
resources:
- type: ...
  name: ...
  properties: ...
```

### Schema

The `$schema` key points to the DSC v3 configuration document JSON schema. This enables YAML language server validation when editing in VS Code with the [YAML extension](https://marketplace.visualstudio.com/items?itemName=redhat.vscode-yaml).

```yaml
$schema: https://raw.githubusercontent.com/PowerShell/DSC/main/schemas/2023/08/config/document.json
```

> [!NOTE]
> The `2023/08` version in the schema URL was current when this article was written. Newer schema versions may be available in the [PowerShell/DSC schemas directory](https://github.com/PowerShell/DSC/tree/main/schemas). Check there if you encounter validation errors.

### Metadata

The `metadata` section tells WinGet which processor to use for evaluating the configuration. For v3 configurations, the processor identifier must be `dscv3`.

```yaml
metadata:
  winget:
    processor:
      identifier: dscv3
```

### Resources

The `resources` section is a top-level array of resource instances. Unlike v2 (where resources are nested under `properties.resources`), v3 resources are at the document root level.

## Resource structure

Each resource in the `resources` array has the following fields:

| Field | Required | Description |
|-------|----------|-------------|
| `type` | Yes | The DSC resource type in the format `{Provider}/{Resource}`. |
| `name` | Yes | A unique identifier for the resource instance. Used by other resources in `dependsOn`. |
| `properties` | Yes | The configuration properties passed to the DSC resource. |
| `dependsOn` | No | An array of resource `name` values that must complete before this resource. |
| `metadata` | No | Additional metadata including security context and description. |

### Example resource

```yaml
- type: Microsoft.WinGet/Package
  name: PowerShell
  properties:
    id: Microsoft.PowerShell
    source: winget
    useLatest: true
  metadata:
    description: Install PowerShell 7
```

## Resource types

### Microsoft.WinGet/Package

Installs a package from a WinGet source. This is the v3 equivalent of `Microsoft.WinGet.DSC/WinGetPackage`.

**Properties:**

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `id` | string | Yes | The package identifier (e.g., `Git.Git`). |
| `source` | string | Yes | The package source (e.g., `winget`). |
| `useLatest` | boolean | No | When `true`, ensures the latest version is installed. |
| `version` | string | No | A specific version to install. |

**Example:**

```yaml
- type: Microsoft.WinGet/Package
  name: VSCode
  properties:
    id: Microsoft.VisualStudioCode
    source: winget
    useLatest: true
  metadata:
    description: Install Visual Studio Code
```

### Microsoft.Windows/Registry

A native DSC v3 resource for managing Windows registry keys and values. This resource does not require a PowerShell module install or adapter and evaluates significantly faster than the v2 `PSDscResources/Registry` resource.

**Properties:**

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `keyPath` | string | Yes | The registry key path (e.g., `HKCU\SOFTWARE\Microsoft\...`). Uses backslash format without a colon (e.g., `HKLM\` not `HKLM:\`). |
| `valueName` | string | No | The name of the registry value. |
| `valueData` | object | No | The registry value data. See [Value data types](#registry-value-data-types). |
| `_exist` | boolean | No | When `true`, ensures the key/value exists. When `false`, ensures it is removed. |

#### Registry value data types

The `valueData` property uses a typed object format:

| Type | Format | Example |
|------|--------|---------|
| `String` | `{ String: "value" }` | `valueData: { String: "about:blank" }` |
| `DWord` | `{ DWord: number }` | `valueData: { DWord: 1 }` |
| `QWord` | `{ QWord: number }` | `valueData: { QWord: 0 }` |
| `ExpandString` | `{ ExpandString: "value" }` | `valueData: { ExpandString: "%PATH%" }` |
| `MultiString` | `{ MultiString: [values] }` | `valueData: { MultiString: ["a", "b"] }` |
| `Binary` | `{ Binary: [bytes] }` | `valueData: { Binary: [0, 1, 2] }` |
| None | `None` | `valueData: None` |

**Example:**

```yaml
- type: Microsoft.Windows/Registry
  name: EnableLongPaths
  properties:
    keyPath: HKLM\SYSTEM\CurrentControlSet\Control\FileSystem
    valueName: LongPathsEnabled
    valueData:
      DWord: 1
    _exist: true
  metadata:
    winget:
      securityContext: elevated
    description: Enable Win32 long path support
```

### Microsoft.DSC.Transitional/RunCommandOnSet

Runs an executable command when applying the configuration. This resource always reports "not in desired state" during test operations because it has no test capability. Use this for tasks that must always run or where idempotency is handled within the command itself.

**Properties:**

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `executable` | string | Yes | The path to the executable to run. |
| `arguments` | object | No | An ordered map of arguments to pass to the executable. Use numeric string keys (`"0"`, `"1"`, etc.) and include `treatAsArray: true`. |
| `exitCode` | integer | No | The expected exit code for success. Default is `0`. |

**Example:**

```yaml
- type: Microsoft.DSC.Transitional/RunCommandOnSet
  name: InstallModule
  dependsOn:
  - PowerShell
  properties:
    executable: C:\Program Files\PowerShell\7\pwsh.exe
    arguments:
      "0": -NoProfile
      "1": -NoLogo
      "2": -Command
      "3": >-
        if (-not (Get-Module -ListAvailable -Name MyModule))
        { Install-PSResource -Name MyModule -TrustRepository -AcceptLicense }
      treatAsArray: true
  metadata:
    description: Ensure MyModule is installed
```

### Microsoft.DSC.Transitional/PowerShellScript

Runs PowerShell 7 (pwsh) scripts with separate get, test, and set operations. Unlike `RunCommandOnSet`, this resource supports proper test operations and can report whether the system is already in the desired state.

**Properties:**

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `getScript` | string | No | Script that returns the current state. |
| `testScript` | string | No | Script that exits with code 0 if in desired state, non-zero otherwise. |
| `setScript` | string | No | Script that applies the desired state. |

### Microsoft.DSC.Transitional/WindowsPowerShellScript

Identical to `PowerShellScript` but runs under Windows PowerShell 5.1 instead of PowerShell 7. Use this when the script depends on modules or features only available in Windows PowerShell.

### Adapted resources

Adapted resources are PowerShell class-based DSC v2 resources that run through a compatibility adapter in dscv3. These include:

- `Microsoft.Windows.Developer/*` — Developer settings (e.g., `EnableLongPathSupport`, `EnableRemoteDesktop`, `UserAccessControl`, `WindowsExplorer`, `Taskbar`)
- `Microsoft.Windows.Settings/*` — Windows settings (e.g., `WindowsSettings` for DeveloperMode, color themes)
- `PSDscResources/*` — Standard DSC resources (e.g., `Registry`, `Script`, `Service`)

> [!IMPORTANT]
> Adapted resources require their PowerShell modules to be explicitly installed before use. In v2, WinGet handled module installation automatically. In v3, you must add a `RunCommandOnSet` resource to install each module. See [Installing PowerShell modules for adapted resources](#installing-powershell-modules-for-adapted-resources).

> [!TIP]
> Where a native v3 resource exists (such as `Microsoft.Windows/Registry`), prefer it over the adapted v2 equivalent (`PSDscResources/Registry`). Native resources are significantly faster because they bypass the PowerShell adapter layer.

## Metadata

### Security context (elevation)

Resources that require administrator privileges must declare `securityContext: elevated` in their metadata. When set, WinGet prompts for UAC approval and runs the resource in an elevated process.

```yaml
  metadata:
    winget:
      securityContext: elevated
    description: Resource that requires admin privileges
```

#### When to use elevated security context

| Scenario | Needs elevation? |
|----------|-----------------|
| Package install where WinGet manifest has `ElevationRequirement: elevationRequired` | **Yes** |
| Package install that installs a service | **Yes** |
| Package install with machine-wide scope | **Yes** |
| Registry key under `HKLM:\` | **Yes** |
| Registry key under `HKCU:\` | No |
| `Microsoft.Windows.Developer/EnableLongPathSupport` | **Yes** |
| `Microsoft.Windows.Developer/EnableRemoteDesktop` | **Yes** |
| `Microsoft.Windows.Developer/UserAccessControl` | **Yes** |
| `Microsoft.Windows.Developer/WindowsExplorer` | No |
| `Microsoft.Windows.Developer/Taskbar` | No |
| `Microsoft.Windows.Settings/WindowsSettings` (DeveloperMode) | **Yes** |

> [!WARNING]
> Do not blindly add `securityContext: elevated` to all resources. Adding unnecessary elevation can cause issues and is a security anti-pattern. Only elevate resources that genuinely require administrator privileges.

### Description

The `description` field provides a human-readable explanation of what the resource does. It is displayed during configuration evaluation and in log output.

```yaml
  metadata:
    description: Install Git version control system
```

## Dependencies

Use the `dependsOn` field to specify that a resource must wait for another resource to complete before it can be evaluated. The value is an array of resource `name` values.

```yaml
- type: Microsoft.WinGet/Package
  name: Ubuntu
  dependsOn:
  - WSL
  properties:
    id: Canonical.Ubuntu
    source: winget
    useLatest: true
```

Dependencies are particularly important for:

- Packages that require another package to be installed first (e.g., Ubuntu depends on WSL)
- Adapted resources that depend on their PowerShell module being installed
- Scripts that depend on a tool being installed first

## Installing PowerShell modules for adapted resources

In v3, the dscv3 processor does not automatically install PowerShell modules for class-based DSC resources. You must explicitly install each module using a `RunCommandOnSet` resource before any resources that depend on it.

### Checking for prerelease requirements

Before writing the module install resource, check the [PowerShell Gallery](https://www.powershellgallery.com/) to determine whether a stable version exists:

```powershell
# Check for a stable release
Find-PSResource -Name Microsoft.Windows.Developer

# Check for prerelease releases
Find-PSResource -Name Microsoft.Windows.Developer -Prerelease
```

Only use the `-Prerelease` flag when no stable version is available.

### Module install pattern

Use `Install-PSResource` (available in PowerShell 7) for installing modules:

```yaml
- type: Microsoft.DSC.Transitional/RunCommandOnSet
  name: Microsoft.Windows.Developer.Module
  dependsOn:
  - PowerShell
  properties:
    executable: C:\Program Files\PowerShell\7\pwsh.exe
    arguments:
      "0": -NoProfile
      "1": -NoLogo
      "2": -Command
      # -Prerelease is required because Microsoft.Windows.Developer has no stable release yet
      "3": >-
        if (-not (Get-Module -ListAvailable -Name Microsoft.Windows.Developer))
        { Install-PSResource -Name Microsoft.Windows.Developer -Prerelease
        -TrustRepository -AcceptLicense }
      treatAsArray: true
  metadata:
    description: Ensure Microsoft.Windows.Developer module is installed
```

Then reference the module install resource in `dependsOn` for any resource that uses it:

```yaml
- type: Microsoft.Windows.Developer/EnableLongPathSupport
  name: LongPaths
  dependsOn:
  - Microsoft.Windows.Developer.Module
  properties:
    Ensure: Present
  metadata:
    winget:
      securityContext: elevated
    description: Enable Win32 long paths
```

## Performance considerations

Different resource types have different evaluation costs when running `winget configure test`. Understanding these costs can help you design configurations that evaluate efficiently.

| Resource type | Approximate evaluation cost | Scaling behavior |
|--------------|---------------------------|------------------|
| `Microsoft.WinGet/Package` | ~8–16 seconds per resource | Linear |
| `Microsoft.DSC.Transitional/RunCommandOnSet` | ~5 seconds per resource | Linear |
| `Microsoft.Windows/Registry` (native v3) | ~3 seconds per resource | Linear |
| `Microsoft.Windows.Settings/*` (adapted) | ~95 seconds per resource | Variable |
| `Microsoft.Windows.Developer/*` (adapted) | ~30–122 seconds per resource | Super-linear, worse when elevated |
| `PSDscResources/Registry` (adapted) | ~89–228 seconds per resource | **Super-linear** — grows per additional resource |

Fixed overhead for dscv3 processor startup is approximately 23 seconds.

> [!NOTE]
> Measured on a reference system; actual times vary by hardware and system state. Use these numbers as relative guidance for choosing between resource types, not as absolute guarantees.

> [!TIP]
> Where possible, prefer native v3 resources over adapted v2 resources. For example, replacing 22 `PSDscResources/Registry` resources with `Microsoft.Windows/Registry` can reduce evaluation time from over 30 minutes to under 2 minutes.

## Complete example

The following example demonstrates a v3 configuration file that installs packages, configures Windows settings, sets registry values, and runs scripts:

```yaml
$schema: https://raw.githubusercontent.com/PowerShell/DSC/main/schemas/2023/08/config/document.json
metadata:
  winget:
    processor:
      identifier: dscv3
resources:

# Install packages
- type: Microsoft.WinGet/Package
  name: PowerShell
  properties:
    id: Microsoft.PowerShell
    source: winget
    useLatest: true
  metadata:
    description: Install PowerShell 7

- type: Microsoft.WinGet/Package
  name: VSCode
  properties:
    id: Microsoft.VisualStudioCode
    source: winget
    useLatest: true
  metadata:
    description: Install Visual Studio Code

# Install a PowerShell module for adapted resources
- type: Microsoft.DSC.Transitional/RunCommandOnSet
  name: Microsoft.Windows.Developer.Module
  dependsOn:
  - PowerShell
  properties:
    executable: C:\Program Files\PowerShell\7\pwsh.exe
    arguments:
      "0": -NoProfile
      "1": -NoLogo
      "2": -Command
      # -Prerelease is required because Microsoft.Windows.Developer has no stable release yet
      "3": >-
        if (-not (Get-Module -ListAvailable -Name Microsoft.Windows.Developer))
        { Install-PSResource -Name Microsoft.Windows.Developer -Prerelease
        -TrustRepository -AcceptLicense }
      treatAsArray: true
  metadata:
    description: Ensure Microsoft.Windows.Developer module is installed

# Use an adapted resource (depends on module install)
- type: Microsoft.Windows.Developer/EnableLongPathSupport
  name: LongPaths
  dependsOn:
  - Microsoft.Windows.Developer.Module
  properties:
    Ensure: Present
  metadata:
    winget:
      securityContext: elevated
    description: Enable Win32 long paths

# Illustrative: both approaches shown; in practice, use one
# Use a native v3 registry resource (no module install needed)
- type: Microsoft.Windows/Registry
  name: EnableLongPaths
  properties:
    keyPath: HKLM\SYSTEM\CurrentControlSet\Control\FileSystem
    valueName: LongPathsEnabled
    valueData:
      DWord: 1
    _exist: true
  metadata:
    winget:
      securityContext: elevated
    description: Enable Win32 long path support

# Run a script
- type: Microsoft.DSC.Transitional/RunCommandOnSet
  name: CustomScript
  dependsOn:
  - PowerShell
  properties:
    executable: C:\Program Files\PowerShell\7\pwsh.exe
    arguments:
      "0": -NoProfile
      "1": -NoLogo
      "2": -Command
      "3": |
        Write-Host "Custom configuration complete"
      treatAsArray: true
  metadata:
    description: Run a custom setup script
```

## Sample configurations

For a collection of sample WinGet Configuration files demonstrating various resource types and scenarios, see the [WinGet DSC samples repository](https://aka.ms/winget-samples).

## Converting v2 configuration files to v3

If you have existing WinGet Configuration files using the v2 (DSC schema v0.2) format, a conversion guide is available in the [WinGet DSC samples repository](https://aka.ms/winget-samples) under the **Convert to v3** folder. The guide covers the key differences between v2 and v3, including syntax mapping, resource type changes, and elevation requirements.

A GitHub Copilot CLI skill that converts v2 configuration files to v3 is also available. The skill and installation instructions are documented in the [samples repository README](https://aka.ms/winget-samples).

## Related content

- [WinGet Configuration overview](index.md)
- [How to author a WinGet Configuration file (v2 schema)](create.md)
- [How to check the trustworthiness of a WinGet Configuration file](check.md)
- [`winget configure` command reference](../winget/configure.md)
