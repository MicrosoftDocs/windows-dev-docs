---
title: Set up Dev Containers on Windows
description: Learn how to set up Dev Containers on Windows using Docker Desktop and WSL 2, including the key file system placement requirement for good performance.
ms.topic: install-set-up-deploy
ms.date: 04/15/2026
---

# Set up Dev Containers on Windows

A Dev Container lets you use a Docker container as a full development environment, defined by a `devcontainer.json` file checked into your repository. Everyone who opens the project gets the same tools, extensions, and settings — regardless of what's installed on their local machine.

This page covers the Windows-specific setup. For a full introduction to what Dev Containers are and how they work, see [Dev Containers documentation](https://code.visualstudio.com/docs/devcontainers/containers) on the VS Code website.

## Prerequisites

Dev Containers on Windows requires:

- **WSL 2** — Windows Subsystem for Linux, version 2. [Install WSL](/windows/wsl/install) if you haven't already.
- **Docker Desktop for Windows** with the WSL 2 backend enabled. [Download Docker Desktop](https://www.docker.com/products/docker-desktop/) and follow the installer. During setup, ensure **Use WSL 2 based engine** is selected in Docker Desktop settings (**Settings** > **General**).
- **Visual Studio Code** — [Download VS Code](https://code.visualstudio.com/).
- **Dev Containers extension** — Install the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) from the VS Code Marketplace.

## Where you store your files matters

> [!IMPORTANT]
> On Windows, Dev Container performance depends heavily on where your project files live. Store your project in the **WSL 2 file system** (for example, `/home/yourname/projects/`), not in the Windows file system (for example, `C:\Users\yourname\projects\`).

When your files are on the Windows file system (`C:\`), Docker accesses them through a cross-OS file share, which is significantly slower. When your files are in the WSL 2 file system, Docker uses native Linux I/O and performance is substantially better — especially for builds and file-watching tools.

To clone into the WSL 2 file system, open your WSL distribution (for example, Ubuntu) from the Start menu or Windows Terminal, and clone there:

```bash
cd ~
mkdir projects && cd projects
git clone https://github.com/your-org/your-repo.git
```

Then open that folder in VS Code from WSL:

```bash
code your-repo
```

VS Code connects to WSL and detects the `devcontainer.json` if one is present.

## Open a project in a Dev Container

Once your project is open in VS Code (connected to WSL):

1. Press <kbd>F1</kbd> and select **Dev Containers: Reopen in Container**.
2. VS Code builds the container image defined in `.devcontainer/devcontainer.json` (or prompts you to add one if none exists).
3. When the build is complete, VS Code reconnects inside the container with all configured tools and extensions available.

To return to your local environment, press <kbd>F1</kbd> and select **Dev Containers: Reopen Folder Locally**.

## Add a Dev Container to an existing project

If your project doesn't have a `devcontainer.json` yet:

1. Press <kbd>F1</kbd> and select **Dev Containers: Add Dev Container Configuration Files**.
2. Choose a base image (for example, Node.js, Python, or a generic Debian/Ubuntu image).
3. VS Code creates a `.devcontainer/devcontainer.json` file you can check into source control.

For the full reference of `devcontainer.json` options, see [devcontainer.json reference](https://containers.dev/implementors/json_reference/) on the Dev Container Specification site.

## Troubleshooting

**Container starts but file changes aren't detected**
Your project is likely stored on the Windows file system. Move it into WSL (see [Where you store your files matters](#where-you-store-your-files-matters) above).

**Docker Desktop doesn't start or WSL integration is missing**
Open Docker Desktop, go to **Settings** > **Resources** > **WSL integration**, and enable integration for your installed WSL distributions.

**VS Code can't connect to the container**
Ensure Docker Desktop is running before opening VS Code. Check the Docker Desktop system tray icon.

## Next steps

- [Dev Containers documentation](https://code.visualstudio.com/docs/devcontainers/containers) — complete reference from the VS Code team
- [devcontainer.json reference](https://containers.dev/implementors/json_reference/) — all configuration options
- [Get started with Docker remote containers on WSL 2](/windows/wsl/tutorials/wsl-containers) — broader WSL + Docker setup guide
- [Install WSL](/windows/wsl/install) — if you haven't set up WSL yet
