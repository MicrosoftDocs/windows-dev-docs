---
title: MXC containment options and platform support
description: Compare MXC containment backends on Windows, Linux, and macOS, check what a host supports, and choose a backend that can enforce your policy.
author: GrantMeStrength
ms.author: jebishop
ms.topic: overview
ms.date: 10/15/2026
---

# MXC containment options and platform support

MXC helps you manage the complexity of using containers: you use MXC to describe a policy and manage a container's lifecycle, but the selected containment backend does the actual enforcement. Each backend is built on a different operating-system mechanism. As a result, backends differ in how they isolate a workload and in how they handle files, networking, UI, and lifecycle.

Start from what your workload needs, and then confirm that a backend can deliver it on the hosts you target. The operating system's name or version alone won't tell you whether a backend is supported.

## Understand host and workload environments

In a request, you can ask for a kind of environment, called a containment intent, or you can name a specific backend. The `process` intent maps to the native process-isolation backend on the host you're running on. Because that backend differs by operating system, the same intent can give you different capabilities on Windows, Linux, and macOS.

| Host | Default process backend | Other environments |
|---|---|---|
| Windows | ProcessContainer | A separate Windows session, a Windows VM, or Linux workloads through WSLc, where supported |
| Linux | Bubblewrap | LXC and specialized virtualization backends, where supported |
| macOS | Seatbelt | Check the release you install for other supported options |

The workload doesn't have to run on the same operating system as the host. For example, WSLc runs a Linux workload on a Windows host. It has its own prerequisites, and it shares resources with the host differently than a Windows process sandbox does.

## Check release status and SDK coverage

Backend support changes from release to release, so check the support information for the package version you install.

In [MXC v1.0.0](https://github.com/microsoft/mxc/releases/tag/v1.0.0), the V1 SDKs can create containers with these backends:

- **Windows:** ProcessContainer, IsolationSession, and WSLc
- **Linux:** Bubblewrap and LXC
- **macOS:** Seatbelt

Windows Sandbox, MicroVM, and Hyperlight are experimental. They require explicit experimental authorization, and you can't create them through the V1 SDK APIs. If you're considering one of these or another specialized environment, check the [release notes](releases.md#release-notes) for your version first.

A backend that's available on the host might still not support the SDK operation you plan to call. In the V1 SDKs:

- The lifecycle APIs work only with IsolationSession and WSLc.
- One-shot PTY execution works with IsolationSession, Bubblewrap, LXC, and Seatbelt.
- PTY execution in an existing container works only with IsolationSession.

For details, see the SDK references:

- [Choose a .NET launch operation](https://github.com/microsoft/mxc/blob/main/docs/api-reference/dotnet/v1/api.md#choosing-a-launch-operation)
- [Choose a TypeScript launch operation](https://github.com/microsoft/mxc/blob/main/docs/api-reference/node/v1/api.md#choosing-a-launch-operation)
- [Choose a Rust launch operation](https://github.com/microsoft/mxc/blob/main/docs/api-reference/rust/v1/api.md#choosing-a-launch-operation)
- [MXC container lifecycle](https://github.com/microsoft/mxc/blob/main/docs/container-lifecycle.md)

If you use Rust, backend support also depends on the crate features you enable. To use IsolationSession or WSLc, enable the `mxc-sdk` crate's `isolation_session` or `wslc` feature. For more information, see the [Rust SDK guide](https://github.com/microsoft/mxc/blob/main/src/mxc-sdk/README.md#build-features-and-backend-support).

## Start with runtime discovery

Each SDK can tell you what the current host supports before you launch anything. Run these checks first, but keep handling launch errors too, because the host's configuration can change between the check and the launch.

Use platform support to find out whether you can launch containers at all, available backends for a broader view of the host, and the probe to test a specific request on Windows before you run it.

| SDK | Discovery API | What to check |
|---|---|---|
| .NET | `MxcPlatform.GetPlatformSupport()` | `IsSupported`, `Reason`, and `AvailableMethods`, which lists the backends the SDK can launch |
| .NET | `MxcPlatform.GetAvailableBackends()` | Host availability, optional capabilities, and warnings. Some execution APIs can't use every backend listed here. |
| .NET | `MxcContainer.Probe()` | On Windows, the ProcessContainer isolation tier the request would get and request-specific compatibility diagnostics, without creating a container |
| TypeScript | `getPlatformSupport()` | `isSupported`, `reason`, `availableMethods`, and any host-specific warnings and capability fields |
| TypeScript | `getAvailableBackends()` | Host availability, optional capabilities, and warnings. Some execution APIs can't use every backend listed here. |
| TypeScript | `probe()` | On Windows, the ProcessContainer isolation tier the request would get and request-specific compatibility diagnostics, without creating a container |
| Rust | `v1::platform_support()` | `is_supported`, `reason`, `available_methods`, and, on Linux, Bubblewrap network capability |
| Rust | `v1::available_backends()` | Host availability, optional capabilities, and warnings. Some execution APIs can't use every backend listed here. |
| Rust | `v1::probe()` | On Windows, the ProcessContainer isolation tier the request would get and request-specific compatibility diagnostics, without creating a container |

Fields that don't apply or can't be detected might be missing from the results. Discovery is a useful first check, but it doesn't validate your whole policy. To be sure, create the request you plan to run and confirm that the selected backend can enforce every control it requires.

Some hosts need extra components or administrator setup before a backend works. Discovery results and launch errors can tell you what's missing, but that doesn't give your app or a coding agent permission to change the host. Ask the person who manages the host to follow the backend's documented setup steps.

For Windows, see [OS-version policy support](https://github.com/microsoft/mxc/blob/main/docs/backends/process-container/os-version-support.md). A Windows version can be supported by MXC and still lack a particular policy feature, so check both.

## Check the restrictions your workload needs

Two backends can accept the same policy syntax and still enforce it differently. Before you settle on a backend, consider areas like:

| Area | Questions to ask |
|---|---|
| Files | Can the backend express the read, write, and deny grants you need, including overlapping ones? Does it share files with the host, mount them, or copy them? |
| Network | Can it enforce your network model, whether direct or through a proxy, including private-network and host-loopback rules? |
| UI | Can it apply the window, clipboard, and input restrictions you need? |
| Lifecycle | Does it run each command independently, or can it keep the environment between commands? What's kept after a stop? |
| Learning Mode | Does the host support ProcessContainer denial capture? If not, what diagnosis workflow does the backend provide? |

The [MXC guides on GitHub](https://github.com/microsoft/mxc) list the supported capabilities, OS builds, and requirements for each backend.

## Choose a backend from your requirements

Before you pick a backend, write down:

- The host operating systems and processor architectures your app supports.
- The guest or tool environment the workload needs.
- The file, network, UI, device, and host-tool access the workload needs.
- Whether each command runs on its own or needs state from earlier commands.
- Your limits on startup time, resources, deployment, and user interaction.
- How strong an isolation boundary the workload and its data call for.
- Whether the backend can enforce every policy control you require.

Then test every host configuration you support, with both positive and negative tests. A successful launch only shows that MXC accepted the configuration. Negative tests, such as an attempt to write outside the granted paths, show whether the restrictions actually hold.

## Related content

- [MXC architecture and containment models](architecture.md)
- [Author and validate an MXC policy](policy.md)
- [Create and refine MXC policies with Learning Mode](learning-mode.md)
- [MXC policy examples](examples.md)
