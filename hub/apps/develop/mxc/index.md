---
title: Microsoft Execution Containers overview
description: Learn how Microsoft Execution Containers uses .NET, TypeScript, and Rust SDKs to run agent workloads with defined resource permissions.
author: GrantMeStrength
ms.author: jebishop
ms.topic: overview
ms.date: 10/15/2026
---

# Microsoft Execution Containers

Microsoft Execution Containers (**MXC**) is an open-source, policy-driven execution technology for running workloads with defined resource permissions. You can use the **.NET, TypeScript, or Rust SDK** in your app to select a workload, describe what it can access, and run it in a supported containment environment.

Apps, agents, and developer tools often run code that they didn't create or fully control, such as AI-generated commands, third-party tools, and user-provided scripts. Without containment, that code can inherit broad access to files, credentials, network services, and other resources. MXC helps you limit that access to what the task requires.

MXC supports Windows, Linux, and macOS through platform-specific containment backends. A container doesn't necessarily mean a virtual machine or a container image: a workload can run in an operating-system process sandbox, a separate session, or a virtualized environment. Available features and restrictions depend on the MXC release, backend, and host.

These Learn articles explain the concepts and integration workflow. The [MXC documentation on GitHub](https://github.com/microsoft/mxc/tree/main/docs) provides detailed SDK, schema, backend, and implementation guidance.

<div class="buttons margin-top-xs">
	<a href="https://github.com/microsoft/mxc" class="button button-sm"><span class="icon docon docon-brand-github" aria-hidden="true"></span><span>View on GitHub</span></a>
</div>

## Understand the core concepts

| Concept | Meaning |
|---|---|
| **Host app** | Your trusted app or agent runtime, which invokes the SDK, selects permissions, supplies inputs, and handles results |
| **Workload** | The command, script, or process that runs under containment |
| **Policy** | A configuration describing the resource access you permit, such as readable files, writable output directories, network destinations, and UI access |
| **Execution configuration** | The policy together with the command, working directory, runtime settings, and containment selection |
| **Backend** | The platform mechanism that implements the supported restrictions (e.g. an operating-system process sandbox, separate user session, or virtualized environment) |

## Why use MXC

Different tasks need different resources. A repository analysis command might need read-only source access and one writable output folder. A build might also need toolchain files and package downloads. A longer-running workflow might need to retain an environment across commands.

MXC gives your app a common policy and execution model instead of requiring a separate integration with every operating-system containment mechanism. You can use it to:

- Run generated commands, scripts, or external tools without granting the host app's full access.
- Separate readable inputs from writable outputs and keep credentials outside the workload's reach.
- Deny network access, permit selected direct connections, or use an application-provided HTTP(S) proxy on supported backends.
- Restrict windows, clipboard access, and synthetic keyboard or pointer input.
- Create and refine resource permissions with [Learning Mode](learning-mode.md) for Windows ProcessContainer workloads.

Containment limits what a workload can do; it doesn't make generated instructions, outputs, or permitted actions trustworthy. Keep policy decisions in trusted host code and validate results before your app acts on them.

## Get started with MXC

Start with [Run a workload with the MXC SDK](quickstart.md). It provides C#, TypeScript, and Rust examples that read an input, write to a dedicated output folder, and demonstrate a denied operation.

| SDK | Package | Detailed guidance |
|---|---|---|
| .NET | [`Microsoft.Mxc.Sdk` on NuGet.org](https://www.nuget.org/packages/Microsoft.Mxc.Sdk) | [.NET SDK guide](https://github.com/microsoft/mxc/blob/main/sdk/dotnet/README.md) |
| Node.js / TypeScript | [`@microsoft/mxc-sdk` on npm](https://www.npmjs.com/package/@microsoft/mxc-sdk) | [TypeScript SDK guide](https://github.com/microsoft/mxc/blob/main/sdk/node/README.md) |
| Rust | [`mxc-sdk` on crates.io](https://crates.io/crates/mxc-sdk) | [Rust SDK guide](https://github.com/microsoft/mxc/blob/main/src/mxc-sdk/README.md) |

It can be difficult to predict all of the resources your workload might need to access. [Learning Mode](learning-mode.md) can help you create a least-privilege policy on Windows by recording the resources that a workload attempts to access.

You can also use a coding agent to help adapt the examples. The coding agent can help explain observations, but make sure you review and approve any changes to resource access policies.

## How MXC works

Your host discovers supported backend containers, creates a policy and execution request, and starts the workload through the SDK. MXC translates that request into the selected backend's operations. Your host then handles output, exit status, warnings, and cleanup.

> [!NOTE]
> Backends can have different security properties. Confirm that the selected environment can enforce your required controls, and test both permitted and denied operations. See [MXC architecture](architecture.md), [platform support](platform-support.md), and [release notes](releases.md#release-notes).

## Related content

- [Quickstart: Run a workload with the MXC SDK](quickstart.md)
- [MXC architecture and containment models](architecture.md)
- [Author and validate an MXC policy](policy.md)
- [Create and refine MXC policies with Learning Mode](learning-mode.md)
- [MXC containment options and platform support](platform-support.md)
- [MXC policy examples](examples.md)
- [MXC releases](releases.md)
- [MXC repository](https://github.com/microsoft/mxc)
- [Security and responsible AI for Windows development](../ai-assisted/security-and-responsible-ai.md)
