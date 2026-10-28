---
title: Add diagnostics and audit logging to a WinUI LOB app
description: Add safe diagnostic and audit signals to a WinUI line-of-business app so administrators and support teams can troubleshoot deployments.
ms.topic: how-to
ms.date: 10/28/2026
author: GrantMeStrength
ms.author: jken
---

# Add diagnostics and audit logging to a WinUI LOB app

Enterprise support teams need enough information to troubleshoot a line-of-business (LOB) app across many devices. Compliance teams might also need an audit trail for important business actions. Plan these signals before rollout so that the app can diagnose failures without logging secrets or sensitive business data.

This article shows a practical logging pattern for a WinUI 3 app. It uses .NET `EventSource` for structured events that can be collected with Event Tracing for Windows (ETW), EventPipe, or your organization's support tooling. If your organization already uses another approved logging library, use the same event design and data-safety rules.

If you need a device-level diagnostics surface instead of app-owned events, Windows Device Portal includes ETW logging and performance tracing. For more information, see [Windows Device Portal overview](../../../advanced-settings/device-portal.md).

## Separate diagnostics from audit events

Diagnostics and audit events serve different purposes.

| Signal type | Purpose | Examples |
|---|---|---|
| Diagnostic event | Help support staff troubleshoot app and environment failures. | App version, package identity, configuration source, service latency, correlation ID, exception type. |
| Audit event | Record an important business or security action. | Record approved, export started, administrative setting changed, privileged workflow completed. |

Keep both streams structured. A text-only log is difficult to filter across devices.

## Decide what to record

Start with events that explain the most common enterprise failures.

| Area | Useful fields |
|---|---|
| App startup | App version, package identity, Windows App SDK runtime version, process architecture, packaged or unpackaged state. |
| Configuration | Effective environment, configuration source, managed-setting conflicts, invalid policy values. |
| Authentication | Tenant, account type, correlation ID, failure category. Don't log tokens or credentials. |
| Service calls | Endpoint host, operation name, HTTP status, elapsed time, retry count, correlation ID. Don't log full payloads by default. |
| Data sync | Sync direction, item count, conflict count, failure category. |
| Deployment | Installer version, package family name, runtime dependency status. |
| Business audit | Actor, action, target record identifier, result, server correlation ID. |

Don't log access tokens, passwords, refresh tokens, client secrets, connection strings, full request or response bodies, or personal data unless your organization explicitly approved that data handling.

## Create an EventSource

Use stable event IDs and names. Add new events over time, but avoid changing the meaning of an existing event ID.

```csharp
using System.Diagnostics.Tracing;

[EventSource(Name = "Contoso-PortfolioApp")]
public sealed class PortfolioAppEventSource : EventSource
{
    public static readonly PortfolioAppEventSource Log = new();

    private PortfolioAppEventSource()
    {
    }

    [Event(1, Level = EventLevel.Informational)]
    public void AppStarted(
        string appVersion,
        string packageName,
        string windowsAppSdkRuntimeVersion,
        string configurationSource)
    {
        WriteEvent(1, appVersion, packageName, windowsAppSdkRuntimeVersion, configurationSource);
    }

    [Event(2, Level = EventLevel.Warning)]
    public void ManagedSettingConflict(
        string settingName,
        string policySource,
        string userPreferenceSource)
    {
        WriteEvent(2, settingName, policySource, userPreferenceSource);
    }

    [Event(3, Level = EventLevel.Warning)]
    public void ServiceRequestFailed(
        string operationName,
        string endpointHost,
        int statusCode,
        string correlationId,
        int elapsedMilliseconds)
    {
        WriteEvent(3, operationName, endpointHost, statusCode, correlationId, elapsedMilliseconds);
    }

    [Event(4, Level = EventLevel.Informational)]
    public void BusinessActionCompleted(
        string actionName,
        string targetId,
        string result,
        string serverCorrelationId)
    {
        WriteEvent(4, actionName, targetId, result, serverCorrelationId);
    }
}
```

If an event might contain optional data, prefer a safe placeholder such as `Unknown` over logging raw objects or serialized payloads.

## Log startup and configuration

Record the app version and effective configuration source during startup. This helps support staff verify that the right package and policy were applied.

```csharp
public static void LogStartup(AppConfiguration configuration)
{
    string packageName = AppInfo.Current.PackageName ?? "Unpackaged";

    PortfolioAppEventSource.Log.AppStarted(
        appVersion: AppInfo.Current.Version,
        packageName: packageName,
        windowsAppSdkRuntimeVersion: AppInfo.Current.WindowsAppSdkRuntimeVersion,
        configurationSource: configuration.ConfigurationSource);
}
```

The `AppInfo` type in this example represents your own helper that normalizes package and runtime information. The exact implementation depends on whether your app is packaged, packaged with external location, or unpackaged.

## Add correlation IDs to service calls

When a WinUI app calls a service, generate or propagate a correlation ID so that client logs can be matched to server logs.

