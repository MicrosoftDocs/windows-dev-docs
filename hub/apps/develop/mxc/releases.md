---
title: MXC releases
description: Find the published Microsoft Execution Containers SDK packages for .NET, TypeScript, and Rust, and the complete MXC release notes on GitHub.
author: GrantMeStrength
ms.author: jebishop
ms.topic: reference
ms.date: 10/27/2026
---

# MXC releases

Microsoft Execution Containers (MXC) SDKs are published to the package registry for each language. Release notes for each version are published on GitHub.

You can use [runtime discovery](platform-support.md#start-with-runtime-discovery) to confirm backend and feature availability on each target host.

## Release notes

See [MXC releases on GitHub](https://github.com/microsoft/mxc/releases) for version-specific release notes. The notes describe new features, breaking changes and migration guidance, supported schema versions, and backend status.

## SDK packages

| SDK | Package |
|---|---|
| .NET | [`Microsoft.Mxc.Sdk` on NuGet.org](https://www.nuget.org/packages/Microsoft.Mxc.Sdk) |
| Node.js / TypeScript | [`@microsoft/mxc-sdk` on npm](https://www.npmjs.com/package/@microsoft/mxc-sdk) |
| Rust | [`mxc-sdk` on crates.io](https://crates.io/crates/mxc-sdk) |

Each package page lists the available versions and installation instructions.

## Command-line tools

Each [GitHub release](https://github.com/microsoft/mxc/releases) includes an `mxc-release-binaries.zip` asset with signed Windows binaries in `x64` and `arm64` folders. It includes these tools:

| Tool | Purpose |
|---|---|
| `wxc-exec.exe` | Runs a workload from a JSON configuration, probes host support, and runs Learning Mode audits without host code |
| `wxc-host-prep.exe` | Performs one-time host preparation that some Windows isolation tiers require. It requires administrator approval. See [Host preparation](https://github.com/microsoft/mxc/blob/main/docs/backends/process-container/host-prep.md). |

Installing an SDK package doesn't add these tools to your `PATH`. Extract the folder for your device's architecture, and then run the tools by their full path or add the folder to `PATH` yourself. Use the binaries from the same release as your SDK package.

## Versioning

MXC SDK packages follow [Semantic Versioning](https://semver.org/). A new major version can include breaking API changes, so review the release notes and migration guidance before you upgrade to a new major version.

The SDK package version is independent of the MXC policy schema version. To learn how the SDKs and raw configurations select a schema, see [Choose a compatible schema](policy.md#choose-a-compatible-schema).

## Related content

- [Microsoft Execution Containers overview](index.md)
- [Quickstart: Run a workload with the MXC SDK](quickstart.md)
- [MXC containment options and platform support](platform-support.md)
- [MXC repository](https://github.com/microsoft/mxc)
