---
title: Author and validate an MXC policy
description: Learn how to define least-privilege file, network, UI, and process access in an MXC policy and validate it on each target host.
author: GrantMeStrength
ms.author: jebishop
ms.topic: how-to
ms.date: 10/15/2026
---

# Author and validate an MXC policy

An MXC policy describes what a workload can access. Start with deny-by-default permissions, add only the resources required for a specific task, and validate the result with representative inputs on every platform your app supports. Each backend also provides the operating-system resources needed to start a workload; deny-by-default doesn't mean the process runs without a system environment.

Keep policy decisions in trusted app code. A tool, plugin, script, or AI-generated command can report what it needs, but it shouldn't select its own containment model or expand its own access.

## Understand policy and configuration

MXC separates resource permissions from the settings that run a command:

- **Policy** settings describe cross-platform intent, such as readable paths, writable paths, network destinations, UI access, and a timeout.
- **Execution** settings add the command, working directory, environment, and containment selection.

In the V1 SDKs, a `ContainerRequest` carries both. In .NET, create a `ContainerRequest` from the command, set its policy and execution properties, and call `MxcContainer.RunAsync` or a spawn API. In TypeScript, pass a `ContainerRequest` object to `run` or a spawn function. In Rust, build a `ContainerRequest` struct and pass it to `v1::run` or a spawn function. Prefer the abstract `Process` containment intent unless your workload needs a particular backend.

The following language-neutral policy settings describe a repository-scoped task. They use the TypeScript `ContainerRequest` property names; .NET uses the same structure with PascalCase names such as `ReadonlyPaths`, and Rust uses snake_case names such as `readonly_paths`. They aren't a complete request: they don't contain a command or select a backend. Create the directories and replace the paths before adapting them.

```json
{
  "filesystem": {
    "readonlyPaths": ["C:\\src\\my-app"],
    "readwritePaths": ["C:\\src\\mxc-output"],
    "deniedPaths": ["C:\\src\\my-app\\.env"]
  },
  "network": {
    "egress": { "default": "deny" },
    "ingress": { "default": "deny", "hostLoopback": "deny" }
  },
  "ui": {
    "disable": true,
    "clipboard": "none",
    "allowInputInjection": false
  },
  "timeoutMs": 30000
}
```

Use the SDK types rather than copying request properties into a raw execution configuration. The shapes differ: for example, the request's `timeoutMs` maps to the configuration's `process.timeout`, `ui.allowInputInjection` maps to `ui.injection`, and `network.runtimeConfig` maps to the top-level `runtimeConfig` section. See the [SDK quickstart](quickstart.md) for complete C#, TypeScript, and Rust examples.

## Choose a compatible schema

Keep these three questions separate:

| Compatibility axis | What to check |
|---|---|
| SDK and product release | Which APIs and runtime components the package provides |
| Policy schema | Which exact configuration contract the release accepts |
| Host capabilities | Which restrictions the selected environment can enforce at run time |

The MXC 1.0 V1 SDKs use the stable `1.0.0` schema internally, so you don't specify a schema version in a `ContainerRequest`. A schema version applies only when you author a raw JSON execution configuration, such as a file for the `wxc-exec` command-line tool.

A raw configuration must declare an exact registered `version`. MXC 1.0 accepts `1.0.0`, the current stable schema, and `0.9.0-alpha`, the minimum supported schema. MXC rejects a missing version, retired versions earlier than `0.9.0-alpha`, such as `0.8.0-alpha`, and unregistered spellings such as `1.0` or `1.0.1`. The `1.1.0-alpha` development contract can change before publication, so don't use it for production configurations. Selecting it doesn't authorize experimental backends.

