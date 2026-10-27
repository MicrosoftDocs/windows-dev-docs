---
title: MXC architecture and containment models
description: Understand the MXC policy, lifecycle, and backend architecture, and choose a containment model for app and coding-agent workloads.
author: GrantMeStrength
ms.author: jebishop
ms.topic: overview
ms.date: 10/27/2026
---

# MXC architecture and containment models

MXC separates workload intent from the platform mechanism that runs the workload. Your host app uses the .NET, TypeScript, or Rust SDK to describe resource permissions and request execution. MXC translates the request into a containment backend's operations on the current host.

This design gives apps a consistent integration surface without claiming that every backend provides the same capabilities or isolation properties.

## Architecture layers

An MXC integration has four main layers:

| Layer | Responsibility |
|---|---|
| **Host app or agent runtime** | Classifies the task, selects trusted policy defaults, supplies inputs, and handles output and approval |
| **MXC SDK and policy contract** | Discovers support, converts workload intent into a versioned configuration, starts sandboxes, and exposes lifecycle and diagnostics |
| **MXC execution layer** | Validates the configuration, resolves a backend, and translates the supported contract to platform operations |
| **Containment backend** | Creates the execution environment and enforces the policy features that the platform and backend support |

The host app remains responsible for policy decisions. A coding agent can suggest a model or resource grant, but it must not choose or expand its own authority without trusted logic or human approval. Code that the host runs outside MXC remains outside the workload's containment.

## Policy, configuration, and runtime settings

The policy describes what the workload may access. The execution request adds what to run, where to run it, and which containment to use. Runtime settings supply values needed for execution, such as an already-running proxy's endpoint.

The V1 SDKs carry these settings in a single `ContainerRequest`. Its filesystem, network, and UI properties describe access, and its command, working directory, environment, timeout, and containment properties describe execution. The SDK converts the request to the stable `1.0.0` schema contract, so you don't specify a schema version.

The JSON execution configuration isn't interchangeable with a serialized SDK object. For example, the request's `ui.allowInputInjection` and `timeoutMs` properties correspond to the configuration's `ui.injection` and `process.timeout` settings. Use the SDK types, or the schema for the configuration version you author.

## One-shot and state-aware lifecycles

MXC supports two lifecycle patterns.

**One-shot execution** creates the sandbox, runs one workload, and tears the sandbox down. Use it for independent coding-agent tools, generated commands, builds, tests, and scripts. A new narrow sandbox for each action limits retained state and reduces the pressure to create one broad policy.

**State-aware execution** exposes five phases: provision, start, execute, stop, and deprovision. Provisioning returns an opaque `ContainerId`, which the caller passes unchanged to later phases, and the caller can execute multiple commands before teardown. Use this pattern only when the workload depends on persistent state, such as an agent profile, installed tools, configuration, or an operating-system environment. In MXC 1.0, the V1 SDK lifecycle APIs support IsolationSession and WSLc.

State-aware execution doesn't make MXC the state owner. The backend owns the sandbox resource, and the caller stores its identifier and manages its lifecycle. Stopping execution and deprovisioning resources are different operations; exactly which state survives a stop depends on the backend.

**Run, spawn, and PTY execution** describe how your host interacts with a running process, not how long the environment exists. A run call returns captured output when execution finishes. A spawn call gives your host live standard input, output, and error streams while the workload runs. A PTY call connects the workload to a pseudoterminal that your host controls, including its size. Check the selected SDK and backend for supported combinations.

### Host responsibilities

Set an execution timeout and handle workload failure, launch failure, and cancellation separately. Cancelling an SDK wait isn't necessarily the same as terminating the workload. Follow the SDK's termination and disposal contract, and complete the required wait so execution and optional denial capture can finalize.

For state-aware execution, arrange cleanup even when a later phase fails. A failed stop shouldn't prevent an attempt to deprovision resources that your app owns.

Sandbox teardown doesn't generally undo writes to granted host directories or actions performed through permitted network services. Choose output locations deliberately, and decide which results to retain before subsequent tasks use them.

## Choose a containment type

Start with your workload's goals and constraints, for example:

| Workload need | Starting type | Why |
|---|---|---|
| Commands that need selected host tools and paths with low startup cost | **Process isolation** | Runs near the host environment with permissions scoped to the task |
| Workload that needs a separate Windows profile or interactive session | **Session isolation** | Separates the workload's user and session state from the interactive user's environment, without MXC file, network, or UI filtering |
| Workload that requires a separate kernel or guest operating environment | **Virtualized environment** | Uses a VM or micro-VM boundary, subject to the selected backend's capabilities |

On Windows, process isolation is a common type because it matches many workloads that need selected host resources with low startup cost.

Session isolation separates users and sessions, but it doesn't restrict what the workload can access. On Windows, IsolationSession rejects filesystem and UI policies. Its request must also set egress, ingress, and host loopback to `allow`, with no network rules or proxy. If the workload needs those restrictions, choose a backend that can enforce them. For details, see [Backend selection and settings](https://github.com/microsoft/mxc/blob/main/docs/containment-policy/1.0.0/policy.md#backend-selection-and-settings).

An abstract intent, such as `process`, lets MXC resolve a platform-specific backend. Select a concrete backend when you require a particular operating environment or lifecycle. Note that neither an abstract intent nor a stronger isolation mechanism guarantees support for every file, network, or UI control.

See [MXC containment options and platform support](platform-support.md) for selection guidance and links to detailed backend documentation.

## Learning Mode in the architecture

Learning Mode helps with policy authoring and diagnostics for Windows ProcessContainer workloads. See [Create and refine MXC policies with Learning Mode](learning-mode.md).

## Design guidance for coding agents

When using a coding agent to help choose a containment type, ask it to evaluate:

1. Which operating systems and processor architectures must the app support?
2. Which files, network paths, user interface features, devices, and host tools does the workload need?
3. Is each command independent, or must identity and state persist between commands?
4. What startup time, resource use, deployment, and interaction constraints apply?
5. Does the workload need Windows, Linux, a full desktop, or another operating environment?
6. Which isolation boundary is appropriate for the workload's trust assumptions and data?
7. Which requested policy features can each available backend enforce?

The agent should compare viable containment types, state its assumptions and tradeoffs, and identify unsupported requirements. You can then select an appropriate containment type and build the policy.

## Related content

- [Microsoft Execution Containers overview](index.md)
- [Quickstart: Run a workload with the MXC SDK](quickstart.md)
- [Author and validate an MXC policy](policy.md)
- [MXC containment options and platform support](platform-support.md)
- [Create and refine MXC policies with Learning Mode](learning-mode.md)
- [MXC policy examples](examples.md)
- [MXC releases](releases.md)