```csharp
using System.Diagnostics;

public async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
{
    string correlationId = Guid.NewGuid().ToString("N");
    request.Headers.Add("x-correlation-id", correlationId);

    Stopwatch stopwatch = Stopwatch.StartNew();

    try
    {
        HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            PortfolioAppEventSource.Log.ServiceRequestFailed(
                operationName: request.RequestUri?.AbsolutePath ?? "Unknown",
                endpointHost: request.RequestUri?.Host ?? "Unknown",
                statusCode: (int)response.StatusCode,
                correlationId: correlationId,
                elapsedMilliseconds: (int)stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
    catch (HttpRequestException)
    {
        PortfolioAppEventSource.Log.ServiceRequestFailed(
            operationName: request.RequestUri?.AbsolutePath ?? "Unknown",
            endpointHost: request.RequestUri?.Host ?? "Unknown",
            statusCode: 0,
            correlationId: correlationId,
            elapsedMilliseconds: (int)stopwatch.ElapsedMilliseconds);
        throw;
    }
}
```

Don't convert a failed request into a successful result just to simplify the UI. Surface an actionable error and keep the diagnostic event available for support.

## Record audit events deliberately

Audit events should align with business and compliance requirements. Don't use a diagnostic trace as the only system of record for regulated or security-sensitive audit data. In many enterprise apps, the service owns the authoritative audit trail, and the client logs only the client-side correlation ID and result.

For each audit event, define:

- The action.
- The actor identity format.
- The target identifier.
- The result.
- The correlation ID that connects client and service logs.
- Retention and access requirements.

Avoid logging full names, email addresses, or business payloads when a stable identifier is enough.

## Log agent-initiated actions

When an agent can invoke business actions, record the authenticated user or workload, verified integration identifier, tool or business operation, outcome, and shared correlation ID. Derive the integration channel from trusted service context, not an agent-supplied label.

Distinguish draft creation, approval, commit, denial, and reversal. For actions that require approval, record the authorized approver and approved draft version. Keep the service's authoritative audit trail separate from optional client diagnostics, and don't suppress required audit events when an administrator reduces diagnostic verbosity.

Apply the same data-minimization and retention rules as other business audit events. For the governance decisions and audit fields to plan, see [Govern agent-initiated actions in WinUI line-of-business apps](govern-agent-actions.md). For MCP integration, see [Make a WinUI line-of-business app usable by AI agents](agent-ready-lob-apps.md).

## Let administrators change logging detail

Use a managed setting for the diagnostic level so support can increase logging detail during an incident. If your app gates event emission itself, validate the configured level before applying it.

```csharp
public static EventLevel ParseLogLevel(string value)
{
    return value.Trim().ToLowerInvariant() switch
    {
        "critical" => EventLevel.Critical,
        "error" => EventLevel.Error,
        "warning" => EventLevel.Warning,
        "information" => EventLevel.Informational,
        "informational" => EventLevel.Informational,
        "verbose" => EventLevel.Verbose,
        _ => EventLevel.Warning
    };
}

public static bool ShouldWrite(EventLevel eventLevel, EventLevel minimumLevel)
{
    return eventLevel != EventLevel.LogAlways && eventLevel <= minimumLevel;
}
```

For example, if the managed setting resolves to `Warning`, the app writes `Critical`, `Error`, and `Warning` events but skips `Informational` and `Verbose` events. Collection tools can also filter events by provider level when support staff capture ETW or EventPipe traces.

For a policy-aware settings pattern, see [Make a WinUI LOB app policy-aware](policy-aware-settings.md).

## Provide a support collection path

Document how support staff collect diagnostics. The exact collection command depends on your organization's tools, but your app documentation should state:

- Which provider name to collect.
- Which app version and package identity to capture.
- Where local app logs are stored, if you write any files.
- How to reproduce the issue while tracing is enabled.
- How to scrub or protect collected data before sharing.

If you write log files, put them under an appropriate per-user or per-machine app data location, rotate them, and set a retention limit. Don't write logs under the installed app directory.

## Test logging before rollout

Test these cases before broad deployment:

- Startup logs identify the app package and effective configuration source.
- Service failures include a correlation ID that the service also records.
- Authentication failures don't include tokens or credentials.
- Policy conflicts are visible in diagnostics and in the app UI.
- Audit events distinguish successful, denied, and approval-required business actions.
- Increasing the logging level doesn't expose sensitive payloads.
- Support staff can collect and interpret logs on a standard managed device.

## Related content

- [Plan enterprise manageability for WinUI LOB apps](enterprise-manageability.md)
- [Make a WinUI LOB app policy-aware](policy-aware-settings.md)
- [Sign in users and call a protected enterprise API from a WinUI app](sign-in-call-api.md)
- [EventSource](/dotnet/core/diagnostics/eventsource)
- [Windows Device Portal overview](../../../advanced-settings/device-portal.md)
- [HTTP client](../../develop/networking/httpclient.md)
