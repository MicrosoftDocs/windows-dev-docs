---
title: Create and refine MXC policies with Learning Mode
description: Use MXC Learning Mode on Windows to diagnose policy denials during app development or to audit existing workloads before you create a policy.
author: GrantMeStrength
ms.author: jebishop
ms.topic: how-to
ms.date: 10/27/2026
---

# Create and refine MXC policies with Learning Mode

Microsoft Execution Containers (MXC) sandboxes are deny-by-default, so that a contained workload can use only the files, capabilities, and other resources that its policy grants. 

However, it can be difficult to predict all of the resources a workload might need to access. **MXC Learning Mode** helps you create a least-privilege policy on Windows by recording the resources that a workload attempts to access, so you can create or refine a policy from evidence instead of trial and error.

> [!NOTE]
> Learning Mode is currently available for workloads that run in an MXC ProcessContainer on Windows. Use [runtime discovery](platform-support.md#start-with-runtime-discovery) to confirm ProcessContainer support on the host.

## Choose a Learning Mode behavior

Learning Mode has two behaviors. They differ in whether MXC continues to enforce the policy while it records access, which determines who should use each one and when.

| Characteristic | Deny and record | Allow and record |
|---|---|---|
| Ungranted access | Blocked and recorded | Allowed and recorded |
| Policy enforcement | Enforced, but [capture can change the isolation tier](#plan-for-capture-prerequisites) | Deny-by-default relaxed for the run |
| Typical users | App developers and policy authors | IT admins and developers evaluating existing workloads |
| When to use it | While you develop an app and its policy | Before a policy exists, to learn what a trusted workload accesses |
| Capture `mode` value | `block` (default) | `allow` |

### Deny and record: develop apps and policies

Use deny and record (`learningModeLogging`) while you develop an app or agent and its MXC policy. MXC keeps enforcing the policy while it records, so ungranted access fails as it would for your users. Learning Mode adds a record of each access that the policy blocked. On some hosts, capture changes the isolation tier that MXC selects. For details, see [Plan for capture prerequisites](#plan-for-capture-prerequisites).

Common uses include:

- Diagnosing why a contained workload fails, and distinguishing a missing grant from an unrelated error.
- Iterating on a draft policy until the supported scenarios succeed and the negative tests still fail.
- Identifying the capabilities that a workload requests.
- Letting users of your app tune their own sandbox. Your app records what was blocked, presents it in its own UI, and regenerates the policy only after the user approves the change.

Because MXC keeps enforcing the policy, deny and record is the safer choice. Use it whenever you have a policy to test, even an incomplete one.

### Allow and record: evaluate existing workloads before you create a policy

Use allow and record (`permissiveLearningMode`) when you need to learn what an existing, trusted workload accesses before applying an MXC policy to it. For example, an IT admin can run a line-of-business tool or script through its typical tasks on a test device, and then use the results to create a baseline policy before turning on enforcement. A developer who brings an existing tool into MXC can use the same approach to draft a first policy.

Allow and record relaxes deny-by-default enforcement for the run, so treat the workload as if it runs with the user's full permissions. MXC records each access that the policy would have denied. Whenever this mode is active, MXC also returns a security warning with the run result. Your host should surface that warning rather than discard it.

Before you use allow and record:

- Run only trusted code, with representative non-sensitive data, on a controlled test device or account.
- Exercise the workload's typical tasks. Access that a run doesn't exercise doesn't appear in the results.
- Treat the results as a starting inventory. Remove unnecessary grants, and then verify the draft policy with deny and record before you deploy it.

> [!IMPORTANT]
> Don't expose allow-and-record Learning Mode as an agent-callable production tool. Trusted host code or a person must approve its use and any resulting expansion of permissions.

## Plan for capture prerequisites

MXC collects Learning Mode results through one of two capture paths. It selects the path at run time, based on the host and the requested policy. This applies to `captureDenials` in either mode and to the `wxc-exec.exe --audit` workflow.

| Capture path | When MXC uses it | Isolation tier | Elevation |
|---|---|---|---|
| Native capture | Windows provides the process security environment and Learning Mode APIs that MXC needs, and they can represent the requested policy | BaseContainer | None |
| Guarded capture | Native capture is unavailable or can't represent the requested policy | A compatible AppContainer tier | MXC starts a signed helper, `plm.exe`, with elevation to record a Windows Performance Recorder (WPR) trace. The user must approve a UAC prompt or provide administrator credentials. |

Native capture can't represent some policies, such as `processContainer.leastPrivilege`, some network proxy configurations, and denied paths on hosts that don't support them natively. Those requests use guarded capture even on hosts that support native capture. If neither path can preserve the full policy, MXC returns an error instead of weakening the policy or skipping capture.

To find out which path a request will use, probe the request with capture enabled before you launch it. The probe doesn't create a container.

| SDK | Call | Results to check |
|---|---|---|
| .NET | `MxcContainer.Probe(request)` | `Tier`, `Probes.NativeCaptureAvailable`, `Probes.GuardedCaptureAvailable` |
| TypeScript | `probe(request)` | `tier`, `probes.nativeCaptureAvailable`, `probes.guardedCaptureAvailable` |
| Rust | `v1::probe(Some(&request))` | `tier`, `probes.native_capture_available`, `probes.guarded_capture_available` |

A BaseContainer tier means native capture. An AppContainer tier means guarded capture and an elevation prompt. The native capture fact describes the host, not your request, so check the tier as well.

Plan for these differences:

- In an app, probe before you start capture, and tell users when capture requires administrator approval. Don't assume that capture runs silently on every host.
- When an IT admin evaluates workloads, confirm that the test devices support native capture or that an administrator can approve the guarded-capture prompt. The AppContainer tier can also require [host preparation](quickstart.md#prerequisites).
- Treat capture results as evidence for policy authoring. Because guarded capture can run the workload on a different tier than production does, confirm the final policy with a run that doesn't use capture.

## Example: enabling capture through an MXC SDK

Use SDK capture when your app hosts the workload. To evaluate a workload without writing host code, see [Use Learning Mode from the command line](#example-use-learning-mode-from-the-command-line).

Start with the [SDK quickstart](quickstart.md), including its denied-write case. The following fragments extend that example and use the same workload and resource grants. Insert the selected fragment immediately before the SDK launches the workload.

The examples use the MXC 1.0 V1 SDK APIs. In each SDK, you configure capture on an explicit ProcessContainer containment selection, which replaces the quickstart's abstract process intent.

Create a host-owned output directory for the capture. It doesn't need to be writable by the contained workload.

# [.NET](#tab/dotnet)

Set capture options on the request's explicit ProcessContainer containment:

```csharp
string captureDirectory = Path.GetFullPath("captures");
Directory.CreateDirectory(captureDirectory);

request.Containment = new Containment.ProcessContainer
{
    CaptureDenials = new CaptureDenialsPolicy
    {
        Mode = CaptureDenialsMode.Block,
        OutputPath = Path.Combine(captureDirectory, "denials.json"),
    },
};
```

# [TypeScript](#tab/typescript)

Set capture options on the request's explicit ProcessContainer containment:

```typescript
const captureDirectory = resolve('captures');
mkdirSync(captureDirectory, { recursive: true });

request.containment = {
  type: 'processcontainer',
  config: {
    captureDenials: {
      mode: 'block',
      outputPath: join(captureDirectory, 'denials.json'),
    },
  },
};
```

# [Rust](#tab/rust)

Add `use mxc_sdk::v1::configs::{CaptureDenials, CaptureDenialsMode, ProcessContainerConfig};` to the imports. Then set capture options on the request's explicit ProcessContainer containment:

```rust
let capture_directory = env::current_dir()?.join("captures");
fs::create_dir_all(&capture_directory)?;

// The configuration types are non-exhaustive, so start from their defaults.
let mut capture_denials = CaptureDenials::default();
capture_denials.mode = CaptureDenialsMode::Block;
capture_denials.output_path = Some(capture_directory.join("denials.json").display().to_string());

let mut process_container = ProcessContainerConfig::default();
process_container.capture_denials = Some(capture_denials);

let request = ContainerRequest {
    containment: Containment::ProcessContainer(process_container),
    ..request
};
```

---

In `block` mode, the denied-write case still fails. Capturing the denial doesn't grant the write. For an approved allow-and-record run, change only the mode to `CaptureDenialsMode.Allow` in C#, `'allow'` in TypeScript, or `CaptureDenialsMode::Allow` in Rust.

When adapting an existing integration, preserve its other containment settings. Use dedicated capture options, not the reserved internal `learningModeLogging` or `permissiveLearningMode` capability names.

## Locate the completed capture

Wait for execution and capture finalization before reading the result. MXC inserts a unique run identifier into the configured filename, so `denials.json` becomes a name such as `denials.<run-id>.json`. Use the reported path instead of guessing it or scanning for the most recent file.

# [.NET](#tab/dotnet)

After `MxcContainer.RunAsync(request)` completes and you report `result.Warnings`, inspect the result metadata:

```csharp
if (result.OutputMetadata?.CaptureDenialsError is { } captureError)
{
    Console.Error.WriteLine($"Denial capture failed: {captureError.Message}");
    return 1;
}

if (result.OutputMetadata?.CaptureDenials is not { } capture)
{
    Console.Error.WriteLine("The run did not produce denial capture metadata.");
    return 1;
}

Console.WriteLine($"Denial document: {capture.OutputPath}");
Console.WriteLine($"Unique denials: {capture.TotalDenials}");
if (capture.DeniedResourcesTruncated)
{
    Console.Error.WriteLine("The capture is incomplete; don't generate a policy from this run.");
    return 1;
}
```

Keep the quickstart's handling of exit status, timeouts, and `result.Warnings`. In particular, the .NET SDK returns warnings about permissive Learning Mode rather than writing them to your terminal automatically.

If you use streaming execution, complete the terminal wait before inspecting `MxcProcess.OutputMetadata`. Disposing the process without waiting isn't a substitute for capture finalization.

# [TypeScript](#tab/typescript)

After `run(request)` completes and you report `result.warnings`, inspect the result metadata:

```typescript
const captureError = result.outputMetadata?.captureDenialsError;
if (captureError) {
  throw new Error(`Denial capture failed: ${captureError.message}`);
}

const capture = result.outputMetadata?.captureDenials;
if (!capture) {
  throw new Error('The run did not produce denial capture metadata.');
}

console.log(`Denial document: ${capture.outputPath}`);
console.log(`Unique denials: ${capture.totalDenials}`);
if (capture.deniedResourcesTruncated) {
  throw new Error("The capture is incomplete; don't generate a policy from this run.");
}
```

Keep the quickstart's handling of exit status, timeouts, and `result.warnings`.

If you use `spawn`, wait for the process to finish before inspecting `outputMetadata` on the returned process. Disposing the process without waiting isn't a substitute for capture finalization. Don't interpret a missing or incomplete capture as zero denials, and don't use arbitrary text printed by the workload as approval to expand its policy.

# [Rust](#tab/rust)

After `v1::run` returns and you report `result.warnings`, inspect the result metadata:

```rust
let metadata = result.output_metadata.as_ref();
if let Some(error) = metadata.and_then(|metadata| metadata.capture_denials_error.as_ref()) {
    eprintln!("Denial capture failed: {}", error.message);
    return Ok(1);
}

let Some(capture) = metadata.and_then(|metadata| metadata.capture_denials.as_ref()) else {
    eprintln!("The run did not produce denial capture metadata.");
    return Ok(1);
};

println!("Denial document: {}", capture.output_path);
println!("Unique denials: {}", capture.total_denials);
if capture.denied_resources_truncated {
    eprintln!("The capture is incomplete; don't generate a policy from this run.");
    return Ok(1);
}
```

Keep the quickstart's handling of exit status, timeouts, and `result.warnings`.

If you use `v1::spawn`, call `wait` on the returned `MxcProcess` before inspecting its `output_metadata()`. Dropping the process without waiting isn't a substitute for capture finalization. Don't interpret a missing or incomplete capture as zero denials, and don't use arbitrary text printed by the workload as approval to expand its policy.

---

## Read denial output

The actionable JSON document identifies resources, attempted access, and a summary. For the quickstart's denied-write case, a representative entry looks like:

```json
{
  "denials": [
    {
      "resource": "C:\\src\\MxcQuickstart\\workspace\\input\\blocked.txt",
      "resourceType": "file",
      "accessType": "write",
      "pid": 1234,
      "filetime": "132847890123456789"
    }
  ],
  "summary": {
    "exitCode": 1,
    "totalDenials": 1,
    "deniedResourcesTruncated": false
  }
}
```

The document is evidence for policy authoring, not an exhaustive log of everything the workload did. The `resourceType` value determines how to read `resource`. For example, a `file` entry contains an absolute path, and a `capability` entry contains a capability name such as `internetClient`. Some observations can't be expressed as policy grants, and a single input won't exercise every code path.

In allow mode, the document doesn't currently include capability requests. Run the workload with deny and record to identify the capabilities it needs.

If `deniedResourcesTruncated` is `true`, don't generate a policy from that run. An empty document also doesn't prove that a workload is safe or that every intended restriction was exercised.

MXC also writes a verbose file beside the actionable document. For example, `denials.<run-id>.json` produces `denials.<run-id>.verbose.json`. The verbose file is a bounded, redacted diagnostic record that includes observations MXC policy can't act on, such as registry writes. The output metadata doesn't report its path, so derive it from the actionable document's path. Use the verbose file to investigate a failure that the actionable document doesn't explain, not as a list of grants.

## Distinguish a denial from other failures

| Outcome | What to do |
|---|---|
| MXC rejects the configuration | Check the schema, field combinations, and selected backend. Don't grant more access to work around an invalid request. |
| The backend or capture feature is unavailable | Inspect support information and [capture prerequisites](#plan-for-capture-prerequisites). Don't silently disable a required restriction. |
| The workload fails under enforcement | Review its exit status, error output, and completed denial capture. A workload failure isn't necessarily a resource denial. |
| Capture fails or is incomplete | Treat the evidence as unavailable or incomplete, even if the workload exited successfully. |

Structured capture collects the actionable document. The lower-level ProcessContainer `learningMode` setting enables recording deny-and-record events, but doesn't by itself collect this document. Use capture when the host needs to consume the evidence.

## Review observed access

Classify each result before you change the policy. For example:

| Finding | Potential action |
|---|---|
| A required input file | Add the narrowest read-only path that covers it |
| A required output or cache | Add a task-specific read-write path |
| A credential or unrelated user file | Keep it denied and change the workload if necessary |
| A broad parent directory | Identify the required child paths instead of granting the parent |
| An unexpected network, UI, or capability request | Verify why the workload needs it before granting access |
| A resource that MXC can't express in policy | Change the workload or choose a backend that can enforce the requirement |

Observed access can come from startup, discovery, optional features, error handling, or the particular input used in one run. Don't assume that every request is necessary. In the quickstart's negative test, the denied write is the intended result, not a missing permission.

## Example: use Learning Mode from the command line

To evaluate a workload without writing host code, run it with the `wxc-exec.exe` command-line tool and a JSON configuration. For the configuration format, see [Author and validate an MXC policy](policy.md).

Installing an MXC SDK package doesn't add `wxc-exec.exe` to your `PATH`. Download `mxc-release-binaries.zip` from the [MXC release](releases.md#command-line-tools) that matches your SDK version, and run the tool by its full path. The following examples assume that you extracted the archive to `C:\Tools\Mxc` and use the `x64` folder. On Arm64 devices, use the `arm64` folder.

### Capture with either behavior

Select ProcessContainer containment and add `captureDenials` to the configuration's `processContainer` section. For example, an IT admin who evaluates a workload that doesn't have a policy yet can use mode `allow`:

```json
{
  "$schema": "https://raw.githubusercontent.com/microsoft/mxc/main/schemas/stable/mxc-config.schema.1.0.0.json",
  "version": "1.0.0",
  "containment": "processcontainer",
  "process": {
    "commandLine": "C:\\Tools\\Contoso\\ReportTool.exe"
  },
  "processContainer": {
    "captureDenials": {
      "mode": "allow",
      "outputPath": "C:\\MxcAudit\\denials.json"
    }
  }
}
```

Create the output directory before the run, and then pass the configuration file to the tool:

```console
C:\Tools\Mxc\x64\wxc-exec.exe audit-config.json
```

After the run, `wxc-exec` writes one JSON line to standard error with the actual output path and a summary:

```json
{"type":"captureDenials","outputPath":"C:\\MxcAudit\\denials.<run-id>.json","exitCode":0,"totalDenials":2,"deniedResourcesTruncated":false}
```

Use the reported path to read the document. To verify a draft policy under enforcement, add your grants to the configuration and change the mode to `block`.

### Propose an adjusted configuration

For developer-driven policy authoring, the audit workflow runs an allow-and-record capture and also proposes a revised configuration:

```console
C:\Tools\Mxc\x64\wxc-exec.exe --audit --config <path-to-config>
```

The audit workflow writes the denial documents, a trace file, a snapshot of the source configuration, and an `Adjusted_*.json` file that merges the observed access into that configuration. The adjusted configuration is a proposal for review, not an approved policy. MXC doesn't produce it when the capture is truncated, and SDK capture doesn't produce it at all.

The audit workflow supports only ProcessContainer. Don't combine `--audit` with an explicit `processContainer.captureDenials` setting. For the full CLI and SDK contracts, see [Learning-mode capabilities on GitHub](https://github.com/microsoft/mxc/blob/main/docs/logging-access-denied.md).

## Complete the learning loop

Use this workflow to move from discovery to an enforcing policy:

1. Select the containment model before you enable Learning Mode.
2. If the workload doesn't have a policy yet and you can't write a narrow one directly, run it with allow and record in a controlled environment. Exercise its typical tasks.
3. Draft a narrow policy from the evidence, and remove broad and unrelated grants.
4. Run the workload under the draft policy with deny and record, and resolve the remaining denials, including required capabilities.
5. Repeat with representative success, failure, and edge-case inputs.
6. Remove all learning, capture, and audit settings, and run a final enforcing test. Capture can change the isolation tier that MXC selects, so this run confirms how the policy behaves in production.

Stop expanding access when the supported workload succeeds and the negative tests still fail as intended. 

A coding agent can help explain observations, but make sure you review and approve any changes to resource access policies.

## Related content

- [Author and validate an MXC policy](policy.md)
- [MXC policy examples](examples.md)
- [MXC containment options and platform support](platform-support.md)
- [Quickstart: Run a workload with the MXC SDK](quickstart.md)
