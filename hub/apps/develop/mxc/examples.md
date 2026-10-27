---
title: MXC policy examples
description: Adapt MXC configuration examples for repository tools, protected credentials, direct network access, custom proxies, and UI.
author: GrantMeStrength
ms.author: jebishop
ms.topic: how-to
ms.date: 10/27/2026
---

# MXC policy examples

These examples show how workload requirements translate into MXC resource permissions. You can start with the [C#, TypeScript, or Rust quickstart](quickstart.md), then adapt the configuration to your workload.

The JSON below uses the raw **execution configuration** shape for the MXC `1.0.0` schema. Full configurations include a version and command; sections labelled as fragments must be incorporated into an existing configuration. When you author in C#, TypeScript, or Rust, use the corresponding V1 SDK types, which don't require a specific schema version.

These are Windows examples. Replace the paths with existing resources on your host, grant any additional tool dependencies deliberately, and confirm that your SDK release and backend support the selected controls.

## Run a repository-scoped tool

This complete configuration uses the same read-input/write-output pattern as the quickstart. Create `C:\src\my-app\input.txt` and the `C:\src\mxc-output` directory before running it. The command can read the source directory and write the output directory, but can't write source or use the network or UI.

```json
{
  "$schema": "https://raw.githubusercontent.com/microsoft/mxc/main/schemas/stable/mxc-config.schema.1.0.0.json",
  "version": "1.0.0",
  "containment": "process",
  "process": {
    "commandLine": "C:\\Windows\\System32\\cmd.exe /d /c type input.txt > C:\\src\\mxc-output\\result.txt",
    "cwd": "C:\\src\\my-app",
    "env": [
      "SystemRoot=C:\\Windows",
      "LOCALAPPDATA=C:\\src\\mxc-output",
      "TEMP=C:\\src\\mxc-output",
      "TMP=C:\\src\\mxc-output"
    ],
    "timeout": 30000
  },
  "filesystem": {
    "readonlyPaths": ["C:\\src\\my-app", "C:\\Windows\\System32\\cmd.exe"],
    "readwritePaths": ["C:\\src\\mxc-output"]
  },
  "network": {
    "egress": { "default": "deny" },
    "ingress": { "default": "deny", "hostLoopback": "deny" }
  },
  "ui": {
    "disable": true,
    "clipboard": "none",
    "injection": false
  }
}
```

This example assumes Windows is installed at `C:\Windows`; use the actual system directory on your host. A Windows ProcessContainer request with an explicit `env` list must include both `SystemRoot` and `LOCALAPPDATA`. When you replace the command with a build or analysis tool, configure its caches and temporary files to use the dedicated writable directory rather than granting write access to the entire repository.

## Protect credentials inside a broader grant

The preceding configuration grants read access to `C:\src\my-app`. If that directory contains an environment file or a secrets directory, add this **fragment inside its existing `filesystem` section**, preserving the read-only and read-write lists:

```json
{
  "deniedPaths": [
    "C:\\src\\my-app\\.env",
    "C:\\src\\my-app\\secrets"
  ]
}
```

These denied paths are inside the granted source directory. Keep unrelated repositories and user profile directories outside the grants.

Keep secrets outside granted trees when possible. Verify overlapping grants and denies on the selected backend; some mounted-directory backends reject nested denies instead of enforcing them.

## Allow direct access to an IP range

This **configuration fragment replaces the `network` section**. It permits outbound TCP port 443 to an approved CIDR range and denies other egress, ingress, and host-loopback access. Supply approved ranges from trusted configuration rather than accepting them from the workload.

```json
{
  "network": {
    "egress": {
      "default": "deny",
      "allow": [
        {
          "to": [{ "cidr": "192.0.2.0/24" }],
          "ports": [{ "protocol": "tcp", "port": 443 }]
        }
      ]
    },
    "ingress": { "default": "deny", "hostLoopback": "deny" }
  }
}
```

`192.0.2.0/24` is a documentation-only range. Replace it with the intended service's approved addresses. The rule permits TCP on that port, not exclusively HTTPS, one hostname, or one application service. Direct hostname resolution can require a separate DNS rule.

This example requires a backend and host that support direct egress rules. Don't combine it with `runtimeConfig.networkProxy`. For hostname-based HTTP(S) decisions, use a supported proxy-based configuration instead.

## Use an application-provided proxy

Your host app must start its proxy before launching the workload. This example configures MXC to use an existing proxy; it doesn't implement, start, or approve the proxy's request-handling policy.

The following **configuration fragment** targets Windows ProcessContainer with a supported OS-scoped proxy enforcement path. Merge it into the full configuration, retaining the command, filesystem permissions, timeout, and UI restrictions. Replace the endpoint and peer name with those of your running proxy.

```json
{
  "containment": "processcontainer",
  "network": {
    "egress": { "default": "deny" },
    "ingress": { "default": "allow", "hostLoopback": "deny" }
  },
  "runtimeConfig": {
    "networkProxy": "http://127.0.0.1:8080"
  },
  "processContainer": {
    "network": {
      "allowedProxyPeer": "<package-family-name-or-appcontainer-profile-name>"
    }
  }
}
```

`allowedProxyPeer` identifies the proxy's installed package or unpackaged AppContainer profile. The proxy must listen on the configured loopback address and port and meet the documented Windows prerequisites. MXC doesn't infer that identity from the URL.

> [!IMPORTANT]
> This Windows mapping requires `ingress.default: "allow"`, which also permits private-network server traffic. `hostLoopback: "deny"` keeps general host-loopback access closed while the configured proxy peer is reachable. The example isn't an all-inbound-denied configuration.

Direct internet access remains denied on the enforcing path. The proxy controls which HTTP(S) requests it forwards. A plain, unpackaged host proxy without an AppContainer identity needs a different, broader compatibility configuration; don't remove `allowedProxyPeer` and assume the same restrictions remain.

Use the corresponding SDK types, and verify support before relying on proxy-only access. See [ProcessContainer proxy deployment choices](https://github.com/microsoft/mxc/blob/main/docs/backends/process-container/networking.md#proxy-deployment-choices) for Windows prerequisites and [directional networking](https://github.com/microsoft/mxc/blob/main/docs/schema.md#directional-networking-supported-contracts) for other backends.

## Allow a visible tool without broad UI access

For a workload that needs a visible window and clipboard input, this **configuration fragment replaces `ui`**. It allows windows and clipboard reads, but not clipboard writes or synthetic keyboard and pointer input:

```json
{
  "ui": {
    "disable": false,
    "clipboard": "read",
    "injection": false
  }
}
```

In a V1 SDK `ContainerRequest`, the equivalent UI properties are `disable`, `clipboard`, and `allowInputInjection`. Grant clipboard reads only when the workload requires them. Check runtime UI support and test the required restrictions on each target host.

## Reuse a state-aware sandbox

Use state-aware execution when commands must share an environment. Provision and start it, execute the commands, then stop and deprovision it when finished. Arrange cleanup after failure and cancellation, not just success.

State-aware calls use a phase-specific `ProvisionRequest` and a separate `ExecutionRequest` for each command, not the complete one-shot configuration above. Provisioning returns an opaque `ContainerId`; pass it unchanged to later phases. Use `MxcLifecycle` in .NET, the TypeScript lifecycle functions such as `provisionContainer`, or the Rust `v1::container` functions such as `provision_container`, with IsolationSession or WSLc, the backends that the V1 lifecycle APIs support. See [lifecycle concepts](architecture.md#one-shot-and-state-aware-lifecycles), [MXC container lifecycle](https://github.com/microsoft/mxc/blob/main/docs/container-lifecycle.md), and the [.NET](https://github.com/microsoft/mxc/blob/main/sdk/dotnet/README.md#lifecycle-api), [TypeScript](https://github.com/microsoft/mxc/blob/main/sdk/node/README.md#lifecycle-api), or [Rust](https://github.com/microsoft/mxc/blob/main/src/mxc-sdk/README.md#lifecycle-api) SDK guide for exact requirements.

Check what each lifecycle backend enforces before you choose it. IsolationSession separates the workload's user and session but doesn't apply MXC filesystem, UI, or network filtering, and its request must explicitly allow all network traffic. WSLc networking is either fully isolated or fully bridged, without per-destination rules. For details, see [Choose a containment type](architecture.md#choose-a-containment-type) and [Backend selection and settings](https://github.com/microsoft/mxc/blob/main/docs/containment-policy/1.0.0/policy.md#backend-selection-and-settings).

## Verify the examples

For each adapted policy, test both intended behavior and attempted misuse:

- Confirm that required inputs can be read and required outputs can be written.
- Confirm that credentials and unrelated files remain inaccessible.
- Confirm that direct, local-network, loopback, and inbound connections match the policy.
- For proxy-based access, confirm that an approved request succeeds and a direct internet attempt fails.
- Confirm that clipboard, input injection, and visible windows match the UI policy.
- Confirm that timeouts and cleanup work after success, failure, and cancellation.

Use [Learning Mode](learning-mode.md) to investigate missing access. Don't automatically grant every resource that a representative run observes.

## Related content

- [Author and validate an MXC policy](policy.md)
- [Create and refine MXC policies with Learning Mode](learning-mode.md)
- [MXC containment options and platform support](platform-support.md)
- [MXC architecture and containment models](architecture.md)