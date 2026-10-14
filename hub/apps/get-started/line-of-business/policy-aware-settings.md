---
title: Make a WinUI LOB app policy-aware
description: Add organization-managed configuration to a WinUI line-of-business app, with precedence, registry policy, and locked-setting UI behavior.
ms.topic: how-to
ms.date: 10/13/2026
author: GrantMeStrength
ms.author: jken
---

# Make a WinUI LOB app policy-aware

Enterprise line-of-business (LOB) apps often need settings that an administrator controls. For example, an organization might need to force a production service endpoint, disable preview features, require a specific sign-in authority, or turn on diagnostic logging during a support incident.

This article shows a pattern for reading organization-managed settings in a WinUI 3 app. The sample uses a registry-backed policy value because registry policy is easy to deploy through Group Policy, Microsoft Intune PowerShell scripts, Configuration Manager, or a custom installer. If your organization uses a service-based configuration system instead, keep the same app pattern: separate administrator policy from user preferences, define precedence, and make locked settings visible in the UI.

For examples of product documentation that exposes administrator policy settings, see [Configure PowerToys with Group Policy Settings](../../../powertoys/grouppolicy.md) and [Configure Dev Drive policy for enterprise business devices](../../../dev-drive/group-policy.md).

## Choose managed settings

Start with settings that affect security, supportability, or environment routing. Don't make every preference an administrator policy.

| Setting | Example policy value | Why administrators might manage it |
|---|---|---|
| Service endpoint | `https://api.contoso.com` | Prevent users from connecting to a test or unapproved environment. |
| Authentication authority | `https://login.microsoftonline.com/{tenantId}` | Require sign-in through the organization's tenant. |
| Feature flag | `true` or `false` | Roll out a feature in phases. |
| Logging level | `Information`, `Warning`, or `Error` | Increase diagnostic detail for support without shipping a new app. |
| Cache retention | Number of days | Enforce data-retention policy for local records. |

Document each setting's name, type, default value, valid values, and whether the user can override it.

## Define precedence

Use a predictable order when the app combines policy, deployment configuration, remote configuration, and user preferences.

| Precedence | Source | Example |
|---|---|---|
| 1 | Organization-managed policy | Registry value deployed by IT. |
| 2 | Deployment or bootstrap configuration | A file installed with the app or supplied by the deployment tool. |
| 3 | Service-side configuration | Feature flags returned after sign-in. |
| 4 | User preference | A setting that the user changes in the app. |
| 5 | App default | The built-in fallback when no source supplies a value. |

When a policy source controls a setting, show the effective value in the app and disable controls that would otherwise let the user change it.

## Store policy values

For a per-machine registry policy, use a path under `HKEY_LOCAL_MACHINE\Software\Policies`. For a per-user policy, use `HKEY_CURRENT_USER\Software\Policies`.

This example uses the following values:

| Value | Type | Example |
|---|---|---|
| `ServiceEndpoint` | `REG_SZ` | `https://api.contoso.com` |
| `EnablePreviewFeatures` | `REG_DWORD` | `0` or `1` |
| `LoggingLevel` | `REG_SZ` | `Warning` |

The sample path is:

```text
HKEY_LOCAL_MACHINE\Software\Policies\Contoso\PortfolioApp
```

> [!NOTE]
> Standard users can usually read policy values under `HKEY_LOCAL_MACHINE\Software\Policies`, but they can't write them. Test access with the deployment and account type that your app supports.

## Read managed settings

Create a small service that reads policy values and returns both the effective value and whether the setting is managed.

```csharp
using Microsoft.Win32;

public sealed record ManagedSetting<T>(T Value, bool IsManaged);

public sealed class AppPolicy
{
    private const string PolicyPath = @"Software\Policies\Contoso\PortfolioApp";

    public ManagedSetting<string> ServiceEndpoint { get; }
    public ManagedSetting<bool> EnablePreviewFeatures { get; }
    public ManagedSetting<string> LoggingLevel { get; }

    public AppPolicy()
    {
        ServiceEndpoint = ReadString(
            "ServiceEndpoint",
            "https://api-dev.contoso.com");

        EnablePreviewFeatures = ReadBool(
            "EnablePreviewFeatures",
            defaultValue: false);

        LoggingLevel = ReadString(
            "LoggingLevel",
            "Warning");
    }

    private static ManagedSetting<string> ReadString(
        string name,
        string defaultValue)
    {
        using RegistryKey? key = Registry.LocalMachine.OpenSubKey(PolicyPath);
        object? value = key?.GetValue(name);

        return value is string text && !string.IsNullOrWhiteSpace(text)
            ? new ManagedSetting<string>(text, IsManaged: true)
            : new ManagedSetting<string>(defaultValue, IsManaged: false);
    }

    private static ManagedSetting<bool> ReadBool(
        string name,
        bool defaultValue)
    {
        using RegistryKey? key = Registry.LocalMachine.OpenSubKey(PolicyPath);
        object? value = key?.GetValue(name);

        return value is int dword && (dword == 0 || dword == 1)
            ? new ManagedSetting<bool>(dword == 1, IsManaged: true)
            : new ManagedSetting<bool>(defaultValue, IsManaged: false);
    }
}
```