Keep SDK and native runtime components from the same release together. See [MXC schema versioning](https://github.com/microsoft/mxc/blob/main/docs/schema.md#schema-versioning) for the registered contracts.

## Scope file access

Classify each path by how the workload uses it:

| Access | Use it for |
|---|---|
| Read-only | Source, tools, libraries, and configuration that the workload must inspect but not change |
| Read-write | A task-specific output, cache, or temporary directory |
| Denied | Credentials, unrelated repositories, sensitive configuration, and narrower paths inside a broader grant |

Grant the narrowest practical path. For example, grant a test-results directory instead of the repository root when a command only writes test output. Treat paths discovered from environment variables, package managers, or tool searches as candidates for review, not automatic grants.

Include the tool binaries, libraries, configuration, and temporary paths the workload actually needs. The SDKs provide filesystem-discovery helpers, but their results aren't an approved policy. Review each candidate path before adding it.

Setting a working directory doesn't grant access to that directory. Add it to the appropriate filesystem list explicitly.

Avoid relying on an overlapping path rule unless you have tested it with the selected backend. Backends use different file-access mechanisms, and some can't represent a deny rule inside a mapped or mounted directory.

## Control network access

Keep network access denied unless the task requires it. Choose a network model that the selected backend can enforce:

| Model | Use it for |
|---|---|
| No external access | Analysis or transformation that needs only granted local resources |
| Direct access with IP/CIDR, protocol, and port rules | A task that needs specific direct connections, such as TCP to a known service endpoint |
| HTTP(S) through an application-provided proxy | Requests whose destinations or application-level permissions your proxy controls |

Directional network policy separates:

- **Egress** from the workload to network destinations.
- **Ingress** from private or local networks to listeners in the workload.
- **Host loopback** between the workload and services on the host, in both directions.

IP/CIDR and port rules don't identify an application protocol or hostname. Allowing TCP port 443 doesn't establish that the connection uses HTTPS or reaches only one named service. DNS requests can also require access when the workload resolves destinations directly.

Supported schemas reject the retired network fields, such as `defaultPolicy`, `allowedHosts`, `blockedHosts`, and `network.proxy`. Use the directional `egress` and `ingress` fields instead. See [network policy examples](examples.md#allow-direct-access-to-an-ip-range).

### Use a custom network proxy

Your host app supplies and starts the proxy before it starts the workload. MXC doesn't provide or manage a production proxy. The proxy decides which requests and destinations to permit; MXC configures supported clients to use it and applies the backend's network restrictions.

The directional request uses the network section's `runtimeConfig.networkProxy` setting to identify an HTTP(S) loopback endpoint with an explicit port. In a raw execution configuration, this setting is the top-level `runtimeConfig.networkProxy`. This endpoint is runtime configuration, not an application-layer allowlist. Proxy mode requires default-deny egress and can't be combined with direct egress allow or deny rules.

Client routing and enforcement are different. Proxy environment variables or platform settings tell cooperating HTTP clients where to send requests. A proxy-only enforcement path must also prevent direct internet connections; a cooperative proxy configuration alone doesn't establish that restriction.

MXC doesn't transparently redirect SSH, arbitrary TCP/UDP, or other non-HTTP protocols through the HTTP(S) proxy. Host-loopback and private-network behavior also varies by backend. Review the [custom-proxy configuration example](examples.md#use-an-application-provided-proxy) and its prerequisites instead of assuming the same fragment works everywhere.

## Restrict UI access

By default, don't allow a background workload to create windows, read or write the clipboard, or inject keyboard and pointer input. Add UI access only when it is part of the workload's intended user experience.

Input injection means synthetic keyboard or pointer input, not arbitrary code injection into another process.

UI controls are host- and backend-dependent. Use runtime support information to verify each restriction that your security design requires.

## Bound execution

Set a timeout for every agent action, tool call, or script that should finish within a known period. Supply only the environment variables the process needs, and keep secrets out of both the process environment and readable file paths. Don't assume that omitting the environment creates an empty one: backend defaults and SDK inheritance options differ.

Choose a lifecycle that matches the task. Prefer one-shot execution for independent commands. Use a state-aware sandbox only when commands must share installed tools, identity, files, or operating-system state.

## Validate the effective policy

Before you ship a policy:

1. Use your SDK's [runtime discovery](platform-support.md#start-with-runtime-discovery) and check the selected backend's prerequisites.
2. Create the configuration and handle policy-validation or backend-unavailable errors without weakening the policy.
3. Run representative success cases with the resources the workload should use.
4. Run negative cases that attempt to read secrets, write outside the output directory, use an unapproved network path, or access disallowed UI.
5. Use [Learning Mode](learning-mode.md) to investigate missing access, and review each result before changing the policy.
6. Remove all learning or audit settings and run one final enforcing test.

Fail when the host can't enforce a required control. Don't silently select a backend with broader access or remove a policy field to make a workload run. A successful parse or launch isn't evidence that every intended restriction holds.

## Related content

- [Microsoft Execution Containers overview](index.md)
- [MXC containment options and platform support](platform-support.md)
- [Create and refine MXC policies with Learning Mode](learning-mode.md)
- [MXC policy examples](examples.md)