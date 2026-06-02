---
title: Dev Configs for Windows
description: Get from a fresh Windows install to a ready-to-code environment in minutes with declarative WinGet Configuration files for toolchains, OS settings, and shells.
ms.topic: overview
ms.date: 05/27/2026
---

# Dev Configs for Windows

Dev Configs are a curated, open-source collection of configuration files that take a fresh Windows machine to a ready-to-code state with a single command. Each config is a declarative, idempotent file describing the packages, OS settings, and post-install steps for a specific scenario — a full developer workstation, a comfortable WSL shell, or a single language toolchain — so you can rebuild your environment on any machine without clicking through installers or maintaining custom scripts.

The configs are maintained in the open at [github.com/microsoft/WindowsDeveloperConfig](https://github.com/microsoft/WindowsDeveloperConfig) and validated on every commit by CI on a clean Windows runner. The repo's README is the source of truth for current commands, file paths, options, and the full list of supported toolchains — this page stays intentionally high-level so it doesn't drift.

<div class="buttons margin-top-xs margin-bottom-sm">
    <a class="button button-sm button-filled button-primary" href="https://github.com/microsoft/WindowsDeveloperConfig" target="_blank" rel="noopener">
        <span class="icon" aria-hidden="true"><span class="docon docon-brand-github"></span></span>
        <span>Windows Developer Configs on GitHub</span>
    </a>
</div>

## Available configs

Dev Configs come in three flavors. Pick the one that matches what you want — see the [repo README](https://github.com/microsoft/WindowsDeveloperConfig#readme) for the exact commands and options.

### Windows Dev Config

A single config that turns a fresh Windows 11 install into a clean, distraction-free developer workstation in one command. It installs a baseline set of developer tools, applies opinionated Windows settings (dark theme, Developer Mode, File Explorer and Start/taskbar cleanup, and similar workstation hygiene), and bootstraps WSL with Ubuntu through the required reboot. Non-interactive and safe to re-run on an existing machine.

### WSL Comfort

An interactive setup for a nicer Windows + WSL shell experience. The Windows side installs WSL, a distro, a Nerd Font, and a themed Windows Terminal profile; the Linux side configures the shell itself with your choice of options (shell, prompt, modern CLI tools, clipboard shims, and more). The Linux half is standalone and can be run directly on any Ubuntu host.

### Workloads (single language toolchains)

If you just want one language stack — TypeScript, Python, .NET, Go, Java, Rust, PHP, WinForms, WinUI 3, and similar — each workload ships its own config plus a small shim that applies it and refreshes `PATH` in your current session. The current list and per-workload details live in the [repo README](https://github.com/microsoft/WindowsDeveloperConfig#-single-language-workloads).

## Related content

- [WinGet Configuration overview](/windows/package-manager/configuration/)
- [Create a WinGet Configuration file](/windows/package-manager/configuration/create)
- [WSL on Windows](/windows/wsl/)
- [PowerToys](/windows/powertoys/)