Validate values before using them. For example, reject an endpoint that isn't HTTPS, reject an unknown logging level, and record a diagnostic event for invalid policy data.

## Combine policy with user preferences

Keep policy and user preferences separate. Don't copy administrator policy into the user's local settings, because that makes it hard to explain which source controls the current value.

```csharp
public sealed class AppConfiguration
{
    private readonly AppPolicy _policy;
    private readonly UserPreferences _preferences;

    public AppConfiguration(AppPolicy policy, UserPreferences preferences)
    {
        _policy = policy;
        _preferences = preferences;
    }

    public string ServiceEndpoint => _policy.ServiceEndpoint.Value;

    public string LoggingLevel =>
        _policy.LoggingLevel.IsManaged
            ? _policy.LoggingLevel.Value
            : _preferences.LoggingLevel;

    public bool IsLoggingLevelManaged => _policy.LoggingLevel.IsManaged;
}
```

Use policy values at app startup for settings that affect authentication, endpoints, and logging. If your app supports changing a policy while the app is running, define whether the change takes effect immediately or after restart.

## Show locked settings in the UI

When a setting is managed by the organization:

- Show the effective value.
- Disable the control that would edit it.
- Explain that the setting is managed by the organization.
- Avoid exposing the policy storage path to users unless the information is needed for support.

For example:

```xml
<StackPanel Spacing="8">
    <ComboBox
        Header="Logging level"
        ItemsSource="{x:Bind ViewModel.LoggingLevels}"
        SelectedItem="{x:Bind ViewModel.LoggingLevel, Mode=TwoWay}"
        IsEnabled="{x:Bind ViewModel.IsLoggingLevelEditable, Mode=OneWay}" />
    <InfoBar
        IsOpen="{x:Bind ViewModel.IsLoggingLevelManaged, Mode=OneWay}"
        Severity="Informational"
        Message="Your organization manages this setting." />
</StackPanel>
```

## Deploy policy values

Coordinate with the team that owns deployment. The right mechanism depends on how the app is distributed.

| Deployment path | Configuration approach |
|---|---|
| Intune Win32 app | Use the install script, a remediation script, or another approved configuration workflow to set policy values. |
| Configuration Manager | Set policy values in the installation script or deploy them with compliance settings. |
| MSIX package | Keep administrator-controlled configuration outside the app package so it can change without repackaging the app. |
| Unpackaged installer | Write policy values during managed installation only when the installer runs with administrator permissions. |

For Intune Win32 deployment capabilities, see [Win32 App Management in Microsoft Intune](/intune/app-management/deployment/win32). For MSIX distribution options, see [Distribute LOB apps to enterprises](../../publish/distribute-lob-apps-to-enterprises.md).

## Test policy behavior

Before rollout, test these cases:

- No policy values exist, so app defaults and user preferences apply.
- Valid policy values override user preferences.
- Invalid policy values are ignored or fail safely with a supportable diagnostic.
- A standard user can't change a managed setting through the app UI.
- Policy changes are applied according to the documented refresh behavior.
- Policy values don't include secrets such as passwords, client secrets, or tokens.

## Related content

- [Plan enterprise manageability for WinUI LOB apps](enterprise-manageability.md)
- [Add diagnostics and audit logging to a WinUI LOB app](diagnostics-audit-logging.md)
- [Sign in users and call a protected enterprise API from a WinUI app](sign-in-call-api.md)
- [Guidelines for app settings](../../design/app-settings/guidelines-for-app-settings.md)
- [Win32 App Management in Microsoft Intune](/intune/app-management/deployment/win32)
- [Configure PowerToys with Group Policy Settings](../../../powertoys/grouppolicy.md)
- [Configure Dev Drive policy for enterprise business devices](../../../dev-drive/group-policy.md)
